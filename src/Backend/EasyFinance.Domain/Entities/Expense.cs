using EasyFinance.Domain.Enum;

namespace EasyFinance.Domain.Entities;
public class Expense : EntityBase
{
    public string Title { get; set; }
    public DateOnly Date { get; set; }
    public int StartMonth { get; set; }
    public int StartYear { get; set; }
    public ExpenseType Type { get; set; }
    public Category Category { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public int? Months { get; set; } = 1;
    public decimal Value { get; set; }
    public bool IsRecurrent { get; set; } = false;
    public bool IsRecurrentActive { get; set; } = false;
    public User User { get; set; } = default!;
    public long UserId { get; set; }
}

