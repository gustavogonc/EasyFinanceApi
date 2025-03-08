namespace EasyFinance.Domain.Repositories;
public interface IUnitOfWork
{
    Task CommitAsync();
}

