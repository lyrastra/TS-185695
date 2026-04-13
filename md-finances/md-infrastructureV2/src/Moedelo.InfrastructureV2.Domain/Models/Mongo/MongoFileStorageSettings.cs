namespace Moedelo.InfrastructureV2.Domain.Models.Mongo;

public struct MongoFileStorageSettings
{
    public string ConnectionString { get; }

    public string Tag { get; }

    private readonly string hashKey;

    public MongoFileStorageSettings(string connectionString, string tag)
    {
        ConnectionString = connectionString;
        Tag = tag;
        hashKey = $"{connectionString}_{tag}";
    }

    public override int GetHashCode()
    {
        return hashKey.GetHashCode();
    }
}