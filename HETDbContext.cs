using HET_BACKEND.EntityModel;
using Microsoft.EntityFrameworkCore;

namespace HET_BACKEND
{
    public class HETDbContext:DbContext
    {
        public HETDbContext(DbContextOptions<HETDbContext> options) : base(options) { }

        public DbSet<AuthModel> authModels { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
    }
}
