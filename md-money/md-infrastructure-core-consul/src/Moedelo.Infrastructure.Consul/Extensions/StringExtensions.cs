#nullable enable
using System;
using System.Text;

namespace Moedelo.Infrastructure.Consul.Extensions;

internal static class StringExtensions
{
    internal static string? GetUtf8StringFromBase64String(this string? value)
    {
        if (value == null)
        {
            return null;
        }

        return Encoding.UTF8.GetString(Convert.FromBase64String(value));
    }
    
    internal static void EnsureIsServiceIdValid(this string? serviceId)
    {
        if (string.IsNullOrEmpty(serviceId))
        {
            throw new ArgumentException(@"Не может быть пустым", nameof(serviceId));
        }

        if (serviceId!.IndexOf('/') >= 0)
        {
            throw new ArgumentException(@"Не может содержать '/'", nameof(serviceId));
        }
    }
}
