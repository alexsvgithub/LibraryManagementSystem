using Swashbuckle.AspNetCore.Annotations;

public class Member
{
    [SwaggerSchema(ReadOnly = true)]
    public string? Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; }
    public bool isActive { get; set; }
}