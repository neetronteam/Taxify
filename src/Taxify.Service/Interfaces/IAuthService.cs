namespace Taxify.Service.Interfaces;

internal interface IAuthService
{
    ValueTask<string> GenerateTokenAsync(string phone, string originalPassword);
}