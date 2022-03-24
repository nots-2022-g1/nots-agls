using Microsoft.EntityFrameworkCore;

namespace api.Model
{
    public class ApplicationContext : DbContext
    {
        public virtual DbSet<GitRepo> Repositories { get; set; }
        public virtual DbSet<GitCommit> Commits { get; set; }
        public virtual DbSet<Keyword> Keywords { get; set; }
        public virtual DbSet<Label> Labels { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GitCommit>(e =>
            {
                e.HasOne(gitCommit => gitCommit.GitRepo)
                    .WithMany()
                    .HasForeignKey(gitCommit => gitCommit.GitRepoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }

}