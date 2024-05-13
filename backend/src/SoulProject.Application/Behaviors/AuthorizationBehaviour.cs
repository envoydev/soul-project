using System.Reflection;
using Ardalis.GuardClauses;
using MediatR;

namespace SoulProject.Application.Behaviors;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ISessionService _sessionService;
    private readonly IDateTimeService _dateTimeService;

    public AuthorizationBehaviour(
        ISessionService sessionService, 
        IDateTimeService dateTimeService
        )
    {
        _sessionService = sessionService;
        _dateTimeService = dateTimeService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authorizeAttributes = request.GetType()
                                         .GetCustomAttributes<AuthorizeAttribute>()
                                         .ToList()
                                         .AsReadOnly();
        
        if (authorizeAttributes.Count == 0)
        {
            return await next();
        }
        
        if (!_sessionService.UserId.HasValue || !_sessionService.UserRole.HasValue || !_sessionService.TokenExpiration.HasValue)
        {
            throw new UnauthorizedException("User is not authorized.");
        }
        
        if (_sessionService.TokenExpiration < _dateTimeService.GetUtc())
        {
            throw new UnauthorizedException("Access token is expired.");
        }
        
        var isUserRolePresentInAttribute = authorizeAttributes.Where(x => x.Roles.HasValue)
                                                              .Select(x => Guard.Against.Null(x.Roles))
                                                              .Any(role => _sessionService.UserRole.Value >= role);
        
        if (!isUserRolePresentInAttribute)
        {
            throw new ForbiddenException("Access to the resource is forbidden.");
        }
        
        return await next();
    }
}