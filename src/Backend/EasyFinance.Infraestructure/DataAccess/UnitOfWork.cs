using EasyFinance.Domain.Repositories;

namespace EasyFinance.Infraestructure.DataAccess;
public class UnitOfWork(EasyFinanceDbContext dbContext) : IUnitOfWork
{
    public async Task CommitAsync() => await dbContext.SaveChangesAsync();
}

