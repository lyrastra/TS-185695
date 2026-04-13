using FluentMigrator;

namespace Moedelo.Money.Migrations
{
    [Migration(20200911130000)]
    public class Mg20200911130000_BP_9055_init: Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Mg20200911130000_BP_9055_init.sql");
        }

        public override void Down()
        {

        }
    }
}
