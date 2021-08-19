namespace Projeto.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Projeto.Models.Context>
    {
        public Configuration()
        {
            //Para criar Migration
            //Enable-Migrations -ContextTypeName Context -EnableAutomaticMigrations -Force
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true; // Migration
            ContextKey = "ECommerce.Models.Context";
        }

        protected override void Seed(Projeto.Models.Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
