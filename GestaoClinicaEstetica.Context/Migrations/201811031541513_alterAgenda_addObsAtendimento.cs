namespace GestaoClinicaEstetica.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterAgenda_addObsAtendimento : DbMigration
    {
        public override void Up()
        {
            AddColumn("AGENDA", "OBS_ATENDIMENTO", c => c.String(maxLength: 5000, unicode: false));
            DropTable("ATENDIMENTO");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.AGENDA", "OBS_ATENDIMENTO");
            CreateIndex("dbo.ATENDIMENTO", "COD_AGENDA");
            AddForeignKey("dbo.ATENDIMENTO", "COD_AGENDA", "dbo.AGENDA", "ID", cascadeDelete: true);
        }
    }
}
