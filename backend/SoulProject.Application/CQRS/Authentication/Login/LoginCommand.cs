using MediatR;

namespace SoulProject.Application.CQRS.Authentication.Login;

public record LoginCommand(string UserName, string Password) : IRequest<LoginCommandResponse>;