using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace SoulProject.Application.CQRS.Authentication.Login;

// ReSharper disable once UnusedType.Global
public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IPasswordService _passwordService;
    
    public LoginCommandValidator(
        IApplicationDbContext applicationDbContext, 
        IPasswordService passwordService
        )
    {
        _applicationDbContext = applicationDbContext;
        _passwordService = passwordService;

        RuleFor(v => v.UserName)
            .NotEmpty();
        
        RuleFor(v => v.Password)
            .NotEmpty()
            .MustAsync(CheckPasswordAsync)
            .WithMessage("Wrong password or user name");
    }

    private async Task<bool> CheckPasswordAsync(LoginCommand loginCommand, string password, CancellationToken cancellationToken)
    {
        var currentUser = await _applicationDbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserName == loginCommand.UserName,
            cancellationToken: cancellationToken);

        if (currentUser == null)
        {
            return false;
        }

        var isPasswordVerified = _passwordService.Verify(password, currentUser.PasswordHash);
        
        return isPasswordVerified;
    }
}