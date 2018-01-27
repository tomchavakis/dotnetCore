using Microsoft.EntityFrameworkCore;


/* Entity Framework Notes
 Scaffold a migration and create the initial set of tables for the model.
 #### $dotnet ef migrations add InitialCreate #####
 Apply the new migration to the database. This command creates the database before applying migrations
 #### $dotnet ef database update ##### */

namespace WebApp.Model
{

    public class DatabaseContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }
    }


}