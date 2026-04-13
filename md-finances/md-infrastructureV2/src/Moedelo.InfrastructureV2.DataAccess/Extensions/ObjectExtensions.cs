using System.Data;
using Dapper;

namespace Moedelo.InfrastructureV2.DataAccess.Extensions;

internal static class ObjectExtensions
{
    internal static object ChangeIfTvp(this object propValue)
    {
        return propValue is not DataTable dataTable
            ? propValue
            : dataTable.AsTableValuedParameter(dataTable.TableName);
    }
}
