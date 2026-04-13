using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Moedelo.InfrastructureV2.ApiClient.Internals;

internal readonly struct QueryParam
{
    private readonly Type type;
    
    public QueryParam(PropertyInfo propertyInfo, object queryParams)
    {
        Name = propertyInfo.Name;
        type = propertyInfo.PropertyType;
        Value = propertyInfo.GetValue(queryParams);
    }

    public string Name { get; }
    public object Value { get; }

    public bool IsNullEnumerableQueryParam()
    {
        return Value == null
               && type != typeof(string)
               && type
                   .GetInterfaces()
                   .Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>));
    }
}
