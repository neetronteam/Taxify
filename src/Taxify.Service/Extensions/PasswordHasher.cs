namespace Taxify.Service.Extensions;

public static class PasswordHasher
{
    public static string Hash(this string password)
        => BCrypt.Net.BCrypt.HashPassword(inputKey: password);

    public static bool Verify(this string password, string hashedPassword)
        => BCrypt.Net.BCrypt.Verify(text: password, hash: hashedPassword);
}