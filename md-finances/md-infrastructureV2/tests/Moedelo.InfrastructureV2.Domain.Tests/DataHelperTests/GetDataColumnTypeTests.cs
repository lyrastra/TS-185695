using FluentAssertions;
using Moedelo.InfrastructureV2.Domain.Helpers;
using NUnit.Framework;

namespace Moedelo.InfrastructureV2.Domain.Tests.DataHelperTests;

[TestFixture, Parallelizable(ParallelScope.All)]
public class GetDataColumnTypeTests
{
    public enum TestByteEnum : byte
    {
        Value1, Value2
    }

    public enum TestEnum
    {
        Value1 = 0, Value2 = int.MaxValue
    }

    private class TestingType
    {
        public long? NullableLong { get; set; }
        public long NonNullableLong { get; set; }
        public TestEnum NonNullableEnum { get; set; }
        public TestEnum? NullableEnum { get; set; }
        public TestByteEnum NonNullableByteEnum { get; set; }
        public TestByteEnum? NullableByteEnum { get; set; }
        public string String { get; set; }
    }

    [Test]
    public void Test_NonNullableLong()
    {
        var propertyInfo = typeof(TestingType).GetProperty(nameof(TestingType.NonNullableLong));

        propertyInfo.GetDataColumnType().Should().Be(new DataColumnType(typeof(long), false));
    }

    [Test]
    public void Test_NullableLong()
    {
        var propertyInfo = typeof(TestingType).GetProperty(nameof(TestingType.NullableLong));

        propertyInfo.GetDataColumnType().Should().Be(new DataColumnType(typeof(long), true));
    }

    [Test]
    public void Test_NonNullableEnum()
    {
        var propertyInfo = typeof(TestingType).GetProperty(nameof(TestingType.NonNullableEnum));

        propertyInfo.GetDataColumnType().Should().Be(new DataColumnType(typeof(int), false));
    }

    [Test]
    public void Test_NullableEnum()
    {
        var propertyInfo = typeof(TestingType).GetProperty(nameof(TestingType.NullableEnum));

        propertyInfo.GetDataColumnType().Should().Be(new DataColumnType(typeof(int), true));
    }

    [Test]
    public void Test_NonNullableByteEnum()
    {
        var propertyInfo = typeof(TestingType).GetProperty(nameof(TestingType.NonNullableByteEnum));

        propertyInfo.GetDataColumnType().Should().Be(new DataColumnType(typeof(byte), false));
    }

    [Test]
    public void Test_NullableByteEnum()
    {
        var propertyInfo = typeof(TestingType).GetProperty(nameof(TestingType.NullableByteEnum));

        propertyInfo.GetDataColumnType().Should().Be(new DataColumnType(typeof(byte), true));
    }

    [Test]
    public void Test_String()
    {
        var propertyInfo = typeof(TestingType).GetProperty(nameof(TestingType.String));

        propertyInfo.GetDataColumnType().Should().Be(new DataColumnType(typeof(string), true));
    }
}