using Example.Business.Models.Entities;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Example.Data.Context
{
    public class StuffContext : DbContext
    {
        public StuffContext() : base("name=Stuff")
        {
            Database.SetInitializer<StuffContext>(null);
        }

        public DbSet<Stuff> StuffSet { get; set; }
        public DbSet<Token> TokenSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Ignore<PropertyChangedEventHandler>();

            modelBuilder.Entity<Stuff>().HasKey(e => e.StuffId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Token>().HasKey(e => e.TokenId).Ignore(e => e.EntityId);
        }
    }
}
