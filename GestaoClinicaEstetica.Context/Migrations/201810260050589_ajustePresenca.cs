namespace GestaoClinicaEstetica.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ajustePresenca : DbMigration
    {
        public override void Up()
        {   
            AddColumn("dbo.AGENDA", "SIT_PRESENCA", c => c.Int(nullable: false));
            AddColumn("dbo.AGENDA", "OBS_PRESENCA", c => c.String(unicode: false));
            DropTable("dbo.PRESENCA_POR_CLIENTE");
        }
        
        public override void Down()
        {
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
            
            DropColumn("dbo.AGENDA", "OBS_PRESENCA");
            DropColumn("dbo.AGENDA", "SIT_PRESENCA");
            CreateIndex("dbo.PRESENCA_POR_CLIENTE", "COD_AGENDA");
            AddForeignKey("dbo.PRESENCA_POR_CLIENTE", "COD_AGENDA", "dbo.AGENDA", "ID", cascadeDelete: true);
        }
    }
}
