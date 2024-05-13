using Microsoft.EntityFrameworkCore;

namespace SoulProject.Infrastructure.Persistence;

internal class ApplicationDbContextInitializer : IApplicationDbContextInitializer
{
    private readonly IApplicationLogger<ApplicationDbContextInitializer> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IPasswordService _passwordService;

    public ApplicationDbContextInitializer(
        IApplicationLogger<ApplicationDbContextInitializer> logger, 
        ApplicationDbContext context,
        IPasswordService passwordService
        )
    {
        _logger = logger;
        _context = context;
        _passwordService = passwordService;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "An error occurred while initialising the database.");
            
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "An error occurred while seeding the database.");
            
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        if (_context.Users.Any())
        {
            return;
        }

        await _context.Users.AddAsync(new User
        {
            UserName = "admin@soulproject.io",
            Email = "admin@soulproject.io",
            PasswordHash = _passwordService.HashPassword("Soul1234!"),
            Role = Role.Admin
        });

        await _context.SaveChangesAsync();
    }
}