namespace GestaoClinicaEstetica.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class incluirTabelaDespesa : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DESPESAS",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DSC_DESPESA = c.String(unicode: false),
                        NOM_FORNECEDOR = c.String(unicode: false),
                        DAT_VENCIMENTO = c.DateTime(nullable: false, precision: 0),
                        DAT_PAGAMENTO = c.DateTime(precision: 0),
                        SIT_DESPESA = c.Int(nullable: false),
                        USU_PAGAMENTO = c.String(unicode: false),
                        VAL_DESPESA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VAL_PAGAMENTO = c.Decimal(precision: 18, scale: 2),
                        TIP_PAGAMENTO = c.Int(nullable: false),
                        DSC_OBSERVACAO = c.String(unicode: false),
                        DATA_CADASTRO = c.DateTime(nullable: false, precision: 0),
                        USU_CADASTRO = c.String(unicode: false),
                        DATA_ALTERACAO = c.DateTime(nullable: false, precision: 0),
                        USU_ALTERACAO = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DESPESAS");
        }
    }
}
