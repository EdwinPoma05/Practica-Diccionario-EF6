namespace Diccionario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreacionTablaRepuesto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Repuestoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.String(),
                        Nombre = c.String(),
                        Stock = c.Int(nullable: false),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Repuestoes");
        }
    }
}
