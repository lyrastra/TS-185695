using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Moedelo.Common.Audit.Logging;

internal sealed class SimpleListPool<TItem>
{
    private readonly List<TItem>[] pool;
    private readonly bool[] captured;
    private readonly int listCapacity;

    public SimpleListPool(int poolCapacity, int listCapacity)
    {
        Debug.Assert(poolCapacity > 0);
        Debug.Assert(listCapacity > 0);

        this.listCapacity = listCapacity;
        this.pool = Enumerable.Range(0, poolCapacity)
            .Select(_ => new List<TItem>(listCapacity))
            .ToArray();
        this.captured = Enumerable.Repeat(false, poolCapacity).ToArray();
    }

    /// <summary>
    /// Возвращает список в пул. Список при этом очищается вызовом метода Clear()
    /// </summary>
    /// <param name="list"></param>
    public void Release(List<TItem> list)
    {
        if (list == null)
        {
            return;
        }

        lock (pool)
        {
            list.Clear();
            var index = Array.IndexOf(pool, list);

            if (index >= 0)
            {
                if (captured[index])
                {
                    captured[index] = false;
                }
            }
        }
    }

    public List<TItem> Capture()
    {
        lock (pool)
        {
            var index = Array.IndexOf(captured, false);

            if (index >= 0)
            {
                captured[index] = true;

                return pool[index];
            }

            return new List<TItem>(listCapacity);
        }
    }
}
