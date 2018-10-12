namespace GestaoClinicaEstetica.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class criacaobanco : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AGENDA",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        COD_PROFISSIONAL = c.Int(nullable: false),
                        COD_CLIENTE = c.Int(nullable: false),
                        COD_ESPECIALIDADE = c.Int(nullable: false),
                        DAT_INICIO = c.DateTime(nullable: false, precision: 0),
                        DAT_FIM = c.DateTime(nullable: false, precision: 0),
                        TIP_CONSULTA = c.Int(nullable: false),
                        PROCEDIMENTO = c.String(unicode: false),
                        DATA_CADASTRO = c.DateTime(nullable: false, precision: 0),
                        USU_CADASTRO = c.String(unicode: false),
                        DATA_ALTERACAO = c.DateTime(nullable: false, precision: 0),
                        USU_ALTERACAO = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CLIENTE", t => t.COD_CLIENTE, cascadeDelete: true)
                .ForeignKey("dbo.ESPECIALIDADE", t => t.COD_ESPECIALIDADE, cascadeDelete: true)
                .ForeignKey("dbo.PRESENCA_POR_CLIENTE", t => t.ID, cascadeDelete: true)
                .ForeignKey("dbo.PROFISSIONAL", t => t.COD_PROFISSIONAL, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.COD_PROFISSIONAL)
                .Index(t => t.COD_CLIENTE)
                .Index(t => t.COD_ESPECIALIDADE);
            
            CreateTable(
                "dbo.ATENDIMENTO",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        COD_AGENDA = c.Int(nullable: false),
                        OBSERVACAO = c.String(unicode: false),
                        DATA_CADASTRO = c.DateTime(nullable: false, precision: 0),
                        USU_CADASTRO = c.String(unicode: false),
                        DATA_ALTERACAO = c.DateTime(nullable: false, precision: 0),
                        USU_ALTERACAO = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AGENDA", t => t.COD_AGENDA, cascadeDelete: true)
                .Index(t => t.COD_AGENDA);
            
            CreateTable(
                "dbo.CLIENTE",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CPF = c.String(unicode: false),
                        DAT_NASCIMENTO = c.DateTime(nullable: false, precision: 0),
                        NOME = c.String(unicode: false),
                        ENDERECO = c.String(unicode: false),
                        BAIRRO = c.String(unicode: false),
                        CIDADE = c.String(unicode: false),
                        UF = c.String(unicode: false),
                        CEP = c.String(unicode: false),
                        TEL_FIXO = c.String(unicode: false),
                        TEL_CELULAR = c.String(unicode: false),
                        EMAIL = c.String(unicode: false),
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CLIENTE", t => t.COD_CLIENTE, cascadeDelete: true)
                .ForeignKey("dbo.RECEBIMENTOS", t => t.ID, cascadeDelete: true)
                .ForeignKey("dbo.SERVICO", t => t.COD_SERVICO, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.COD_SERVICO)
                .Index(t => t.COD_CLIENTE);
            
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ESPECIALIDADE", t => t.COD_ESPECIALIDADE, cascadeDelete: true)
                .ForeignKey("dbo.SERVICO_POR_CLIENTE", t => t.COD_SERVICO_POR_CLIENTE, cascadeDelete: true)
                .Index(t => t.COD_SERVICO_POR_CLIENTE)
                .Index(t => t.COD_ESPECIALIDADE);
            
            CreateTable(
                "dbo.ESPECIALIDADE",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DESCRICAO = c.String(unicode: false),
                        TIP_ATENDIMENTO = c.Int(nullable: false),
                        TMP_ATENDIMENTO_PADRAO = c.Time(nullable: false, precision: 0),
                        DATA_CADASTRO = c.DateTime(nullable: false, precision: 0),
                        USU_CADASTRO = c.String(unicode: false),
                        DATA_ALTERACAO = c.DateTime(nullable: false, precision: 0),
                        USU_ALTERACAO = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ESPECIALIDADE_POR_SERVICO",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        COD_SERVICO = c.Int(nullable: false),
                        COD_ESPECIALIDADE = c.Int(nullable: false),
                        QTD_SESSOES = c.Int(nullable: false),
                        DATA_CADASTRO = c.DateTime(nullable: false, precision: 0),
                        USU_CADASTRO = c.String(unicode: false),
                        DATA_ALTERACAO = c.DateTime(nullable: false, precision: 0),
                        USU_ALTERACAO = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ESPECIALIDADE", t => t.COD_ESPECIALIDADE, cascadeDelete: true)
                .ForeignKey("dbo.SERVICO", t => t.COD_SERVICO, cascadeDelete: true)
                .Index(t => t.COD_SERVICO)
                .Index(t => t.COD_ESPECIALIDADE);
            
            CreateTable(
                "dbo.SERVICO",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DESCRICAO = c.String(unicode: false),
                        VLR_SERVICO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PERIDIOCIDADE = c.Int(nullable: false),
                        DATA_CADASTRO = c.DateTime(nullable: false, precision: 0),
                        USU_CADASTRO = c.String(unicode: false),
                        DATA_ALTERACAO = c.DateTime(nullable: false, precision: 0),
                        USU_ALTERACAO = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RECEBIMENTOS",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        COD_SERVICO_POR_CLIENTE = c.Int(nullable: false),
                        DAT_VENCIMENTO = c.DateTime(nullable: false, precision: 0),
                        DAT_PAGAMENTO = c.DateTime(nullable: false, precision: 0),
                        VLR_DEVIDO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VLR_RECEBIDO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SIT_PAGAMENTO = c.Int(nullable: false),
                        USU_RECEBIMENTO = c.String(unicode: false),
                        TIP_PAGAMENTO = c.Int(nullable: false),
                        DATA_CADASTRO = c.DateTime(nullable: false, precision: 0),
                        USU_CADASTRO = c.String(unicode: false),
                        DATA_ALTERACAO = c.DateTime(nullable: false, precision: 0),
                        USU_ALTERACAO = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PRESENCA_POR_CLIENTE",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        COD_AGENDA = c.Int(nullable: false),
                        SIT_PRESENCA = c.Int(nullable: false),
                        OBSERVACAO = c.String(unicode: false),
                        DATA_CADASTRO = c.DateTime(nullable: false, precision: 0),
                        USU_CADASTRO = c.String(unicode: false),
                        DATA_ALTERACAO = c.DateTime(nullable: false, precision: 0),
                        USU_ALTERACAO = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PROFISSIONAL",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CPF = c.String(unicode: false),
                        DAT_NASCIMENTO = c.DateTime(nullable: false, precision: 0),
                        NOME = c.String(unicode: false),
                        ENDERECO = c.String(unicode: false),
                        BAIRRO = c.String(unicode: false),
                        CIDADE = c.String(unicode: false),
                        UF = c.String(unicode: false),
                        CEP = c.String(unicode: false),
                        TEL_FIXO = c.String(unicode: false),
                        TEL_CELULAR = c.String(unicode: false),
                        EMAIL = c.String(unicode: false),
                        DATA_CADASTRO = c.DateTime(nullable: false, precision: 0),
                        USU_CADASTRO = c.String(unicode: false),
                        DATA_ALTERACAO = c.DateTime(nullable: false, precision: 0),
                        USU_ALTERACAO = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DADOS_CLINICA",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CPF = c.String(unicode: false),
                        DAT_FUNDACAO = c.DateTime(nullable: false, precision: 0),
                        NOME = c.String(unicode: false),
                        ENDERECO = c.String(unicode: false),
                        BAIRRO = c.String(unicode: false),
                        CIDADE = c.String(unicode: false),
                        UF = c.String(unicode: false),
                        CEP = c.String(unicode: false),
                        TEL_FIXO = c.String(unicode: false),
                        TEL_CELULAR = c.String(unicode: false),
                        EMAIL = c.String(unicode: false),
                        DATA_CADASTRO = c.DateTime(nullable: false, precision: 0),
                        USU_CADASTRO = c.String(unicode: false),
                        DATA_ALTERACAO = c.DateTime(nullable: false, precision: 0),
                        USU_ALTERACAO = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.USUARIO",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NOME = c.String(unicode: false),
                        LOGIN = c.String(unicode: false),
                        SENHA = c.String(unicode: false),
                        DATA_CADASTRO = c.DateTime(nullable: false, precision: 0),
                        USU_CADASTRO = c.String(unicode: false),
                        DATA_ALTERACAO = c.DateTime(nullable: false, precision: 0),
                        USU_ALTERACAO = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AGENDA", "COD_PROFISSIONAL", "dbo.PROFISSIONAL");
            DropForeignKey("dbo.AGENDA", "ID", "dbo.PRESENCA_POR_CLIENTE");
            DropForeignKey("dbo.AGENDA", "COD_ESPECIALIDADE", "dbo.ESPECIALIDADE");
            DropForeignKey("dbo.AGENDA", "COD_CLIENTE", "dbo.CLIENTE");
            DropForeignKey("dbo.SERVICO_POR_CLIENTE", "COD_SERVICO", "dbo.SERVICO");
            DropForeignKey("dbo.SERVICO_POR_CLIENTE", "ID", "dbo.RECEBIMENTOS");
            DropForeignKey("dbo.ESPECIALIDADE_POR_SERVICO_POR_CLIENTE", "COD_SERVICO_POR_CLIENTE", "dbo.SERVICO_POR_CLIENTE");
            DropForeignKey("dbo.ESPECIALIDADE_POR_SERVICO_POR_CLIENTE", "COD_ESPECIALIDADE", "dbo.ESPECIALIDADE");
            DropForeignKey("dbo.ESPECIALIDADE_POR_SERVICO", "COD_SERVICO", "dbo.SERVICO");
            DropForeignKey("dbo.ESPECIALIDADE_POR_SERVICO", "COD_ESPECIALIDADE", "dbo.ESPECIALIDADE");
            DropForeignKey("dbo.SERVICO_POR_CLIENTE", "COD_CLIENTE", "dbo.CLIENTE");
            DropForeignKey("dbo.ATENDIMENTO", "COD_AGENDA", "dbo.AGENDA");
            DropIndex("dbo.ESPECIALIDADE_POR_SERVICO", new[] { "COD_ESPECIALIDADE" });
            DropIndex("dbo.ESPECIALIDADE_POR_SERVICO", new[] { "COD_SERVICO" });
            DropIndex("dbo.ESPECIALIDADE_POR_SERVICO_POR_CLIENTE", new[] { "COD_ESPECIALIDADE" });
            DropIndex("dbo.ESPECIALIDADE_POR_SERVICO_POR_CLIENTE", new[] { "COD_SERVICO_POR_CLIENTE" });
            DropIndex("dbo.SERVICO_POR_CLIENTE", new[] { "COD_CLIENTE" });
            DropIndex("dbo.SERVICO_POR_CLIENTE", new[] { "COD_SERVICO" });
            DropIndex("dbo.SERVICO_POR_CLIENTE", new[] { "ID" });
            DropIndex("dbo.ATENDIMENTO", new[] { "COD_AGENDA" });
            DropIndex("dbo.AGENDA", new[] { "COD_ESPECIALIDADE" });
            DropIndex("dbo.AGENDA", new[] { "COD_CLIENTE" });
            DropIndex("dbo.AGENDA", new[] { "COD_PROFISSIONAL" });
            DropIndex("dbo.AGENDA", new[] { "ID" });
            DropTable("dbo.USUARIO");
            DropTable("dbo.DADOS_CLINICA");
            DropTable("dbo.PROFISSIONAL");
            DropTable("dbo.PRESENCA_POR_CLIENTE");
            DropTable("dbo.RECEBIMENTOS");
            DropTable("dbo.SERVICO");
            DropTable("dbo.ESPECIALIDADE_POR_SERVICO");
            DropTable("dbo.ESPECIALIDADE");
            DropTable("dbo.ESPECIALIDADE_POR_SERVICO_POR_CLIENTE");
            DropTable("dbo.SERVICO_POR_CLIENTE");
            DropTable("dbo.CLIENTE");
            DropTable("dbo.ATENDIMENTO");
            DropTable("dbo.AGENDA");
        }
    }
}
