using Microsoft.EntityFrameworkCore;

namespace TestProject.DataAccess
{
    public class CategoriesManagementContext: DbContext
    {
        public CategoriesManagementContext(DbContextOptions<CategoriesManagementContext> options)
           : base(options)
        { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Token> Tokens { get; set; }
    }
}
