using Microsoft.EntityFrameworkCore;

namespace ValetaxProj.Models
{
    public class MyDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(@"server=localhost;Database=Valetax;trusted_connection=true;TrustServerCertificate=True");
        }

        public DbSet<Node> Nodes { get; set; }
        public DbSet<ExceptionsLog> Exceptions { get; set; }
    }
}
