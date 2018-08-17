namespace ProyectoTelefonia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Area",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubArea",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Referente = c.String(),
                        Area_id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Area", t => t.Area_id, cascadeDelete: true)
                .Index(t => t.Area_id);
            
            CreateTable(
                "dbo.Directo",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Numero = c.String(nullable: false),
                        Estado = c.String(),
                        NoMostrar = c.Boolean(nullable: false),
                        Observacion = c.String(),
                        SubArea_id = c.Long(nullable: false),
                        Puesto_id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Puesto", t => t.Puesto_id, cascadeDelete: true)
                .ForeignKey("dbo.SubArea", t => t.SubArea_id, cascadeDelete: true)
                .Index(t => t.SubArea_id)
                .Index(t => t.Puesto_id);
            
            CreateTable(
                "dbo.Puesto",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NumeroTipo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Interno",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Numero = c.Long(nullable: false),
                        Tipo = c.String(),
                        Tn = c.String(),
                        PuestoTel = c.String(),
                        Estado = c.String(),
                        NoMostrar = c.Boolean(nullable: false),
                        Observacion = c.String(maxLength: 500),
                        SubArea_id = c.Long(nullable: false),
                        Puesto_id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Puesto", t => t.Puesto_id, cascadeDelete: true)
                .ForeignKey("dbo.SubArea", t => t.SubArea_id, cascadeDelete: true)
                .Index(t => t.SubArea_id)
                .Index(t => t.Puesto_id);
            
            CreateTable(
                "dbo.Piso",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Numero = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PisoSubAreas",
                c => new
                    {
                        Piso_Id = c.Long(nullable: false),
                        SubArea_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Piso_Id, t.SubArea_Id })
                .ForeignKey("dbo.Piso", t => t.Piso_Id, cascadeDelete: true)
                .ForeignKey("dbo.SubArea", t => t.SubArea_Id, cascadeDelete: true)
                .Index(t => t.Piso_Id)
                .Index(t => t.SubArea_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PisoSubAreas", "SubArea_Id", "dbo.SubArea");
            DropForeignKey("dbo.PisoSubAreas", "Piso_Id", "dbo.Piso");
            DropForeignKey("dbo.Directo", "SubArea_id", "dbo.SubArea");
            DropForeignKey("dbo.Directo", "Puesto_id", "dbo.Puesto");
            DropForeignKey("dbo.Interno", "SubArea_id", "dbo.SubArea");
            DropForeignKey("dbo.Interno", "Puesto_id", "dbo.Puesto");
            DropForeignKey("dbo.SubArea", "Area_id", "dbo.Area");
            DropIndex("dbo.PisoSubAreas", new[] { "SubArea_Id" });
            DropIndex("dbo.PisoSubAreas", new[] { "Piso_Id" });
            DropIndex("dbo.Interno", new[] { "Puesto_id" });
            DropIndex("dbo.Interno", new[] { "SubArea_id" });
            DropIndex("dbo.Directo", new[] { "Puesto_id" });
            DropIndex("dbo.Directo", new[] { "SubArea_id" });
            DropIndex("dbo.SubArea", new[] { "Area_id" });
            DropTable("dbo.PisoSubAreas");
            DropTable("dbo.Piso");
            DropTable("dbo.Interno");
            DropTable("dbo.Puesto");
            DropTable("dbo.Directo");
            DropTable("dbo.SubArea");
            DropTable("dbo.Area");
        }
    }
}
