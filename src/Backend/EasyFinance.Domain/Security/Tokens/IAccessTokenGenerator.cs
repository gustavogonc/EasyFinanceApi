namespace EasyFinance.Domain.Security.Tokens;
public interface IAccessTokenGenerator
{
    string Generate(Guid userIdentifier);
}

