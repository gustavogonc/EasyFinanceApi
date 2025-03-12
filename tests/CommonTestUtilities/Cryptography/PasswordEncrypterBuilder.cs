using EasyFinance.Domain.Security.Cryptography;
using EasyFinance.Infraestructure.Security.Cryptography;

namespace CommonTestUtilities.Cryptography;
public class PasswordEncrypterBuilder
{
    public static IPasswordEncrypter Build() => new PasswordEncrypter();
}

