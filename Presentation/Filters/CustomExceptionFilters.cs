using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.Filters;

public class CustomExceptionFilters : IExceptionFilter
{

    private readonly ILogger<CustomExceptionFilters> _logger;

    public CustomExceptionFilters(ILogger<CustomExceptionFilters> logger)
    {
        _logger = logger;
    }
    
    
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is DomainException domainException)
        {
            context.Result = new BadRequestObjectResult(new { error = domainException.Message });
            context.ExceptionHandled = true;
        }
        else
        {
            _logger.LogError(context.Exception, "Erro não tratado.");

            context.Result = new ObjectResult(new { error = "Ocorreu um erro interno no servidor." })
            {
                StatusCode = 500
            };
            context.ExceptionHandled = true;
        }
    }
}