using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SoulProject.Application.CQRS.Authentication.Login;

// ReSharper disable once UnusedType.Global
public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ITokenService _tokenService;

    public LoginCommandHandler(
        IApplicationDbContext applicationDbContext,
        ITokenService tokenService
        )
    {
        _applicationDbContext = applicationDbContext;
        _tokenService = tokenService;
    }

    public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var currentUser = await _applicationDbContext.Users.FirstAsync(x => x.UserName == request.UserName, 
            cancellationToken: cancellationToken);
        
        var accessToken = _tokenService.GenerateAccessToken(currentUser.Id, currentUser.Role);
        var refreshToken = _tokenService.GenerateRefreshToken();

        currentUser.RefreshToken = refreshToken.Token;
        currentUser.RefreshTokenExpiration = refreshToken.ExpiredAt;

        await _applicationDbContext.SaveChangesAsync();

        return new LoginCommandResponse(accessToken, refreshToken.Token);
    }
}