using EasyFinance.Domain.Security.Cryptography;

namespace EasyFinance.Infraestructure.Security.Cryptography;
public class PasswordEncrypter : IPasswordEncrypter
{
    public string Encrypt(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool IsValid(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}

