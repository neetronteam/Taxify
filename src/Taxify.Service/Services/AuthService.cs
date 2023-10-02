using System.Text;
using System.Security.Claims;
using Taxify.Domain.Entities;
using Taxify.Service.Exceptions;
using Taxify.Service.Extensions;
using Taxify.Service.Interfaces;
using Taxify.DataAccess.Contracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace Taxify.Service.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IConfiguration configuration;
    public AuthService(IConfiguration configuration, IUnitOfWork unitOfWork)
    {
        this.configuration = configuration;
        this.unitOfWork = unitOfWork;
    }

    public async ValueTask<string> GenerateTokenAsync(string phone, string originalPassword)
    {
        var user = await this.unitOfWork.UserRepository.SelectAsync(u => u.Phone.Equals(phone));
        if (user is null)
            throw new NotFoundException("This user is not found");

        bool verifiedPassword = PasswordHasher.Verify(user.Password, originalPassword);
        if (!verifiedPassword)
            throw new CustomException("Phone or password is invalid", 400);

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                 new Claim("Phone", user.Phone),
                 new Claim("Id", user.Id.ToString()),
                 new Claim(ClaimTypes.Role, user.Role.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}