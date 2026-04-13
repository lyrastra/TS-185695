using System;
using FluentAssertions;
using Moedelo.InfrastructureV2.Domain.Helpers;
using NUnit.Framework;

namespace Moedelo.InfrastructureV2.Domain.Tests.DataHelperTests;

[TestFixture, Parallelizable(ParallelScope.All)]
public class ToListTvpTests
{
    private class ClassWithPublicProps
    {
        public ClassWithPublicProps()
        {
        }

        public ClassWithPublicProps(long longValue, long? nullableLong)
        {
            Long = longValue;
            NullableLong = nullableLong;
        }

        public long Long { get; set; }
        public long? NullableLong { get; set; }
    }

    private class SubClassWithNonPublicProps : ClassWithPublicProps
    {
        internal long InternalProp { get; set; }
    }

    [Test]
    public void ToListTvp_SetsTableName()
    {
        const string tableName = "SomeTableName";
        var tvp = Array.Empty<ClassWithPublicProps>().ToListTvp(tableName);

        tvp.TableName.Should().Be(tableName);
    }

    [Test]
    public void ToListTvp_CreatesColumnForEachPublicProperty()
    {
        const int numOfPublicPropsInType1 = 2;
        var tvp = Array.Empty<ClassWithPublicProps>().ToListTvp();

        tvp.Columns.Count.Should().Be(numOfPublicPropsInType1);
    }

    [Test]
    public void ToListTvp_SetAllowDbNullFalseForNonNullableProperty()
    {
        var tvp = Array.Empty<ClassWithPublicProps>().ToListTvp();
        tvp.Columns[nameof(ClassWithPublicProps.Long)].AllowDBNull.Should().BeFalse();
    }

    [Test]
    public void ToListTvp_SetAllowDbNullTrueForNullableProperty()
    {
        var tvp = Array.Empty<ClassWithPublicProps>().ToListTvp();
        tvp.Columns[nameof(ClassWithPublicProps.NullableLong)].AllowDBNull.Should().BeTrue();
    }

    [Test]
    public void ToListTvp_DoesNotCreateColumnForNonPublicProperties()
    {
        const int numOfPublicPropsInType1 = 2;
        var tvp = Array.Empty<SubClassWithNonPublicProps>().ToListTvp();

        tvp.Columns.Count.Should().Be(numOfPublicPropsInType1);
    }

    [Test]
    public void ToListTvp_FillsRows()
    {
        var values = new[] { new ClassWithPublicProps(10, 20), new ClassWithPublicProps(30, 40) };

        var tvp = values.ToListTvp();
        tvp.Rows.Count.Should().Be(values.Length);
        
        tvp.Rows[0][0].Should().Be(10);
        tvp.Rows[0][1].Should().Be(20);
        tvp.Rows[1][0].Should().Be(30);
        tvp.Rows[1][1].Should().Be(40);
    }

    [Test]
    public void ToListTvp_FillsRowsWithNullInNullableFields()
    {
        var values = new[] { new ClassWithPublicProps(11, null), new ClassWithPublicProps(12, null) };

        var tvp = values.ToListTvp();
        tvp.Rows.Count.Should().Be(values.Length);

        tvp.Rows[0][0].Should().Be(11);
        tvp.Rows[0][1].Should().BeOfType<DBNull>();
        tvp.Rows[1][0].Should().Be(12);
        tvp.Rows[1][1].Should().BeOfType<DBNull>();
    }
}
