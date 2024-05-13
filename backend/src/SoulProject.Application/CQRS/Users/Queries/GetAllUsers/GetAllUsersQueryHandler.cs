using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoulProject.Application.CQRS.Users.Dtos;

namespace SoulProject.Application.CQRS.Users.Queries.GetAllUsers;

// ReSharper disable once UnusedType.Global
public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;
    
    public GetAllUsersQueryHandler(
        IApplicationDbContext applicationDbContext, 
        IMapper mapper
        )
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }
    
    public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _applicationDbContext.Users.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);

        var usersResponse = _mapper.Map<List<UserDto>>(users);
        
        return usersResponse;
    }
}