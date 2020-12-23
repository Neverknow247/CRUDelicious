using Microsoft.EntityFrameworkCore;
namespace CRUDelicious.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options) {}
        public DbSet<Dishes> Dishes {get;set;}
    }
}