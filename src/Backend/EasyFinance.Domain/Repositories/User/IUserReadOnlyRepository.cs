namespace EasyFinance.Domain.Repositories.User;
public interface IUserReadOnlyRepository
{
    Task<bool> ExistsUserWithEmailAsync(string email);
}

