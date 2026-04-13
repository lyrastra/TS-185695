using FluentAssertions;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Exceptions;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Tests;

[TestFixture]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[Parallelizable(ParallelScope.Children)]
public class OffsetMapTests
{
    private const int BitsCapacity = 1024;
    private const int BytesCapacity = BitsCapacity / 8;
    
    private IOffsetMap map = null!;
    private long offset;
    private DateTime committedAt;

    [SetUp]
    public void SetUp()
    {
        map = new OffsetMap("TestOffsetMap", BitsCapacity);
        offset = TestContext.CurrentContext.Random.NextLong(10, 100000);
        committedAt = DateTime.UtcNow;
    }

    [Test]
    public void CommittedOffset_IsNull_ByDefault()
    {
        map.CommittedOffset.Should().BeNull();
    }

    [Test]
    public void OffsetMapDepth_IsZero_ByDefault()
    {
        map.OffsetMapDepth.Should().Be(0);
    }

    [Test]
    public void OffsetMapData_HasCountRelatedToCapacity_ByDefault()
    {
        map.OffsetMapData.Should().HaveCount(BytesCapacity);
    }

    [Test]
    public void OffsetMapData_ContainsOnlyZero_ByDefault()
    {
        map.OffsetMapData.Should().OnlyContain(v => v == 0);
    }

    [Test]
    public void SetCommittedOffset_SetsCommittedOffset()
    {
        map.SetCommittedOffset(offset);

        map.CommittedOffset.Should().Be(offset);
    }
    
    [Test]
    public void SetCommittedOffset_ThrowsException_IfNewValueIsLessThanCurrent()
    {
        map.SetCommittedOffset(offset);
        var act = () => map.SetCommittedOffset(offset-1);

        act.Should().Throw<InvalidOffsetException>();
    }
    
    [Test]
    public void SetCommittedOffset_DoesNotThrowException_IfNewValueTooFarFromCurrent()
    {
        map.SetCommittedOffset(offset);
        var act = () => map.SetCommittedOffset(offset + BitsCapacity);

        act.Should().NotThrow();
    }

    [Test]
    public void Set_SetsValues()
    {
        var depth = 14;
        var offsetMap = new byte[] { 1, 4 };
        map.Set(offset, committedAt, depth, offsetMap);

        var expectedOffsetMap = new byte[BytesCapacity];
        offsetMap.CopyTo(expectedOffsetMap, 0);

        map.CommittedOffset.Should().Be(offset);
        map.OffsetMapDepth.Should().Be(depth);
        map.OffsetMapData.Should().BeEquivalentTo(expectedOffsetMap);
    }

    [Test]
    public void Set_SavesData_IfBinDataIsLongerThanCurrentCapacity(
        [Values(BitsCapacity * 2)] int biggerBitsCapacity,
        [Values(BitsCapacity - 128, BitsCapacity - 1, BitsCapacity, BitsCapacity + 128)] int index,
        [Values(true, false)]bool processed)
    {
        var map0 = new OffsetMap("saved", biggerBitsCapacity);
        map0.SetCommittedOffset(0);
        if (processed)
        {
            map0.MarkAsProcessed(index);
        }

        var savedBytes = map0.OffsetMapData;

        map.Set(committedOffset: 0, committedAt, biggerBitsCapacity, savedBytes);

        map.IsMarkedAsProcessed(index).Should().Be(processed);
    }

    [Test]
    public void Set_ResetsData_IfBinDataIsShorterThanCurrentCapacity(
        [Values(BitsCapacity / 2)] int shorterBitsCapacity,
        [Values(BitsCapacity / 2 - 1)] int index,
        [Values(true, false)] bool processed)
    {
        var map0 = new OffsetMap("saved", shorterBitsCapacity);
        map0.SetCommittedOffset(0);
        if (processed)
        {
            map0.MarkAsProcessed(index);
        }

        var savedBytes = map0.OffsetMapData;

        map.Set(committedOffset: 0, committedAt, shorterBitsCapacity, savedBytes);

        map.IsMarkedAsProcessed(index).Should().Be(processed);
    }

    [Test]
    public void SetCommittedOffset_CutsDepth()
    {
        var depth = 14;
        var offsetMap = new byte[] { 0, 0 };
        map.Set(offset, committedAt, depth, offsetMap);

        var newOffset = offset + 2;
        map.SetCommittedOffset(newOffset);

        map.CommittedOffset.Should().Be(newOffset);
        map.OffsetMapDepth.Should().Be(depth - 2);
    }

    [Test]
    public void SetCommittedOffset_RecalculatesOffsetMapData()
    {
        var depth = 24;
        var offsetMap = new byte[] { 0b00000001, 0b00000101 };
        map.Set(offset, committedAt, depth, offsetMap);

        var newOffset = offset + 1;
        map.SetCommittedOffset(newOffset);
        
        var expectedOffsetMap = new byte[BytesCapacity];
        expectedOffsetMap[0] = 0b10000000;
        expectedOffsetMap[1] = 0b00000010;
        expectedOffsetMap[2] = 0b00000000;
        expectedOffsetMap[3] = 0b00000000;

        map.OffsetMapData.Should().BeEquivalentTo(expectedOffsetMap);
    }

    [Test(Description = "Пропуск сообщений должен помечать предыдущее смещение закоммиченным, если коммита нет и глубина равна 0")]
    public void MarkAsSkipped_CommitsPrevOffset_IfCommittedOffsetIsNotSetAndDepthIsZero()
    {
        map.MarkAsSkipped(offset + 1);
        map.CommittedOffset.Should().Be(offset);
    }

    [Test]
    public void MarkAsSkipped_IncrementsDepth()
    {
        map.SetCommittedOffset(offset);

        map.MarkAsSkipped(offset + 1);
        map.OffsetMapDepth.Should().Be(1);
    }

    [Test]
    public void MarkAsSkipped_IncrementsDepth2()
    {
        map.SetCommittedOffset(offset);

        map.MarkAsSkipped(offset + 3);
        map.OffsetMapDepth.Should().Be(3);
    }

    [Test]
    public void MarkAsProcessed_ThrowsException_IfCommittedOffsetIsNotSet()
    {
        var act = () => map.MarkAsProcessed(offset + 1);
        act.Should().Throw<InvalidOffsetException>();
    }

    [Test]
    public void MarkAsProcessed_IncrementsDepth()
    {
        map.SetCommittedOffset(offset);

        map.MarkAsProcessed(offset + 1);
        map.OffsetMapDepth.Should().Be(1);
    }

    [Test]
    public void MarkAsProcessed_IncrementsDepth2()
    {
        map.SetCommittedOffset(offset);

        map.MarkAsProcessed(offset + 10);
        map.OffsetMapDepth.Should().Be(10);
    }

    [Test]
    public void MarkAsProcessed_ChangesOffsetMapData()
    {
        map.SetCommittedOffset(offset);

        map.MarkAsProcessed(offset + 1);
        var expected = new byte[BytesCapacity];
        expected[0] = 1;
        map.OffsetMapData.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void MarkAsSkipped_ChangesOffsetMapData()
    {
        map.SetCommittedOffset(offset);

        map.MarkAsProcessed(offset + 1);
        map.MarkAsSkipped(offset + 1);
        var expected = new byte[BytesCapacity];
        expected[0] = 0;
        map.OffsetMapData.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void MarkAsProcessed_ChangesOffsetMapData2()
    {
        map.SetCommittedOffset(offset);

        map.MarkAsProcessed(offset + 11);
        var expected = new byte[BytesCapacity];
        expected[0] = 0;
        expected[1] = 0b00000100;
        map.OffsetMapData.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void MarkAsProcessed_ChangesOffsetMapData3()
    {
        map.SetCommittedOffset(offset);

        map.MarkAsSkipped(offset + 1);
        map.MarkAsProcessed(offset + 2);
        var expected = new byte[BytesCapacity];
        expected[0] = 0b00000010;
        map.OffsetMapData.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void IsMarkedAsProcessed_ReturnsFalse_IfCommittedOffsetIsNull()
    {
        map.IsMarkedAsProcessed(offset).Should().BeFalse("Если закоммиченное смещение неизвестно, все невозможно узнать, обработано ли сообщение. Считаем, что нет");
    }
    
    [Test]
    public void IsMarkedAsProcessed_ReturnsTrue_ForOffsetBeforeCommitted()
    {
        map.SetCommittedOffset(offset);
        map.IsMarkedAsProcessed(offset - 1).Should().BeTrue("Все сообщения до закоммиченного смещения являются обработанными");
    }

    [Test]
    public void IsMarkedAsProcessed_ReturnsTrue_ForCommittedOffset()
    {
        map.SetCommittedOffset(offset);
        map.IsMarkedAsProcessed(offset).Should().BeTrue("Сообщение на закомиченном смещении является обработанным");
    }

    [Test]
    public void IsMarkedAsProcessed_ReturnsFalse_ForOffsetThatIsNotMarkedAsProcessed()
    {
        map.SetCommittedOffset(offset);
        map.IsMarkedAsProcessed(offset+1).Should().BeFalse();
    }

    [Test]
    public void IsMarkedAsProcessed_ReturnsFalse_ForOffsetThatIsMarkedAsSkipped()
    {
        map.SetCommittedOffset(offset);
        map.MarkAsSkipped(offset + 1);
        map.IsMarkedAsProcessed(offset + 1).Should().BeFalse();
    }

    [Test]
    public void IsMarkedAsProcessed_ReturnsTrue_ForOffsetThatIsMarkedAsProcessed()
    {
        map.SetCommittedOffset(offset);
        map.MarkAsProcessed(offset + 1);
        map.IsMarkedAsProcessed(offset + 1).Should().BeTrue();
    }

    [Test]
    public void IsMarkedAsProcessed_ReturnsProperValue()
    {
        map.SetCommittedOffset(offset);
        map.MarkAsSkipped(offset + 1);
        map.MarkAsProcessed(offset + 2);
        map.IsMarkedAsProcessed(offset + 1).Should().BeFalse();
    }

    [Test]
    public void IsMarkedAsProcessed_ReturnsProperValue2()
    {
        map.SetCommittedOffset(offset);
        map.MarkAsSkipped(offset + 1);
        map.MarkAsProcessed(offset + 2);
        map.MarkAsSkipped(offset + 3);
        map.IsMarkedAsProcessed(offset + 2).Should().BeTrue();
    }

    [Test]
    public void HasAnySkippedMessageBefore_ReturnsFalse_IfCommittedOffsetIsNotSet()
    {
        map.HasAnySkippedMessageBefore(offset).Should().BeFalse("Если не знаем о коммите, то считаем, что пропущенных сообщений нет");
    }

    [Test]
    public void HasAnySkippedMessageBefore_ReturnsFalse_ForCommittedOffset()
    {
        map.SetCommittedOffset(offset);
        map.HasAnySkippedMessageBefore(offset).Should().BeFalse();
    }

    [Test]
    public void HasAnySkippedMessageBefore_ReturnsFalse_ForSkippedOffset()
    {
        map.SetCommittedOffset(offset);
        map.MarkAsSkipped(offset + 1);
        map.HasAnySkippedMessageBefore(offset + 1).Should().BeFalse();
    }

    [Test]
    public void HasAnySkippedMessageBefore_ReturnsTrue_ForOffsetAfterSkipped()
    {
        map.SetCommittedOffset(offset);
        map.MarkAsSkipped(offset + 1);
        map.HasAnySkippedMessageBefore(offset + 2).Should().BeTrue();
    }

    [Test]
    public void HasAnySkippedMessageBefore_ReturnsFalse_ForProcessedOffset()
    {
        map.SetCommittedOffset(offset);
        map.MarkAsProcessed(offset + 1);
        map.HasAnySkippedMessageBefore(offset + 1).Should().BeFalse();
    }

    [Test]
    public void HasAnySkippedMessageBefore_ReturnsFalse_ForOffsetAfterProcessed()
    {
        map.SetCommittedOffset(offset);
        map.MarkAsProcessed(offset + 1);
        map.HasAnySkippedMessageBefore(offset + 2).Should().BeFalse();
    }

    [Test]
    public void HasAnySkippedMessageBefore_ReturnsProperValue1()
    {
        map.SetCommittedOffset(offset);
        map.MarkAsProcessed(offset + 1);
        map.MarkAsProcessed(offset + 2);
        map.MarkAsSkipped(offset + 3);
        map.MarkAsProcessed(offset + 4);

        map.HasAnySkippedMessageBefore(offset + 5).Should().BeTrue();
    }

    [Test]
    public void HasAnySkippedMessageBefore_ReturnsProperValue_IfStateIsSetBySet1()
    {
        var depth = 1;
        var offsetMap = new byte[] { 0b00000000};
        map.Set(offset, committedAt, depth, offsetMap);

        map.HasAnySkippedMessageBefore(offset + 1).Should().BeFalse("Потому что перед этим смещением только закоммиченное");
    }

    [Test]
    public void HasAnySkippedMessageBefore_ReturnsProperValue_IfStateIsSetBySet2()
    {
        var depth = 1;
        var offsetMap = new byte[] { 0b00000000};
        map.Set(offset, committedAt, depth, offsetMap);

        map.HasAnySkippedMessageBefore(offset + 2).Should().BeTrue("Потому что одно сообщение пропущено");
    }

    [Test]
    public void HasAnySkippedMessageBefore_ReturnsProperValue_IfStateIsSetBySet3()
    {
        var depth = 1;
        var offsetMap = new byte[] { 0b00000010};
        map.Set(offset, committedAt, depth, offsetMap);

        map.HasAnySkippedMessageBefore(offset + 3).Should().BeTrue();
    }

    [Test]
    public void HasAnySkippedMessageBefore_ReturnsProperValue2()
    {
        map.SetCommittedOffset(offset);
        map.MarkAsProcessed(offset + 1);
        map.MarkAsProcessed(offset + 2);
        map.MarkAsSkipped(offset + 3);

        map.HasAnySkippedMessageBefore(offset + 4).Should().BeTrue();
    }

    [Test]
    public void CountSkippedNotAfter_ReturnsZero_ByDefault()
    {
        map.CountSkippedNotAfter(offset).Should().Be(0);
    }

    [Test]
    public void CountSkippedNotAfter_ReturnsZero_ForOffsetBeforeCommitted()
    {
        map.SetCommittedOffset(offset);
        map.CountSkippedNotAfter(offset-1).Should().Be(0);
    }

    [Test]
    public void CountSkippedNotAfter_ReturnsZero_ForCommittedOffset()
    {
        map.SetCommittedOffset(offset);
        map.CountSkippedNotAfter(offset).Should().Be(0);
    }

    [Test]
    public void CountSkippedNotAfter_CountsCurrentOffset_IfItIsSkipped()
    {
        map.SetCommittedOffset(offset);
        map.MarkAsSkipped(offset + 1);
        map.CountSkippedNotAfter(offset + 1).Should().Be(1);
    }

    [Test]
    public void CountSkippedNotAfter_CountsCurrentOffset_IfItIsSkipped2()
    {
        map.SetCommittedOffset(offset);
        map.MarkAsSkipped(offset + 1);
        map.MarkAsSkipped(offset + 2);
        map.CountSkippedNotAfter(offset + 2).Should().Be(2);
    }

    [Test]
    public void CountSkippedNotAfter_ReturnsProperValue1()
    {
        map.SetCommittedOffset(offset);
        map.MarkAsProcessed(offset + 1);
        map.MarkAsProcessed(offset + 2);
        map.MarkAsProcessed(offset + 3);
        map.CountSkippedNotAfter(offset + 3).Should().Be(0);
    }

    [Test]
    public void CountSkippedNotAfter_ReturnsProperValue2()
    {
        map.SetCommittedOffset(offset);
        map.MarkAsProcessed(offset + 1);
        map.MarkAsProcessed(offset + 2);
        map.MarkAsSkipped(offset + 3);
        map.CountSkippedNotAfter(offset + 3).Should().Be(1);
    }

    [Test]
    public void CountSkippedNotAfter_ReturnsProperValue3()
    {
        map.SetCommittedOffset(offset);
        map.MarkAsSkipped(offset + 1);
        map.MarkAsSkipped(offset + 2);
        map.MarkAsProcessed(offset + 3);
        map.CountSkippedNotAfter(offset + 3).Should().Be(2);
    }
}
