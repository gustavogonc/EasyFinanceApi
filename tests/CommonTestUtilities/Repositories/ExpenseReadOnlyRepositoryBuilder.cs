using EasyFinance.Domain.Repositories.Expense;
using Moq;

namespace CommonTestUtilities.Repositories;
public class ExpenseReadOnlyRepositoryBuilder
{
    private readonly Mock<IExpenseReadOnlyRepository> _repository;

    public ExpenseReadOnlyRepositoryBuilder() => _repository = new Mock<IExpenseReadOnlyRepository>();

    public IExpenseReadOnlyRepository Build() => _repository.Object;
}
