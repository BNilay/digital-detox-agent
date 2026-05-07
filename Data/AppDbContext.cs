using ai.Models;
using Microsoft.EntityFrameworkCore;



namespace ai.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options): base(options) { }


        public DbSet<User> Users { get; set; }
        public DbSet<UsageRecord> UsageRecords { get; set; }
        public DbSet<Plan> Plans { get; set; }
    }
}
