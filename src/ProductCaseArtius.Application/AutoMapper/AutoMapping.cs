using AutoMapper;
using ProductCaseArtius.Communication.Requests;
using ProductCaseArtius.Communication.Responses;
using ProductCaseArtius.Domain.Entities;

namespace ProductCaseArtius.Application.AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        // Mapeamento para Product
        CreateMap<RequestProductJson, Product>();
        
        // Mapeamento para User
        CreateMap<RequestRegisterUserJson, User>()
            .ForMember(dest => dest.Password, config => config.Ignore());
    }

    private void EntityToResponse()
    {
        // Mapeamento para Product
        CreateMap<Product, ResponseProductJson>();
    }
}
