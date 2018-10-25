namespace GestaoClinicaEstetica.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ajusteEstruturaTabelas : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SERVICO_POR_CLIENTE", "COD_CLIENTE", "dbo.CLIENTE");
            DropForeignKey("dbo.ESPECIALIDADE_POR_SERVICO_POR_CLIENTE", "COD_ESPECIALIDADE", "dbo.ESPECIALIDADE");
            DropForeignKey("dbo.ESPECIALIDADE_POR_SERVICO_POR_CLIENTE", "COD_SERVICO_POR_CLIENTE", "dbo.SERVICO_POR_CLIENTE");
            DropForeignKey("dbo.SERVICO_POR_CLIENTE", "ID", "dbo.RECEBIMENTOS");
            DropForeignKey("dbo.SERVICO_POR_CLIENTE", "COD_SERVICO", "dbo.SERVICO");
            DropIndex("dbo.SERVICO_POR_CLIENTE", new[] { "ID" });
            DropIndex("dbo.SERVICO_POR_CLIENTE", new[] { "COD_SERVICO" });
            DropIndex("dbo.SERVICO_POR_CLIENTE", new[] { "COD_CLIENTE" });
            DropIndex("dbo.ESPECIALIDADE_POR_SERVICO_POR_CLIENTE", new[] { "COD_SERVICO_POR_CLIENTE" });
            DropIndex("dbo.ESPECIALIDADE_POR_SERVICO_POR_CLIENTE", new[] { "COD_ESPECIALIDADE" });
            AddColumn("dbo.RECEBIMENTOS", "COD_CLIENTE", c => c.Int(nullable: false));
            AddColumn("dbo.RECEBIMENTOS", "COD_SERVICO", c => c.Int(nullable: false));
            AddColumn("dbo.RECEBIMENTOS", "DAT_AQUISICAO", c => c.DateTime(nullable: false, precision: 0));
            DropColumn("dbo.RECEBIMENTOS", "COD_SERVICO_POR_CLIENTE");
            DropTable("dbo.SERVICO_POR_CLIENTE");
            DropTable("dbo.ESPECIALIDADE_POR_SERVICO_POR_CLIENTE");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ESPECIALIDADE_POR_SERVICO_POR_CLIENTE",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        COD_SERVICO_POR_CLIENTE = c.Int(nullable: false),
                        COD_ESPECIALIDADE = c.Int(nullable: false),
                        REFERENCIA = c.String(unicode: false),
                        QTD_SESSOES = c.Int(nullable: false),
                        QTD_SESSOES_REALIZADAS = c.Int(nullable: false),
                        DATA_CADASTRO = c.DateTime(nullable: false, precision: 0),
                        USU_CADASTRO = c.String(unicode: false),
                        DATA_ALTERACAO = c.DateTime(nullable: false, precision: 0),
                        USU_ALTERACAO = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SERVICO_POR_CLIENTE",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        COD_SERVICO = c.Int(nullable: false),
                        COD_CLIENTE = c.Int(nullable: false),
                        DAT_AQUISICAO = c.DateTime(nullable: false, precision: 0),
                        SIT_SERVICO = c.String(unicode: false),
                        DATA_CADASTRO = c.DateTime(nullable: false, precision: 0),
                        USU_CADASTRO = c.String(unicode: false),
                        DATA_ALTERACAO = c.DateTime(nullable: false, precision: 0),
                        USU_ALTERACAO = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.RECEBIMENTOS", "COD_SERVICO_POR_CLIENTE", c => c.Int(nullable: false));
            DropColumn("dbo.RECEBIMENTOS", "DAT_AQUISICAO");
            DropColumn("dbo.RECEBIMENTOS", "COD_SERVICO");
            DropColumn("dbo.RECEBIMENTOS", "COD_CLIENTE");
            CreateIndex("dbo.ESPECIALIDADE_POR_SERVICO_POR_CLIENTE", "COD_ESPECIALIDADE");
            CreateIndex("dbo.ESPECIALIDADE_POR_SERVICO_POR_CLIENTE", "COD_SERVICO_POR_CLIENTE");
            CreateIndex("dbo.SERVICO_POR_CLIENTE", "COD_CLIENTE");
            CreateIndex("dbo.SERVICO_POR_CLIENTE", "COD_SERVICO");
            CreateIndex("dbo.SERVICO_POR_CLIENTE", "ID");
            AddForeignKey("dbo.SERVICO_POR_CLIENTE", "COD_SERVICO", "dbo.SERVICO", "ID", cascadeDelete: true);
            AddForeignKey("dbo.SERVICO_POR_CLIENTE", "ID", "dbo.RECEBIMENTOS", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ESPECIALIDADE_POR_SERVICO_POR_CLIENTE", "COD_SERVICO_POR_CLIENTE", "dbo.SERVICO_POR_CLIENTE", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ESPECIALIDADE_POR_SERVICO_POR_CLIENTE", "COD_ESPECIALIDADE", "dbo.ESPECIALIDADE", "ID", cascadeDelete: true);
            AddForeignKey("dbo.SERVICO_POR_CLIENTE", "COD_CLIENTE", "dbo.CLIENTE", "ID", cascadeDelete: true);
        }
    }
}
