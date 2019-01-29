namespace GestaoClinicaEstetica.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterTableEspecialidade_obrigatorios : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ESPECIALIDADE", "DESCRICAO", c => c.String(nullable: false, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ESPECIALIDADE", "DESCRICAO", c => c.String(unicode: false));
        }
    }
}
