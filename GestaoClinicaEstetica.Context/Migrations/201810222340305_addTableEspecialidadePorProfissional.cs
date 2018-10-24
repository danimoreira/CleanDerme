namespace GestaoClinicaEstetica.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTableEspecialidadePorProfissional : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ESPECIALIDADE_POR_PROFISSIONAL",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        COD_PROFISSIONAL = c.Int(nullable: false),
                        COD_ESPECIALIDADE = c.Int(nullable: false),
                        DATA_CADASTRO = c.DateTime(nullable: false, precision: 0),
                        USU_CADASTRO = c.String(unicode: false),
                        DATA_ALTERACAO = c.DateTime(nullable: false, precision: 0),
                        USU_ALTERACAO = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ESPECIALIDADE", t => t.COD_ESPECIALIDADE, cascadeDelete: true)
                .ForeignKey("dbo.PROFISSIONAL", t => t.COD_PROFISSIONAL, cascadeDelete: true)
                .Index(t => t.COD_PROFISSIONAL)
                .Index(t => t.COD_ESPECIALIDADE);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ESPECIALIDADE_POR_PROFISSIONAL", "COD_PROFISSIONAL", "dbo.PROFISSIONAL");
            DropForeignKey("dbo.ESPECIALIDADE_POR_PROFISSIONAL", "COD_ESPECIALIDADE", "dbo.ESPECIALIDADE");
            DropIndex("dbo.ESPECIALIDADE_POR_PROFISSIONAL", new[] { "COD_ESPECIALIDADE" });
            DropIndex("dbo.ESPECIALIDADE_POR_PROFISSIONAL", new[] { "COD_PROFISSIONAL" });
            DropTable("dbo.ESPECIALIDADE_POR_PROFISSIONAL");
        }
    }
}
