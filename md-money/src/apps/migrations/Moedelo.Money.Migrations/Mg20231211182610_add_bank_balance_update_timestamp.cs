using FluentMigrator;

namespace Moedelo.Money.Migrations
{
    [Migration(20231211182610)]
    public class Mg20231211182610_add_bank_balance_update_timestamp : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Mg20231211182610_add_bank_balance_update_timestamp.sql");
        }

        public override void Down()
        {
        }
    }
}
