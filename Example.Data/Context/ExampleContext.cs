using Example.Business.Models.Entities;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Example.Data.Context
{
    public class ExampleContext : DbContext
    {
        public ExampleContext() : base("name=Stuff")
        {
            Database.SetInitializer<ExampleContext>(null);
        }

        public DbSet<Stuff> StuffSet { get; set; }
        public DbSet<Colour> ColourSet { get; set; }
        public DbSet<Category> CategorySet { get; set; }
        public DbSet<Token> TokenSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Ignore<PropertyChangedEventHandler>();

            // Standard tables
            modelBuilder.Entity<Token>().HasKey(e => e.TokenId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Stuff>().HasKey(e => e.StuffId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Colour>().HasKey(e => e.ColourId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Category>().HasKey(e => e.CategoryId).Ignore(e => e.EntityId);

            // Add one 2 many relationships
            modelBuilder.Entity<Stuff>()
                        .HasOptional(s => s.Colour)
                        .WithMany()
                        .HasForeignKey(s => s.ColourId);

            // Add many 2 many relationships
            modelBuilder.Entity<Stuff>()
                        .HasMany(s => s.Categories)
                        .WithMany()
                        .Map(x => {
                            x.MapLeftKey("StuffId");
                            x.MapRightKey("CategoryId");
                            x.ToTable("StuffCategory");
                        });
        }
    }
}
