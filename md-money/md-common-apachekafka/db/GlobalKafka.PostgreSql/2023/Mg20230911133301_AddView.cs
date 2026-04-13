using FluentMigrator;

namespace Moedelo.Common.Kafka.PostgreSql.Migrations;

[Migration(20230911133301)]
// ReSharper disable once InconsistentNaming
public class Mg20230911133301_AddView : Migration
{
    public override void Up()
    {
        Execute.EmbeddedScript("Mg20230911133301_AddView.sql");
    }

    public override void Down()
    {
        throw new InvalidOperationException();
    }
}
