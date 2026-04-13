#nullable enable
using System;
using System.Runtime.CompilerServices;

namespace Moedelo.Common.Kafka.Abstractions.Extensions;

internal static class EnsureExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static TType EnsureIsNotNull<TType>(this TType? value, string paramName)
    {
        return value ?? throw new ArgumentNullException(paramName);
    }
}
