using EasyFinance.Domain.Entities;

namespace EasyFinance.Domain.Services.LoggedUser;
public interface ILoggedUser
{
    Task<User> User();
}

