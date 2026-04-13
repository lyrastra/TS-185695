using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moedelo.CommonV2.Extensions.System;
using NUnit.Framework;

namespace UtilsTests
{
    [TestFixture]
    public class EnumerableChunkExTest
    {
        private static List<int> CreateList(int len)
        {
            return Enumerable.Range(0, len).ToList();
        }

        [Test]
        public void Chunk_ReturnsCorrectChunks_ByComparer()
        {
            var list = new[] {"aa", "bb", "cccc", "dddd", "eeee", "ff", "gg"};
            var result = list.Chunk((a, b) => a.Length == b.Length).ToList();

            result.Should().HaveCount(3);
            result[0].Should().BeEquivalentTo(new[] {"aa", "bb"});
            result[1].Should().BeEquivalentTo(new[] {"cccc", "dddd", "eeee"});
            result[2].Should().BeEquivalentTo(new[] {"ff", "gg"});
        }

        [Test]
        public void Chunk_ReturnsCorrectChunks_IfRequestedChunkSizeLessThenInputSequenceSize()
        {
            var list = CreateList(10);

            var result = list.Chunk(5).ToList();

            result.Should().HaveCount(2);

            result[0].Should().HaveCount(5);
            result[1].Should().HaveCount(5);

            result[0].Should().BeEquivalentTo(new []{0, 1, 2, 3, 4});
            result[1].Should().BeEquivalentTo(new []{5, 6, 7, 8, 9});
        }

        [Test]
        public void Chunk_ReturnsCorrectChunks_IfRequestedChunkSizeLessThenInputSequenceSizeV2()
        {
            var list = CreateList(12);

            var result = list.Chunk(5).ToList();

            result.Should().HaveCount(3);

            result[0].Should().HaveCount(5);
            result[1].Should().HaveCount(5);
            result[2].Should().HaveCount(2);

            result[0].Should().BeEquivalentTo(new []{0, 1, 2, 3, 4});
            result[1].Should().BeEquivalentTo(new []{5, 6, 7, 8, 9});
            result[2].Should().BeEquivalentTo(new []{10, 11});
        }

        [Test]
        public void Chunk_ReturnsEmptyEnumerable_ForEmptyInput()
        {
            var list = new List<int>();
            var result = list.Chunk(1);
            result.Should().BeEmpty();
        }

        [Test]
        public void Chunk_ReturnsInputSequence_IfRequestedChunkSizeNotAboveInputSequenceSize()
        {
            var list = CreateList(10);

            var result = list.Chunk(10).ToList();
            result.Should().HaveCount(1);
            result[0].Should().BeEquivalentTo(list);

            result = list.Chunk(11).ToList();
            result.Should().HaveCount(1);
            result[0].Should().BeEquivalentTo(list);
        }

        [Test]
        public void Chunk_ThrowsArgumentNullException_ForNullInput()
        {
            List<int> list = null;

            Action act = () => list.Chunk(100).ToList();
            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Chunk_ThrowsArgumentOutOfRangeException_ForZeroChunkLength()
        {
            var list = new List<int>();

            Action act = () => list.Chunk(0).ToList();

            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        private class TestEnumerable
        {
            public int CallsCount { get; private set; }
            public int[] Source { get; private set; }

            public TestEnumerable(params int[] numbers)
            {
                Source = numbers;
            }

            public IEnumerable<int> Next()
            {
                while (CallsCount < Source.Length)
                {
                    yield return Source[CallsCount++];
                }
            }
        }

        [Test(Description = "Перебор исходной последовательности не происходит до момента обращения к элементу (не происходит при вызове Chunk)")]
        public void Chunk_DoesNotGetEnumerableElementsUntilItBeCalledExplicetly()
        {
            var arr = new TestEnumerable(6,5,4,3,2);

            var chunked = arr.Next().Chunk(2);

            arr.CallsCount.Should().Be(0);
        }
        
        [Test(Description = "Перебор исходной последовательности происходит непосредственно в момент обращения к элементу")]
        public void Chunk_EnumeratesElementsJustInMomentOfPeekingThisElement()
        {
            var arr = new TestEnumerable(6,5,4,3,2);

            const int chunkSize = 2;

            // бьём на подпоследовательности (реально ничего не происходит, просто определяется правило разбиения)
            var chunked = arr.Next().Chunk(chunkSize);

            // непосредственный перебор элементов первой подпоследовательности
            var first = chunked.First().ToList();
            
            arr.CallsCount.Should().Be(chunkSize+1);
            first.Count.Should().Be(chunkSize);
        }
    }
}