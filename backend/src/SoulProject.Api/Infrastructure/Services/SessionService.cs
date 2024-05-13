using System.Security.Claims;

namespace SoulProject.Api.Infrastructure.Services;

internal class SessionService : ISessionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDateTimeService _dateTimeService;
    
    public Guid? UserId => GetUserId();
    public Role? UserRole => GetUserRole();
    public DateTime? TokenExpiration => GetExpirationTime();

    public SessionService(
        IHttpContextAccessor httpContextAccessor, 
        IDateTimeService dateTimeService
        )
    {
        _httpContextAccessor = httpContextAccessor;
        _dateTimeService = dateTimeService;
    }
    
    private Guid? GetUserId()
    {
        var value = _httpContextAccessor.HttpContext?.User.FindFirstValue(TokenConstants.UserId);

        if (value != null && Guid.TryParse(value, out var userId))
        {
            return userId;
        }
            
        return null;
    }

    private Role? GetUserRole()
    {
        var value = _httpContextAccessor.HttpContext?.User.FindFirstValue(TokenConstants.Role);

        if (value != null && Enum.TryParse(value, out Role role))
        {
            return role;
        }
            
        return null;
    }
    
    private DateTime? GetExpirationTime()
    {
        var value = _httpContextAccessor.HttpContext?.User.FindFirstValue(TokenConstants.ExpiredAt);

        if (value != null && int.TryParse(value, out var unixTimeStamp))
        {
            return _dateTimeService.FromUnixTimeStampToDateTime(unixTimeStamp);
        }
            
        return null;
    }
}