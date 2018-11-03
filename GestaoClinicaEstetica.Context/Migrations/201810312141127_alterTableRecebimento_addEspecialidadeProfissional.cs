namespace GestaoClinicaEstetica.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterTableRecebimento_addEspecialidadeProfissional : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RECEBIMENTOS", "COD_ESPECIALIDADE", c => c.Int(nullable: false));
            AddColumn("dbo.RECEBIMENTOS", "COD_PROFISSIONAL", c => c.Int(nullable: false));
            CreateIndex("dbo.RECEBIMENTOS", "COD_ESPECIALIDADE");
            CreateIndex("dbo.RECEBIMENTOS", "COD_PROFISSIONAL");
            AddForeignKey("dbo.RECEBIMENTOS", "COD_ESPECIALIDADE", "dbo.ESPECIALIDADE", "ID", cascadeDelete: true);
            AddForeignKey("dbo.RECEBIMENTOS", "COD_PROFISSIONAL", "dbo.PROFISSIONAL", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RECEBIMENTOS", "COD_PROFISSIONAL", "dbo.PROFISSIONAL");
            DropForeignKey("dbo.RECEBIMENTOS", "COD_ESPECIALIDADE", "dbo.ESPECIALIDADE");
            DropIndex("dbo.RECEBIMENTOS", new[] { "COD_PROFISSIONAL" });
            DropIndex("dbo.RECEBIMENTOS", new[] { "COD_ESPECIALIDADE" });
            DropColumn("dbo.RECEBIMENTOS", "COD_PROFISSIONAL");
            DropColumn("dbo.RECEBIMENTOS", "COD_ESPECIALIDADE");
        }
    }
}
