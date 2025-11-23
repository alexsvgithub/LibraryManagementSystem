using Swashbuckle.AspNetCore.Annotations;

public class Book
{
    [SwaggerSchema(ReadOnly = true)]
    public int? Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int NoOfCopiesAvailable{ get; set; }
}