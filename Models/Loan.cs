using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

public class Loan
{
    [Key]
    [SwaggerSchema(ReadOnly = true)]
    public string? Id { get; set; } = Guid.NewGuid().ToString();
    public int BookId { get; set; }
    public string MemberId { get; set; }
    public bool isReturned { get; set; }
    public DateTime BorrowedAt { get; set; }
    public DateTime? ReturnedAt { get; set; }
}