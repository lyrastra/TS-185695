using System.Data;

namespace Moedelo.InfrastructureV2.Domain.Models.DataAccess;

public class DynamicParam
{
    public DynamicParam(string name, object value = null, DbType? dbType = null, ParameterDirection? direction = null, int? size = null, byte? precision = null, byte? scale = null)
    {
        Name = name;
        Value = value;
        DbType = dbType;
        Direction = direction;
        Size = size;
        Precision = precision;
        Scale = scale;
    }

    public string Name { get; private set; }

    public object Value { get; private set; }

    public DbType? DbType { get; private set; }

    public ParameterDirection? Direction { get; private set; }

    public int? Size { get; private set; }

    public byte? Precision { get; private set; }

    public byte? Scale { get; private set; }
}