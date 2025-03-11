using Bogus;
using EasyFinance.Communication.Request;

namespace CommonTestUtilities.Requests;
public class RequestRegisterUserJsonBuilder
{
    public static RequestRegisterUserJson Build(int passwordLength = 10)
    {
        return new Faker<RequestRegisterUserJson>()
            .RuleFor(user => user.Name, (f) => f.Person.FirstName)
            .RuleFor(user => user.Email, (f) => f.Internet.Email())
            .RuleFor(user => user.Password, (f) => f.Internet.Password(passwordLength));
    }
}

