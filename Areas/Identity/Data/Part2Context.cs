using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Part2.Areas.Identity.Data;
using Part2.Models;

namespace Part2.Data;

public class Part2Context : IdentityDbContext<ApplicationUser>
{
    public Part2Context(DbContextOptions<Part2Context> options)
        : base(options)
    {
    }
    public DbSet<Craft> Crafts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<ProcessedOrder> ProcessedOrders { get; set; }
    public DbSet<OrderHistory> OrderHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

public DbSet<Part2.Models.OrderHistory> OrderHistory { get; set; } = default!;
}
