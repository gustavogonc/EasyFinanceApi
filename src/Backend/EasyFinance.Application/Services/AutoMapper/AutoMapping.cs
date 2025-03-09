using AutoMapper;
using EasyFinance.Communication.Request;
using EasyFinance.Communication.Response;

namespace EasyFinance.Application.Services.AutoMapper;
public class AutoMapping : Profile
{

    public AutoMapping()
    {
        RequestToDomain();
        DomainToResponse();
    }
    private void RequestToDomain()
    {
        CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
            .ForMember(opt => opt.Password, config => config.Ignore());
    }
    private void DomainToResponse()
    {
        CreateMap<Domain.Entities.User, ResponseRegisteredUserJson>();
    }
}

