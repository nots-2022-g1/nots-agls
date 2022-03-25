using Microsoft.EntityFrameworkCore;

namespace api.Model
{
    public class ApplicationContext : DbContext
    {
        public virtual DbSet<GitRepo> Repositories { get; set; }
        public virtual DbSet<GitCommit> Commits { get; set; }
        public virtual DbSet<LabeledData> LabeledData { get; set; }
        public virtual DbSet<DataSet> DataSets { get; set; }
        public virtual DbSet<Keyword> Keywords { get; set; }

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

            modelBuilder.Entity<LabeledData>(e =>
            {
                e.HasOne(labeledData => labeledData.GitCommit)
                    .WithMany()
                    .HasForeignKey(labeledData => labeledData.GitCommitHash)
                    .OnDelete(DeleteBehavior.NoAction);
                e.HasOne(labeledData => labeledData.DataSet)
                    .WithMany()
                    .HasForeignKey(labeledData => labeledData.DataSetId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }

}