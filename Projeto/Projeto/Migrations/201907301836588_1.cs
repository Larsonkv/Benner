namespace Projeto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuarios", "Senha", c => c.String(nullable: false, maxLength: 250));
            CreateIndex("dbo.Usuarios", "Senha", unique: true, name: "Usuario_Senha_Index");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Usuarios", "Usuario_Senha_Index");
            DropColumn("dbo.Usuarios", "Senha");
        }
    }
}
