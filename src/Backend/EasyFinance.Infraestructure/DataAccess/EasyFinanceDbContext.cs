using EasyFinance.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyFinance.Infraestructure.DataAccess;
public class EasyFinanceDbContext(DbContextOptions<EasyFinanceDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Expense> Expenses { get; set; }
}

