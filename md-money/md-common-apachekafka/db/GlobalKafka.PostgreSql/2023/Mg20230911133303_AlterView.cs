using FluentMigrator;

namespace Moedelo.Common.Kafka.PostgreSql.Migrations;

[Migration(20230911133303)]
// ReSharper disable once InconsistentNaming
public class Mg20230911133303_AlterView : Migration
{
    public override void Up()
    {
        Execute.EmbeddedScript("Mg20230911133303_AlterView.sql");
    }

    public override void Down()
    {
        throw new InvalidOperationException();
    }
}
