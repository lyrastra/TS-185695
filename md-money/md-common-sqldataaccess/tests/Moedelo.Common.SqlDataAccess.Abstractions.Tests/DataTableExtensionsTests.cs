using System.Data;
using FluentAssertions;
using Moedelo.Common.SqlDataAccess.Abstractions.Extensions;

namespace Moedelo.Common.SqlDataAccess.Abstractions.Tests;

public class DataTableExtensionsTests
{
    [Test]
    public void ReturnsNull_IfTableIsNull()
    {
        DataTable? table = default;

        var actual = table.DumpDataTableRows();

        actual.Should().BeNull();
    }

    [Test]
    public void ReturnsEmptyArray_IfTableIsEmpty()
    {
        using var table = new DataTable("testTable");

        var actual = table.DumpDataTableRows();

        actual.Should().BeEmpty();
    }

    [Test]
    public void ReturnsArrayWithValues_IfTableHasOneColumn()
    {
        using var table = new DataTable("testTable");
        table.Columns.Add("Column1", typeof(long));
        table.Rows.Add(2);
        table.Rows.Add(4);
        table.Rows.Add(5);
        
        var actual = table.DumpDataTableRows();
        actual.Should().BeEquivalentTo(new[] { 2, 4, 5 });
    }
    
    [Test]
    public void ReturnsArrayOfListsOfValues_IfTableHasMoreThanOneColumns()
    {
        using var table = new DataTable("testTable");
        table.Columns.Add("Column1", typeof(long));
        table.Columns.Add("Column2", typeof(string));
        table.Rows.Add(2, "two");
        table.Rows.Add(4, "four");
        table.Rows.Add(5, "five");
        
        var actual = table.DumpDataTableRows();
        actual.Should().BeEquivalentTo(new[] { new object[]{2, "two"}, new object[]{4, "four"}, new object[]{5, "five"} });
    }

    [Test]
    public void ReturnsNoMoreRowsThanLimit_IfLimitISSpecified()
    {
        var values = Enumerable.Range(10, 100).ToArray();
        const int limit = 14;
        var expected = values.Take(limit);
        
        using var table = new DataTable("testTable");
        table.Columns.Add("Column1", typeof(long));
        foreach (var value in values)
        {
            table.Rows.Add(value);
        }
        
        var actual = table.DumpDataTableRows(limit);
        actual.Should().BeEquivalentTo(expected);
    }
}
