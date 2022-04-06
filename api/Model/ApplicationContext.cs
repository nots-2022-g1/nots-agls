using Microsoft.EntityFrameworkCore;

namespace api.Model
{
    public class ApplicationContext : DbContext
    {
        public virtual DbSet<GitRepo> Repositories { get; set; }
        public virtual DbSet<GitCommit> Commits { get; set; }
        public virtual DbSet<LabeledData> LabeledData { get; set; }
        public virtual DbSet<Dataset> Datasets { get; set; }
        public virtual DbSet<Keyword> Keywords { get; set; }
        public virtual DbSet<KeywordSet> KeywordSets { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
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

            modelBuilder.Entity<LabeledData>(e =>
            {
                e.HasOne(labeledData => labeledData.GitCommit)
                    .WithMany()
                    .HasForeignKey(labeledData => labeledData.GitCommitHash)
                    .OnDelete(DeleteBehavior.NoAction);
                e.HasOne(labeledData => labeledData.Dataset)
                    .WithMany()
                    .HasForeignKey(labeledData => labeledData.DatasetId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Keyword>(e =>
            {
                e.HasOne(keyword => keyword.KeywordSet)
                    .WithMany()
                    .HasForeignKey(keyword => keyword.KeywordSetId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }

}