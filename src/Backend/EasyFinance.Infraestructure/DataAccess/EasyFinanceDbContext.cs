using EasyFinance.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyFinance.Infraestructure.DataAccess;
public class EasyFinanceDbContext : DbContext
{
    public EasyFinanceDbContext(DbContextOptions<EasyFinanceDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
}

