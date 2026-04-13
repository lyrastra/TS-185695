using FluentMigrator;

namespace Moedelo.Money.Migrations
{
    [Migration(20251124150101)]
    public class Mg20251124150101_AD_4592_add_is_manual_flag: Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Mg20251124150101_AD_4592_add_is_manual_flag.sql");
        }

        public override void Down()
        {
        }
    }
}