namespace SoulProject.Application.Security;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class AuthorizeAttribute : Attribute
{
    public Role? Roles { get; set; }
    
    public AuthorizeAttribute() { }

    public AuthorizeAttribute(Role role) : this()
    {
        Roles = role;
    }
}