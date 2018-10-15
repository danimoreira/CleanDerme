namespace GestaoClinicaEstetica.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterTableClientevalidations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CLIENTE", "CPF", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.CLIENTE", "NOME", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.CLIENTE", "TEL_CELULAR", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.CLIENTE", "EMAIL", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.PROFISSIONAL", "CPF", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.PROFISSIONAL", "NOME", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.PROFISSIONAL", "TEL_CELULAR", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.PROFISSIONAL", "EMAIL", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.DADOS_CLINICA", "NOME", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.DADOS_CLINICA", "TEL_CELULAR", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.DADOS_CLINICA", "EMAIL", c => c.String(nullable: false, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DADOS_CLINICA", "EMAIL", c => c.String(unicode: false));
            AlterColumn("dbo.DADOS_CLINICA", "TEL_CELULAR", c => c.String(unicode: false));
            AlterColumn("dbo.DADOS_CLINICA", "NOME", c => c.String(unicode: false));
            AlterColumn("dbo.PROFISSIONAL", "EMAIL", c => c.String(unicode: false));
            AlterColumn("dbo.PROFISSIONAL", "TEL_CELULAR", c => c.String(unicode: false));
            AlterColumn("dbo.PROFISSIONAL", "NOME", c => c.String(unicode: false));
            AlterColumn("dbo.PROFISSIONAL", "CPF", c => c.String(unicode: false));
            AlterColumn("dbo.CLIENTE", "EMAIL", c => c.String(unicode: false));
            AlterColumn("dbo.CLIENTE", "TEL_CELULAR", c => c.String(unicode: false));
            AlterColumn("dbo.CLIENTE", "NOME", c => c.String(unicode: false));
            AlterColumn("dbo.CLIENTE", "CPF", c => c.String(unicode: false));
        }
    }
}
