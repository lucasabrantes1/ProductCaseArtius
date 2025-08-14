using ProductCaseArtius.Communication.Requests;
using ProductCaseArtius.Communication.Responses;

namespace ProductCaseArtius.Application.UseCases.Users.Register;
public interface IRegisterUserUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
}
