namespace GestaoClinicaEstetica.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterTableEspecialidade_addPrcRepasseCorEvento : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ESPECIALIDADE", "TIP_COR_EVENTO", c => c.String(maxLength: 10, unicode: false));
            AddColumn("dbo.ESPECIALIDADE", "PRC_REPASSE", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ESPECIALIDADE", "PRC_REPASSE");
            DropColumn("dbo.ESPECIALIDADE", "TIP_COR_EVENTO");
        }
    }
}
