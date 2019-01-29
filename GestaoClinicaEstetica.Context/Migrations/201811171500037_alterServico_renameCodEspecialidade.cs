namespace GestaoClinicaEstetica.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterServico_renameCodEspecialidade : DbMigration
    {
        public override void Up()
        {
            //RenameColumn(table: "dbo.SERVICO", name: "CodigoEspecialidade", newName: "COD_ESPECIALIDADE");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.SERVICO", name: "COD_ESPECIALIDADE", newName: "CodigoEspecialidade");
        }
    }
}
