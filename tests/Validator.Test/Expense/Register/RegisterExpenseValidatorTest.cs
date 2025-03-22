using CommonTestUtilities.Requests;
using EasyFinance.Application.UseCases.Expense.Register;
using EasyFinance.Exceptions;
using Shouldly;
using Xunit;

namespace Validator.Test.Expense.Register;
public class RegisterExpenseValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new RegisterExpenseValidator();

        var request = RequestRegisterExpenseJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeTrue();
        result.Errors.Count.ShouldBe(0);
    }

    [Fact]
    public void Error_Empty_Title()
    {
        var validator = new RegisterExpenseValidator();

        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Title = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors.Select(error => error.ErrorMessage).ShouldContain(ResourceMessageException.EXPENSE_EMPTY_TITLE);
    }

    [Fact]
    public void Error_Zero_Value()
    {
        var validator = new RegisterExpenseValidator();

        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Value = 0;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors.Select(error => error.ErrorMessage).ShouldContain(ResourceMessageException.EXPENSE_GREATHER_THAN_ZERO);
    }

    [Fact]
    public void Error_Zero_Month()
    {
        var validator = new RegisterExpenseValidator();

        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Months = 0;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors.Select(error => error.ErrorMessage).ShouldContain(ResourceMessageException.EXPENSE_MONTHS_GREATHER_THAN_ZERO);
    }

    [Fact]
    public void Error_Null_Category()
    {
        var validator = new RegisterExpenseValidator();

        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Category = null;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors.Select(error => error.ErrorMessage).ShouldContain(ResourceMessageException.EXPENSE_CATEGORY_EMPTY);
    }

    [Fact]
    public void Error_Null_Type()
    {
        var validator = new RegisterExpenseValidator();

        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Type = null;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors.Select(error => error.ErrorMessage).ShouldContain(ResourceMessageException.EXPENSE_TYPE_EMPTY);
    }

    [Fact]
    public void Error_Null_PaymentMethod()
    {
        var validator = new RegisterExpenseValidator();

        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.PaymentMethod = null;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors.Select(error => error.ErrorMessage).ShouldContain(ResourceMessageException.EXPENSE_PAYMENT_METHOD_EMPTY);
    }
}

