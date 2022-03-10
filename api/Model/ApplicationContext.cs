using Microsoft.EntityFrameworkCore;

namespace api.Model
{
    public class ApplicationContext : DbContext
    {
        public virtual DbSet<GitRepo> Repositories { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }
    }

}