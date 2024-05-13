using Mapster;
using SoulProject.Application.CQRS.Users.Dtos;

namespace SoulProject.Application.CQRS.Users.Mappings;

public class UserDtoMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserDto>()
              .Map(dto => dto.FullName, src => GetFullName(src));
    }

    private static string? GetFullName(User user)
    {
        var isFirstName = !string.IsNullOrWhiteSpace(user.FirstName);
        var isLastName = !string.IsNullOrWhiteSpace(user.LastName);
        string? fullName = null;
        
        if (isFirstName)
        {
            fullName = user.FirstName + (isLastName ? " " : string.Empty);
        }

        if (isLastName)
        {
            fullName = fullName != null ? fullName + user.LastName : user.LastName;
        }

        return fullName;
    }
}