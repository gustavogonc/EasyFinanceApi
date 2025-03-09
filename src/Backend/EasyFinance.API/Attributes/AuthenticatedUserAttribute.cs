using EasyFinance.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EasyFinance.API.Attributes;
[AttributeUsage(AttributeTargets.All, Inherited = true)]
public class AuthenticatedUserAttribute : TypeFilterAttribute
{
    public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter))
    {
    }
}

