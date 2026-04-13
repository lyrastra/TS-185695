using System.Collections;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Exceptions;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Extensions;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance;

internal sealed class OffsetMap : IOffsetMap
{
    private BitArray offsetMap;
    private readonly string name;
    public long? CommittedOffset { get; private set; }
    public DateTime? CommittedDateTimeUtc { get; private set; }
    public int OffsetMapDepth { get; private set; }
    public byte[] OffsetMapData => offsetMap.ToByteArray();

    public OffsetMap(string name, int capacity)
    {
        this.name = name;
        this.offsetMap = new BitArray(capacity);
        this.OffsetMapDepth = 0;
    }

    public void SetCommittedOffset(long offset)
    {
        if (CommittedOffset == null)
        {
            CommittedOffset = offset;
            CommittedDateTimeUtc = DateTime.UtcNow;
            offsetMap.SetAll(false);
            return;
        }

        if (offset < CommittedOffset)
        {
            throw new InvalidOffsetException($"{name}: нельзя переместить смещение назад. Текущее смещение = {CommittedOffset}, предлагаемое = {offset}");
        }

        var delta = offset - CommittedOffset;

        if (delta >= offsetMap.Length)
        {
            offsetMap.SetAll(false);
        }
        else
        {
            offsetMap.RightShift((int)delta);
        }

        OffsetMapDepth = Math.Max(0, OffsetMapDepth - (int)delta);
        CommittedOffset = offset;
        CommittedDateTimeUtc = DateTime.UtcNow;
        
    }

    public void MarkAsSkipped(long offset)
    {
        if (CommittedOffset == null)
        {
            SetCommittedOffset(offset - 1);
        }

        var index = GetMarkingItemIndex(offset);
        offsetMap[index] = false;
        OffsetMapDepth = Math.Max(OffsetMapDepth, index + 1);
    }

    public void MarkAsProcessed(long offset)
    {
        var index = GetMarkingItemIndex(offset);
        offsetMap[index] = true;
        OffsetMapDepth = Math.Max(OffsetMapDepth, index + 1);
    }

    public bool IsMarkedAsProcessed(long offset)
    {
        if (CommittedOffset == null)
        {
            return false;
        }

        if (offset <= CommittedOffset)
        {
            return true;
        }

        var index = GetMarkingItemIndex(offset, throwOnTooFarFromCommitted: false);

        if (index >= OffsetMapDepth)
        {
            return false;
        }

        return offsetMap.Get(index);
    }

    public int CountSkippedNotAfter(long offset)
    {
        if (CommittedOffset == null || offset <= CommittedOffset)
        {
            return 0;
        }
        
        var index = (int)Math.Min(OffsetMapDepth, offset - CommittedOffset.Value);

        return offsetMap
            .Cast<bool>()
            .Take(index)
            .Count(value => value == false);
    }

    public bool HasAnySkippedMessageBefore(long offset)
    {
        if (CommittedOffset == null || offset <= CommittedOffset)
        {
            return false;
        }

        var index = GetMarkingItemIndex(offset);

        return offsetMap
            .Cast<bool>()
            .Take(index)
            .Any(value => value == false);
    }

    public void Set(long committedOffset, DateTime committedAtUtc, int offsetMapDepth, byte[] offsetMapData)
    {
        CommittedOffset = committedOffset;
        CommittedDateTimeUtc = committedAtUtc;
        OffsetMapDepth = offsetMapDepth;

        var newOffsetMap = new BitArray(offsetMapData);
        var newCapacity = Math.Max(Math.Max(offsetMap.Length, newOffsetMap.Length), offsetMapDepth);
        var capacityInBytes = newCapacity / 8 + ((newCapacity % 8) > 0 ? 1 : 0); 
        var newBytes = new byte[capacityInBytes];
        offsetMapData.CopyTo(newBytes, 0);

        offsetMap = new BitArray(newBytes);
    }

    private int GetMarkingItemIndex(long offset, bool throwOnTooFarFromCommitted = true)
    {
        if (CommittedOffset == null)
        {
            throw new InvalidOffsetException($"{name}: нельзя отметить сообщение со смещением {offset}. Причина: не задано закоммиченное смещение");
        }
        
        if (offset <= CommittedOffset)
        {
            throw new InvalidOffsetException($"{name}: нельзя отметить сообщение со смещением {offset} находящее не после закоммиченного смещения {CommittedOffset}");
        }

        var index = offset - CommittedOffset - 1;
        if (index >= offsetMap.Length && throwOnTooFarFromCommitted)
        {
            throw new InvalidOffsetException($"{name}: нельзя отметить сообщение со смещением {offset}, причина: оно находится на расстоянии больше чем на {offsetMap.Length-1} от закоммиченного смещения {CommittedOffset}");
        }

        return (int)index;
    }
}
