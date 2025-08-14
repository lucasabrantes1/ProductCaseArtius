using ProductCaseArtius.Communication.Requests;
using ProductCaseArtius.Communication.Responses;

namespace ProductCaseArtius.Application.UseCases.Login.DoLogin;

public interface IDoLoginUseCase
{
    Task<ResponseLoginJson> Execute(RequestLoginJson request);
}
