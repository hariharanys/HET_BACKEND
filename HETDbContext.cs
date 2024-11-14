using HET_BACKEND.EntityModel;
using Microsoft.EntityFrameworkCore;

namespace HET_BACKEND
{
    public class HETDbContext:DbContext
    {
        public HETDbContext(DbContextOptions<HETDbContext> options) : base(options) { }

        public DbSet<AuthEntityModel> authModels { get; set; }
        public DbSet<UserEntityModel> Users { get; set; }
        public DbSet<UserDetailsEntityModel> UserDetails { get; set; }
        public DbSet<ExpenseEntityModel> ExpenseDetails { get; set; }
    }
}
