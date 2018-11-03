namespace GestaoClinicaEstetica.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCodigoServicoAgenda : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AGENDA", "COD_SERVICO", c => c.Int());
            CreateIndex("dbo.AGENDA", "COD_SERVICO");
            AddForeignKey("dbo.AGENDA", "COD_SERVICO", "dbo.SERVICO", "ID", cascadeDelete: true);
            DropColumn("dbo.AGENDA", "TIP_CONSULTA");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AGENDA", "TIP_CONSULTA", c => c.Int(nullable: false));
            DropForeignKey("dbo.AGENDA", "COD_SERVICO", "dbo.SERVICO");
            DropIndex("dbo.AGENDA", new[] { "COD_SERVICO" });
            DropColumn("dbo.AGENDA", "COD_SERVICO");
        }
    }
}
