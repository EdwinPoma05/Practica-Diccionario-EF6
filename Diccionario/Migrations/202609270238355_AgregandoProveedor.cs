namespace Diccionario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregandoProveedor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Proveedores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Repuestos", "ProveedorId", c => c.Int());
            CreateIndex("dbo.Repuestos", "ProveedorId");
            AddForeignKey("dbo.Repuestos", "ProveedorId", "dbo.Proveedores", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Repuestos", "ProveedorId", "dbo.Proveedores");
            DropIndex("dbo.Repuestos", new[] { "ProveedorId" });
            DropColumn("dbo.Repuestos", "ProveedorId");
            DropTable("dbo.Proveedores");
        }
    }
}
