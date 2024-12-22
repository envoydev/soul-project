namespace SoulProject.Application.CQRS.Authentication.Login;

public record LoginCommandResponse(string AccessToken, string RefreshToken);