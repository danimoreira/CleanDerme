namespace GestaoClinicaEstetica.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropTable_EspecialidadePorServico : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SERVICO", "CodigoEspecialidade", c => c.Int(nullable: false));
            AddColumn("dbo.SERVICO", "QTD_SESSOES", c => c.Int(nullable: false));
            CreateIndex("dbo.SERVICO", "CodigoEspecialidade");
            AddForeignKey("dbo.SERVICO", "CodigoEspecialidade", "dbo.ESPECIALIDADE", "ID", cascadeDelete: true);            
        }
        
        public override void Down()
        {   DropForeignKey("dbo.SERVICO", "CodigoEspecialidade", "dbo.ESPECIALIDADE");
            DropIndex("dbo.SERVICO", new[] { "CodigoEspecialidade" });
            DropColumn("dbo.SERVICO", "QTD_SESSOES");
            DropColumn("dbo.SERVICO", "CodigoEspecialidade");
         
        }
    }
}
