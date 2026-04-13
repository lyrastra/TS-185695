using System;
using System.Collections;

namespace Moedelo.InfrastructureV2.DataAccess.Extensions;

internal static class ListExtensions
{
    internal static Type GetListItemType(this IList list)
    {
        var genericArguments = (list ?? throw new ArgumentNullException(nameof(list)))
            .GetType()
            .GetGenericArguments();

        if (genericArguments.Length > 0)
        {
            return genericArguments[0];
        }
        
        foreach (var item in list)
        {
            if (item != default)
            {
                return item.GetType();
            }
        }

        throw new ArgumentException("Не удалось определить тип элементов списка");
    }
}
