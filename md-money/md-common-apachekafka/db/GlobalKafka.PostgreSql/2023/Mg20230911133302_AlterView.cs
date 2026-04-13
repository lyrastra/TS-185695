using FluentMigrator;

namespace Moedelo.Common.Kafka.PostgreSql.Migrations;

[Migration(20230911133302)]
// ReSharper disable once InconsistentNaming
public class Mg20230911133302_AlterView : Migration
{
    public override void Up()
    {
        Execute.EmbeddedScript("Mg20230911133302_AlterView.sql");
    }

    public override void Down()
    {
        throw new InvalidOperationException();
    }
}
