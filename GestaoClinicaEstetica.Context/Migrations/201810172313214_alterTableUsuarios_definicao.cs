namespace GestaoClinicaEstetica.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterTableUsuarios_definicao : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.USUARIO", "NOME", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.USUARIO", "LOGIN", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.USUARIO", "SENHA", c => c.String(nullable: false, maxLength: 15, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.USUARIO", "SENHA", c => c.String(unicode: false));
            AlterColumn("dbo.USUARIO", "LOGIN", c => c.String(unicode: false));
            AlterColumn("dbo.USUARIO", "NOME", c => c.String(unicode: false));
        }
    }
}
