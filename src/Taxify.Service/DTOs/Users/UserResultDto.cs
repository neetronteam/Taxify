using Taxify.Domain.Enums;

namespace Taxify.Service.DTOs.Users;

public class UserResultDto
{
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Username { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public Gender Gender { get; set; }
    public Role Role { get; set; }
}