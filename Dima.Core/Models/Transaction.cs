namespace Dima.Core.Models;

public class Transaction
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? PaidOrReceveidAt { get; set; }

    public int Type { get; set; }
    public decimal Amount { get; set; }

    public long CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public string UserId { get; set; } = string.Empty;
}
