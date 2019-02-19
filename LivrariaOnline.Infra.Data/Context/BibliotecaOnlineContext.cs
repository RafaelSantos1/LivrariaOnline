using LivrariaOnline.Domain.Entities;
using LivrariaOnline.Infra.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace LivrariaOnline.Infra.Data.Context
{
    public class BibliotecaOnlineContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public BibliotecaOnlineContext()
        {

        }
        public BibliotecaOnlineContext(DbContextOptions<BibliotecaOnlineContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookMap());            
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // get the configuration from the app settings
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // define the database to use
            optionsBuilder.
                UseInMemoryDatabase
                (config.GetConnectionString("DefaultConnection"));
        }
    }
}
