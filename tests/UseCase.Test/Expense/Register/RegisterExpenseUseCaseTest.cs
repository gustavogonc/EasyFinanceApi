using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using EasyFinance.Application.UseCases.Expense.Register;
using EasyFinance.Exceptions;
using EasyFinance.Exceptions.ExceptionBase;
using Shouldly;
using Xunit;

namespace UseCase.Test.Expense.Register;
public class RegisterExpenseUseCaseTest
{
    private static RegisterExpenseUseCase CreateUseCase(EasyFinance.Domain.Entities.User user)
    {
        var writeOnly = new ExpenseWriteOnlyRepositoryBuilder().Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new RegisterExpenseUseCase(writeOnly, unitOfWork, loggedUser, mapper);
    }

    [Fact]
    public async void Success()
    {
        var user = UserBuilder.Build();

        var useCase = CreateUseCase(user);

        var expense = RequestRegisterExpenseJsonBuilder.Build();

        Func<Task> act = async () => await useCase.Execute(expense);

        await act.ShouldNotThrowAsync();
    }

    [Fact]
    public async void Error_Zero_Value()
    {
        var user = UserBuilder.Build();

        var useCase = CreateUseCase(user);

        var expense = RequestRegisterExpenseJsonBuilder.Build();
        expense.Value = 0;

        Func<Task> act = async () => await useCase.Execute(expense);

        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();

        exception.GetErrorMessages().Count.ShouldBe(1);
        exception.GetErrorMessages().ShouldContain(ResourceMessageException.EXPENSE_GREATHER_THAN_ZERO);
    }

    [Fact]
    public async void Error_Zero_Month()
    {
        var user = UserBuilder.Build();

        var useCase = CreateUseCase(user);

        var expense = RequestRegisterExpenseJsonBuilder.Build();
        expense.Months = 0;

        Func<Task> act = async() => await useCase.Execute(expense);

        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();

        exception.GetErrorMessages().Count.ShouldBe(1);
        exception.GetErrorMessages().ShouldContain(ResourceMessageException.EXPENSE_MONTHS_GREATHER_THAN_ZERO);
    }

    [Fact]
    public async void Error_Empty_Title()
    {
        var user = UserBuilder.Build();

        var useCase = CreateUseCase(user);

        var expense = RequestRegisterExpenseJsonBuilder.Build();
        expense.Title = string.Empty;

        Func<Task> act = async () => await useCase.Execute(expense);

        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();

        exception.GetErrorMessages().Count.ShouldBe(1);
        exception.GetErrorMessages().ShouldContain(ResourceMessageException.EXPENSE_EMPTY_TITLE);
    }

    [Fact]
    public async void Error_Null_Category()
    {
        var user = UserBuilder.Build();

        var useCase = CreateUseCase(user);

        var expense = RequestRegisterExpenseJsonBuilder.Build();
        expense.Category = null;

        Func<Task> act = async () => await useCase.Execute(expense);

        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();

        exception.GetErrorMessages().Count.ShouldBe(1);
        exception.GetErrorMessages().ShouldContain(ResourceMessageException.EXPENSE_CATEGORY_EMPTY);
    }

    [Fact]
    public async void Error_Null_Type()
    {
        var user = UserBuilder.Build();

        var useCase = CreateUseCase(user);

        var expense = RequestRegisterExpenseJsonBuilder.Build();
        expense.Type = null;

        Func<Task> act = async () => await useCase.Execute(expense);

        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();

        exception.GetErrorMessages().Count.ShouldBe(1);
        exception.GetErrorMessages().ShouldContain(ResourceMessageException.EXPENSE_TYPE_EMPTY);
    }

    [Fact]
    public async void Error_Null_PaymentMethod()
    {
        var user = UserBuilder.Build();

        var useCase = CreateUseCase(user);

        var expense = RequestRegisterExpenseJsonBuilder.Build();
        expense.PaymentMethod = null;

        Func<Task> act = async () => await useCase.Execute(expense);

        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();

        exception.GetErrorMessages().Count.ShouldBe(1);
        exception.GetErrorMessages().ShouldContain(ResourceMessageException.EXPENSE_PAYMENT_METHOD_EMPTY);
    }
}

