namespace GestaoClinicaEstetica.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterarCorEventosClinica : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DADOS_CLINICA", "COR_EVENTO_ANIVERSARIANTE", c => c.String(unicode: false));
            AddColumn("dbo.DADOS_CLINICA", "COR_EVENTO_RECEBIMENTOS", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DADOS_CLINICA", "COR_EVENTO_RECEBIMENTOS");
            DropColumn("dbo.DADOS_CLINICA", "COR_EVENTO_ANIVERSARIANTE");
        }
    }
}
