using DoAn1.Areas.Identity.Data;
using DoAn1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DoAn1.Data;

public class DbContext : IdentityDbContext<AppUser>
{
    public DbContext(DbContextOptions<DbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Budget> Budgets { get; set; }
    public DbSet<Income> Incomes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Tiền điện" , Icon = "⚡" },
            new Category { Id = 2, Name = "Ăn uống", Icon = "🍽️" },
            new Category { Id = 3, Name = "Di chuyển", Icon = "🚗" },
            new Category { Id = 4, Name = "Tiền điện thoại", Icon = "📱" },
            new Category { Id = 5, Name = "Tiền nước" , Icon = "💧" },
            new Category { Id = 6, Name = "Học phí" , Icon = "🏫" },
            new Category { Id = 7, Name = "Tiết kiệm" , Icon = "🏦" }
            );
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
