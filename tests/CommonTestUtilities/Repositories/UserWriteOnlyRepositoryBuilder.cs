using EasyFinance.Domain.Repositories.User;
using Moq;

namespace CommonTestUtilities.Repositories;
public class UserWriteOnlyRepositoryBuilder
{
    private readonly Mock<IUserWriteOnlyRepository> _repository;

    public UserWriteOnlyRepositoryBuilder() => _repository = new Mock<IUserWriteOnlyRepository>();

    public IUserWriteOnlyRepository Build() => _repository.Object;
}

