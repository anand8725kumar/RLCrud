using Assignment.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Assignment.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext>options) :base(options)
        {

        }
        public DbSet<Registration> Tasks1 { get; set; }
        public DbSet<Crud>Task2 { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the AccessToken property to have a default value
            modelBuilder.Entity<Registration>()
                .Property(u => u.AccessToken)
                .HasDefaultValue("default_value");
            modelBuilder.Entity<Registration>()
                .Property(x => x.UserMessage)
                .HasDefaultValue("default_value");

            // Other model configurations go here

            base.OnModelCreating(modelBuilder);
        }
    }
}
