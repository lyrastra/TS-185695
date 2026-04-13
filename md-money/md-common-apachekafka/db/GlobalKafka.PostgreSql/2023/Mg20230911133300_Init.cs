using FluentMigrator;

namespace Moedelo.Common.Kafka.PostgreSql.Migrations;

[Migration(20230911133300)]
// ReSharper disable once InconsistentNaming
public class Mg20230911133300_Init : Migration
{
    public override void Up()
    {
        Execute.EmbeddedScript("Mg20230911133300_Init.sql");
    }

    public override void Down()
    {
        throw new InvalidOperationException();
    }
}