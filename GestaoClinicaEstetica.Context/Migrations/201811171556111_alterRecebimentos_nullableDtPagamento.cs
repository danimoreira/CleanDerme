namespace GestaoClinicaEstetica.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterRecebimentos_nullableDtPagamento : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RECEBIMENTOS", "DAT_PAGAMENTO", c => c.DateTime(precision: 0));
            AlterColumn("dbo.RECEBIMENTOS", "VLR_RECEBIDO", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RECEBIMENTOS", "VLR_RECEBIDO", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.RECEBIMENTOS", "DAT_PAGAMENTO", c => c.DateTime(nullable: false, precision: 0));
        }
    }
}
