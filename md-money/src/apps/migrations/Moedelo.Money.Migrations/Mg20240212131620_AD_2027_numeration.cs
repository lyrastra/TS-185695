using FluentMigrator;

namespace Moedelo.Money.Migrations
{
    [Migration(20240212131620)]
    public class Mg20240212131620_AD_2027_numeration : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Mg20240212131620_AD_2027_numeration.sql");
        }

        public override void Down()
        {

        }
    }
}
