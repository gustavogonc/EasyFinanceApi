using EasyFinance.Domain.Entities;
using EasyFinance.Domain.Repositories.User;

namespace EasyFinance.Infraestructure.DataAccess.Repositories;
public class UserRepository(EasyFinanceDbContext dbContext) : IUserWriteOnlyRepository
{
    public async Task RegisterUserAsync(User request) => await dbContext.Users.AddAsync(request);
}

