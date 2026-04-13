#nullable enable
using System.Runtime.CompilerServices;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Extensions;

internal static class EnsureExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static TType EnsureIsNotNull<TType>(this TType? value, string paramName)
    {
        return value ?? throw new ArgumentNullException(paramName);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static TType EnsureIsNotNull<TType>(this TType? value, string paramName, string message)
    {
        return value ?? throw new ArgumentNullException(paramName, message);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static TType EnsureIsNotNull<TType>(this TType? value, string paramName, Func<string> message)
    {
        return value ?? throw new ArgumentNullException(paramName, message.Invoke());
    }
}
