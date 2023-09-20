using Microsoft.Extensions.Options;

namespace Singulare.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=host.docker.internal,1433;Database=quote_db;User ID=sa;Password=A&VeryComplex123Password;Trusted_Connection=False; TrustServerCertificate=True;");
        }

        public DbSet<Quote> Quotes{ get; set; }
        public DbSet<ProcessReports> ProcessReports { get; set; }

    }
}
