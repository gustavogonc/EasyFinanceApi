namespace EasyFinance.Domain.Entities;
public class User : EntityBase
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public Guid UserIdentifier { get; set; } = Guid.NewGuid();
}

