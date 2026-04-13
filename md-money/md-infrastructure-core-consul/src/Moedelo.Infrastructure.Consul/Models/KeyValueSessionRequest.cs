using System;
using System.Text;

namespace Moedelo.Infrastructure.Consul.Models;

internal struct KeyValueSessionRequest
{
    public string Key { get; set; }
    public string Value { get; set; }
    public string Session { get; set; }

    public static KeyValueSessionRequest Create(string key, string value, string sessionId)
    {
        return new KeyValueSessionRequest
        {
            Key = key,
            Value = Convert.ToBase64String(Encoding.UTF8.GetBytes(value)),
            Session = sessionId
        };
    }
} 