using ProductCaseArtius.Communication.Responses;
using ProductCaseArtius.Exception;
using ProductCaseArtius.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProductCaseArtius.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is ProductCaseArtiusException)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowUnkowError(context);
        }
    }

    private void HandleProjectException(ExceptionContext context)
    {
        var ProductCaseArtiusException = (ProductCaseArtiusException)context.Exception;
        var errorResponse = new ResponseErrorJson(ProductCaseArtiusException.GetErrors());

        context.HttpContext.Response.StatusCode = ProductCaseArtiusException.StatusCode;
        context.Result = new ObjectResult(errorResponse);
    }

    private void ThrowUnkowError(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(ResourceErrorMessages.UNKNOWN_ERROR);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
