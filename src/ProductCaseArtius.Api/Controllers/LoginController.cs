using Microsoft.AspNetCore.Mvc;
using ProductCaseArtius.Application.UseCases.Login.DoLogin;
using ProductCaseArtius.Communication.Requests;
using ProductCaseArtius.Communication.Responses;

namespace ProductCaseArtius.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly IDoLoginUseCase _useCase;

    public LoginController(IDoLoginUseCase useCase)
    {
        _useCase = useCase;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] RequestLoginJson request)
    {
        var response = await _useCase.Execute(request);

        return Ok(response);
    }
}
