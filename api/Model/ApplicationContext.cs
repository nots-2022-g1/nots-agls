using Microsoft.EntityFrameworkCore;

namespace api.Model
{
    public class ApplicationContext : DbContext
    {
        public virtual DbSet<Commit> Commits { get; set; }
        public virtual DbSet<Label> Labels { get; set; }
        public virtual DbSet<Repository> Repositories { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }
    }

}