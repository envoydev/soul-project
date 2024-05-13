using MediatR;
using SoulProject.Application.CQRS.Users.Dtos;

namespace SoulProject.Application.CQRS.Users.Queries.GetAllUsers;

[Authorize(Role.Admin)]
public class GetAllUsersQuery : IRequest<List<UserDto>>
{
}