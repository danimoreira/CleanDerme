namespace GestaoClinicaEstetica.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTableClinica_alternameCNPJ : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DADOS_CLINICA", "CPF");
            AddColumn("dbo.DADOS_CLINICA", "CNPJ", c => c.String(nullable: false, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DADOS_CLINICA", "CNPJ");
            AddColumn("dbo.DADOS_CLINICA", "CPF", c => c.String(nullable: false, unicode: false));
        }
    }
}
