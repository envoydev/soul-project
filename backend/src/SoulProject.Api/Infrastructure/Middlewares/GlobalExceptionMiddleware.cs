using System.Net;
using System.Text;
using SoulProject.Api.Constants;

namespace SoulProject.Api.Infrastructure.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IJsonSerializerService _jsonSerializerService;
    
    private readonly Dictionary<Type, Func<HttpContext, Exception, CancellationToken, Task>> _exceptionHandlers;

    public GlobalExceptionMiddleware(
        RequestDelegate next, 
        IJsonSerializerService jsonSerializerService
        )
    {
        _next = next;
        _jsonSerializerService = jsonSerializerService;

        _exceptionHandlers = GetExceptionHandlers();
    }

    // ReSharper disable once UnusedMember.Global
    public async Task Invoke(HttpContext httpContext, IApplicationLogger<GlobalExceptionMiddleware> logger)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Middleware caught an error. Exception message: {Message}", exception.Message);
            
            var exceptionType = exception.GetType();

            if (_exceptionHandlers.TryGetValue(exceptionType, out var handler))
            {
                await handler.Invoke(httpContext, exception, CancellationToken.None);
            }
            else
            {
                var requestError = new RequestError((int)HttpStatusCode.InternalServerError, exception.Message);
        
                await WriteResponseAsync(httpContext, requestError, CancellationToken.None);
            }
        }
    }

    private Dictionary<Type, Func<HttpContext, Exception, CancellationToken, Task>> GetExceptionHandlers()
    {
        return new Dictionary<Type, Func<HttpContext, Exception, CancellationToken, Task>>
        {
            { typeof(BadRequestException), HandleValidationException },
            { typeof(NotFoundException), HandleNotFoundException },
            { typeof(UnauthorizedException), HandleUnauthorizedAccessException },
            { typeof(ForbiddenException), HandleForbiddenAccessException }
        };
    }
    
    private async Task HandleValidationException(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var badRequestException = (BadRequestException)exception;
        
        var requestError = new RequestError((int)HttpStatusCode.BadRequest, badRequestException.Message, badRequestException.Errors);
        
        await WriteResponseAsync(httpContext, requestError, cancellationToken);
    }

    private async Task HandleNotFoundException(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var notFoundException = (NotFoundException)exception;
        
        var requestError = new RequestError((int)HttpStatusCode.NotFound, notFoundException.Message);
        
        await WriteResponseAsync(httpContext, requestError, cancellationToken);
    }

    private async Task HandleUnauthorizedAccessException(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var unAuthorizedException = (UnauthorizedException)exception;
        
        var requestError = new RequestError((int)HttpStatusCode.Unauthorized, unAuthorizedException.Message);
        
        await WriteResponseAsync(httpContext, requestError, cancellationToken);
    }
    
    private async Task HandleForbiddenAccessException(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var forbiddenException = (ForbiddenException)exception;
        
        var requestError = new RequestError((int)HttpStatusCode.Forbidden, forbiddenException.Message);

        await WriteResponseAsync(httpContext, requestError, cancellationToken);
    }

    private async Task WriteResponseAsync(HttpContext httpContext, RequestError requestError, CancellationToken cancellationToken)
    {
        var json = _jsonSerializerService.SerializeObject(requestError);

        using var body = new MemoryStream();

        var buffer = Encoding.UTF8.GetBytes(json);
        body.Write(buffer, 0, buffer.Length);
        body.Position = 0;

        httpContext.Response.Headers.Append(HttpItemsConstants.ContentTypeHeader, HttpItemsConstants.ContentTypeValue);
        httpContext.Response.StatusCode = requestError.StatusCode;

        await body.CopyToAsync(httpContext.Response.Body, cancellationToken);
    }
}