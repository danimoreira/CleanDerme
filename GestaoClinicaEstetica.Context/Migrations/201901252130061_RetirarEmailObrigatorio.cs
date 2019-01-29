namespace GestaoClinicaEstetica.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RetirarEmailObrigatorio : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CLIENTE", "EMAIL", c => c.String(unicode: false));
            AlterColumn("dbo.PROFISSIONAL", "EMAIL", c => c.String(unicode: false));
            AlterColumn("dbo.DADOS_CLINICA", "EMAIL", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DADOS_CLINICA", "EMAIL", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.PROFISSIONAL", "EMAIL", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.CLIENTE", "EMAIL", c => c.String(nullable: false, unicode: false));
        }
    }
}
