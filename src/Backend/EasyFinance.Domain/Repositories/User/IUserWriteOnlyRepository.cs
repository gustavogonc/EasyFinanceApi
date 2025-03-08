namespace EasyFinance.Domain.Repositories.User;
public interface IUserWriteOnlyRepository
{
    Task RegisterUserAsync(Entities.User request);
}

