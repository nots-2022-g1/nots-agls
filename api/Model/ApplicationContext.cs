using Microsoft.EntityFrameworkCore;

namespace api.Model
{
    public class ApplicationContext : DbContext
    {
        public virtual DbSet<Label> Repositories { get; set; }
        public virtual DbSet<Label> Labels { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }
    }

}