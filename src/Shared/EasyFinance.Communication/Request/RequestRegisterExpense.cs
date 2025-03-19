﻿using EasyFinance.Communication.Enum;

namespace EasyFinance.Communication.Request;
public class RequestRegisterExpense
{
    public string Title { get; set; }
    public DateOnly Date { get; set; }
    public ExpenseType Type { get; set; }
    public Category Category { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public int? Months { get; set; } = 1;
    public bool? IsRecurrent { get; set; }
    public bool? IsRecurrentActive { get; set; }
    public decimal Value { get; set; }
}

