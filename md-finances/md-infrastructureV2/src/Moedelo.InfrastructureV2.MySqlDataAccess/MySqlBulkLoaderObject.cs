using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Moedelo.InfrastructureV2.MySqlDataAccess;

public sealed class MySqlBulkLoaderObject<T> where T : class
{
    public MySqlBulkLoaderObject(string name, IEnumerable<T> data)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));

        if (data == null)
            throw new ArgumentNullException(nameof(data));
            
        Name = name;
            
        var type = typeof(T);
        var propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
        var result = new StringBuilder();
            
        foreach (var item in data)
            result.AppendLine(string.Join(";", propertyInfos.Select(x => x.GetValue(item).ToString())));

        Data = result.ToString();
    }
        
    public string Name { get; }
        
    public string Data { get; }
}