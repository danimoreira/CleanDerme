namespace GestaoClinicaEstetica.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterServico_addObrigatorios : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SERVICO", "DESCRICAO", c => c.String(nullable: false, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SERVICO", "DESCRICAO", c => c.String(unicode: false));
        }
    }
}
