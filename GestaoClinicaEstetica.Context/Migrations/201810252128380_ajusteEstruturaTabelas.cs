namespace GestaoClinicaEstetica.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ajusteEstruturaTabelas : DbMigration
    {
        public override void Up()
        {
            AddColumn("RECEBIMENTOS", "COD_CLIENTE", c => c.Int(nullable: false));
            AddColumn("RECEBIMENTOS", "COD_SERVICO", c => c.Int(nullable: false));
            AddColumn("RECEBIMENTOS", "DAT_AQUISICAO", c => c.DateTime(nullable: false, precision: 0));

        }
        
        public override void Down()
        {
            CreateTable(
                "ESPECIALIDADE_POR_SERVICO_POR_CLIENTE",
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
                "SERVICO_POR_CLIENTE",
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
            
            AddColumn("RECEBIMENTOS", "COD_SERVICO_POR_CLIENTE", c => c.Int(nullable: false));
            DropColumn("RECEBIMENTOS", "DAT_AQUISICAO");
            DropColumn("RECEBIMENTOS", "COD_SERVICO");
            DropColumn("RECEBIMENTOS", "COD_CLIENTE");
            CreateIndex("ESPECIALIDADE_POR_SERVICO_POR_CLIENTE", "COD_ESPECIALIDADE");
            CreateIndex("ESPECIALIDADE_POR_SERVICO_POR_CLIENTE", "COD_SERVICO_POR_CLIENTE");
            CreateIndex("SERVICO_POR_CLIENTE", "COD_CLIENTE");
            CreateIndex("SERVICO_POR_CLIENTE", "COD_SERVICO");
            CreateIndex("SERVICO_POR_CLIENTE", "ID");
            AddForeignKey("SERVICO_POR_CLIENTE", "COD_SERVICO", "SERVICO", "ID", cascadeDelete: true);
            AddForeignKey("SERVICO_POR_CLIENTE", "ID", "RECEBIMENTOS", "ID", cascadeDelete: true);
            AddForeignKey("ESPECIALIDADE_POR_SERVICO_POR_CLIENTE", "COD_SERVICO_POR_CLIENTE", "SERVICO_POR_CLIENTE", "ID", cascadeDelete: true);
            AddForeignKey("ESPECIALIDADE_POR_SERVICO_POR_CLIENTE", "COD_ESPECIALIDADE", "ESPECIALIDADE", "ID", cascadeDelete: true);
            AddForeignKey("SERVICO_POR_CLIENTE", "COD_CLIENTE", "CLIENTE", "ID", cascadeDelete: true);
        }
    }
}
