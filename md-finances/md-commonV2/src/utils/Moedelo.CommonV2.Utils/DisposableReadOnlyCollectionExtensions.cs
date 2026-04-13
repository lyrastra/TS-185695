using System;
using System.Collections.Generic;

namespace Moedelo.CommonV2.Utils;

public static class DisposableReadOnlyCollectionExtensions
{
    public static DisposableReadOnlyCollection<TDisposable> ToDisposableReadOnlyCollection<TDisposable>(
        this IReadOnlyCollection<TDisposable> collection)
        where TDisposable : IDisposable => new DisposableReadOnlyCollection<TDisposable>(collection);
}
