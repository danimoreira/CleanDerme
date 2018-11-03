namespace GestaoClinicaEstetica.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterTableRecebimento_addFKs : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.RECEBIMENTOS", "COD_CLIENTE");
            CreateIndex("dbo.RECEBIMENTOS", "COD_SERVICO");
            AddForeignKey("dbo.RECEBIMENTOS", "COD_CLIENTE", "dbo.CLIENTE", "ID", cascadeDelete: true);
            AddForeignKey("dbo.RECEBIMENTOS", "COD_SERVICO", "dbo.SERVICO", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RECEBIMENTOS", "COD_SERVICO", "dbo.SERVICO");
            DropForeignKey("dbo.RECEBIMENTOS", "COD_CLIENTE", "dbo.CLIENTE");
            DropIndex("dbo.RECEBIMENTOS", new[] { "COD_SERVICO" });
            DropIndex("dbo.RECEBIMENTOS", new[] { "COD_CLIENTE" });
        }
    }
}
