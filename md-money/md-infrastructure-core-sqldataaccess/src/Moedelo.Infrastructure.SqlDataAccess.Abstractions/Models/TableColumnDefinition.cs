using System;
using System.Data;
using System.Reflection;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Helpers;

namespace Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;

internal record TableColumnDefinition(PropertyInfo PropertyInfo, string ColumnDeclaration)
{
    private Type DbColumnDataType => PropertyInfo.PropertyType.GetColumnDataType();

    public DataColumn DataColumn => new DataColumn(PropertyName, DbColumnDataType)
    {
        AllowDBNull = PropertyInfo.PropertyType.CanBeNull()
    };

    public string PropertyName => PropertyInfo.Name;
};
