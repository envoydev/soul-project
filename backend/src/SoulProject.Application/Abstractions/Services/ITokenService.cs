namespace SoulProject.Application.Abstractions.Services;

public interface ITokenService
{
    string GenerateAccessToken(Guid userId, Role role);
    JwtRefreshToken GenerateRefreshToken();
    JwtAccessToken? ParseAccessToken(string accessToken);
}