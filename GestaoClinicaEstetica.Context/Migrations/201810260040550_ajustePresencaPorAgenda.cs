namespace GestaoClinicaEstetica.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ajustePresencaPorAgenda : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("AGENDA", "ID", "PRESENCA_POR_CLIENTE");
            //DropForeignKey("ATENDIMENTO", "COD_AGENDA", "AGENDA");
            //DropIndex("AGENDA", new[] { "ID" });
            //DropPrimaryKey("AGENDA");
            //AlterColumn("AGENDA", "ID", c => c.Int(nullable: false, identity: true));
            //AddPrimaryKey("AGENDA", "ID");
            //CreateIndex("PRESENCA_POR_CLIENTE", "COD_AGENDA");
            //AddForeignKey("PRESENCA_POR_CLIENTE", "COD_AGENDA", "AGENDA", "ID", cascadeDelete: true);
            //AddForeignKey("ATENDIMENTO", "COD_AGENDA", "AGENDA", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ATENDIMENTO", "COD_AGENDA", "dbo.AGENDA");
            DropForeignKey("dbo.PRESENCA_POR_CLIENTE", "COD_AGENDA", "dbo.AGENDA");
            DropIndex("dbo.PRESENCA_POR_CLIENTE", new[] { "COD_AGENDA" });
            DropPrimaryKey("dbo.AGENDA");
            AlterColumn("dbo.AGENDA", "ID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.AGENDA", "ID");
            CreateIndex("dbo.AGENDA", "ID");
            AddForeignKey("dbo.ATENDIMENTO", "COD_AGENDA", "dbo.AGENDA", "ID", cascadeDelete: true);
            AddForeignKey("dbo.AGENDA", "ID", "dbo.PRESENCA_POR_CLIENTE", "ID", cascadeDelete: true);
        }
    }
}
