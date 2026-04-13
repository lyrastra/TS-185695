using FluentMigrator;

namespace Moedelo.Money.Migrations
{
    [Migration(20201001183300)]
    public class Mg20201001183300_alter_bank_balance_date_type : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Mg20201001183300_alter_bank_balance_date_type.sql");
        }

        public override void Down()
        {
        }
    }
}
