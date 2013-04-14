using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Notesapp.Models
{
    public class NotesappContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<Notesapp.Models.NotesappContext>());

        public NotesappContext() : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NotesappContext, NotesappMigrationConfiguration>());//Database is configured to use migrations
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Group> Groups { get; set; }
        public DbSet<Note> Notes { get; set; }
    }

    
    /// <summary>
    /// Migration configuration for using migrations
    /// </summary>
    public class NotesappMigrationConfiguration : DbMigrationsConfiguration<NotesappContext>
    {
        public NotesappMigrationConfiguration()
        {
            AutomaticMigrationsEnabled = true;//Enabled automatic migrations
            AutomaticMigrationDataLossAllowed = true;
        }
    }

}
