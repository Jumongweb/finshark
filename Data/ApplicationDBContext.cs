

using Microsoft.EntityFrameworkCore;

namespace finshark_api.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<models.Stock> Stocks { get; set; }
        public DbSet<models.Comment> Comments { get; set; }
        
    }
}