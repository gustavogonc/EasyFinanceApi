using EasyFinance.Domain.Repositories.Expense;
using Moq;

namespace CommonTestUtilities.Repositories;
public class ExpenseWriteOnlyRepositoryBuilder
{
    private readonly Mock<IExpenseWriteOnlyRepository> _repository;

    public ExpenseWriteOnlyRepositoryBuilder() => _repository = new Mock<IExpenseWriteOnlyRepository>();

    public IExpenseWriteOnlyRepository Build() => _repository.Object;
}

