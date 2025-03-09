using EasyFinance.Domain.Entities;
using EasyFinance.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace EasyFinance.Infraestructure.DataAccess.Repositories;
public class UserRepository(EasyFinanceDbContext dbContext) : IUserReadOnlyRepository, IUserWriteOnlyRepository
{
    public async Task<bool> ExistActiveUserWithIdentifierAsync(Guid userIdentifier) => await dbContext.Users.AnyAsync(user => user.UserIdentifier == userIdentifier && user.Active);
    public async Task<bool> ExistsUserWithEmailAsync(string email) => await dbContext.Users.AnyAsync(user => user.Email == email && user.Active);
    public async Task<User?> GetByEmailAsync(string email) => await dbContext.Users.FirstOrDefaultAsync(user => user.Email == email && user.Active);
    public async Task RegisterUserAsync(User request) => await dbContext.Users.AddAsync(request);
}

