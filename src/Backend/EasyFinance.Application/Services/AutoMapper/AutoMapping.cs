using AutoMapper;
using EasyFinance.Communication.Request;

namespace EasyFinance.Application.Services.AutoMapper;
public class AutoMapping : Profile
{

    public AutoMapping()
    {
        RequestToDomain();
    }

    private void RequestToDomain()
    {
        CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
            .ForMember(opt => opt.Password, config => config.Ignore());
    }
}

