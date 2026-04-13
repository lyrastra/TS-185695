using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Extensions;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Helpers;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Tests.TestingTypes;

namespace Moedelo.Infrastructure.SqlDataAccess.Abstractions.Tests;

[TestFixture]
public class TypeExtensionsTests
{
    [Test]
    [Description("Проверяет, что базовые типы C# (int, string, bool) корректно сопоставляются с их SQL-аналогами с правильной поддержкой null")]
    public void GetDataColumnList_BasicTypes_ReturnsCorrectSqlMapping()
    {
        // Arrange
        var type = typeof(BasicTypesModel);

        // Act
        var columns = type.GetDataColumnList();

        // Assert
        columns.Length.ShouldBe(3);

        var intColumn = columns.Single(c => c.PropertyName == nameof(BasicTypesModel.IntProperty));
        intColumn.ColumnDeclaration.ShouldContain("int not null");
        intColumn.DataColumn.DataType.ShouldBe(typeof(int));

        var stringColumn = columns.Single(c => c.PropertyName == nameof(BasicTypesModel.StringProperty));
        stringColumn.ColumnDeclaration.ShouldContain("varchar(max) null");
        stringColumn.DataColumn.DataType.ShouldBe(typeof(string));

        var boolColumn = columns.Single(c => c.PropertyName == nameof(BasicTypesModel.BoolProperty));
        boolColumn.ColumnDeclaration.ShouldContain("bit not null");
        boolColumn.DataColumn.DataType.ShouldBe(typeof(bool));
    }

    [Test]
    [Description("Обеспечивает, что nullable-типы (int?, string?, DateTime?) сопоставляются с SQL-типами с ограничением 'null'")]
    public void GetDataColumnList_NullableTypes_ReturnsCorrectNullableMapping()
    {
        // Arrange
        var type = typeof(NullableTypesModel);

        // Act
        var columns = type.GetDataColumnList();

        // Assert
        columns.Length.ShouldBe(3);

        var nullableIntColumn = columns.Single(c => c.PropertyName == nameof(NullableTypesModel.NullableInt));
        nullableIntColumn.ColumnDeclaration.ShouldContain("int null");
        nullableIntColumn.DataColumn.DataType.ShouldBe(typeof(int));

        var nullableStringColumn = columns.Single(c => c.PropertyName == nameof(NullableTypesModel.NullableString));
        nullableStringColumn.ColumnDeclaration.ShouldContain("varchar(max) null");
        nullableStringColumn.DataColumn.DataType.ShouldBe(typeof(string));

        var nullableDateTimeColumn = columns.Single(c => c.PropertyName == nameof(NullableTypesModel.NullableDateTime));
        nullableDateTimeColumn.ColumnDeclaration.ShouldContain("datetime2 null");
        nullableDateTimeColumn.DataColumn.DataType.ShouldBe(typeof(DateTime));
    }

    [Test]
    [Description("Проверяет, что колонки возвращаются в точном порядке, указанном параметром propertyOrder")]
    public void GetDataColumnList_WithPropertyOrder_ReturnsOrderedColumns()
    {
        // Arrange
        var type = typeof(BasicTypesModel);
        var propertyOrder = new[] 
        { 
            nameof(BasicTypesModel.StringProperty), 
            nameof(BasicTypesModel.BoolProperty), 
            nameof(BasicTypesModel.IntProperty) 
        };

        // Act
        var columns = type.GetDataColumnList(propertyOrder);

        // Assert
        columns[0].PropertyName.ShouldBe(nameof(BasicTypesModel.StringProperty));
        columns[1].PropertyName.ShouldBe(nameof(BasicTypesModel.BoolProperty));
        columns[2].PropertyName.ShouldBe(nameof(BasicTypesModel.IntProperty));
    }

    [Test]
    [Description("Проверяет, что при частичном порядке свойств неупорядоченные свойства добавляются в конец")]
    public void GetDataColumnList_WithPartialOrder_AppendsUnorderedColumns()
    {
        // Arrange
        var type = typeof(BasicTypesModel);
        var propertyOrder = new[] { nameof(BasicTypesModel.StringProperty) };

        // Act
        var columns = type.GetDataColumnList(propertyOrder);

        // Assert
        columns[0].PropertyName.ShouldBe(nameof(BasicTypesModel.StringProperty));
        var remainingColumns = columns.Skip(1).Select(c => c.PropertyName).ToList();
        remainingColumns.ShouldContain(nameof(BasicTypesModel.IntProperty));
        remainingColumns.ShouldContain(nameof(BasicTypesModel.BoolProperty));
    }

    [Test]
    [Description("Подтверждает, что последующие вызовы с тем же типом возвращают кэшированные определения колонок")]
    public void GetDataColumnList_SameType_ReturnsCachedInstance()
    {
        // Arrange
        var type = typeof(BasicTypesModel);

        // Act
        var firstCall = type.GetDataColumnList();
        var secondCall = type.GetDataColumnList();

        // Assert
        secondCall.ShouldBeSameAs(firstCall);
    }

    [Test]
    [Description("Проверяет сопоставление специальных типов (DateTime, Guid, Enum) с их SQL-эквивалентами")]
    public void GetDataColumnList_SpecialTypes_ReturnsCorrectMapping()
    {
        // Arrange
        var type = typeof(SpecialTypesModel);

        // Act
        var columns = type.GetDataColumnList();

        // Assert
        columns.Length.ShouldBe(3);

        var dateTimeColumn = columns.Single(c => c.PropertyName == nameof(SpecialTypesModel.DateTimeProperty));
        dateTimeColumn.ColumnDeclaration.ShouldContain("datetime2 not null");
        dateTimeColumn.DataColumn.DataType.ShouldBe(typeof(DateTime));

        var guidColumn = columns.Single(c => c.PropertyName == nameof(SpecialTypesModel.GuidProperty));
        guidColumn.ColumnDeclaration.ShouldContain("uniqueidentifier not null");
        guidColumn.DataColumn.DataType.ShouldBe(typeof(Guid));

        var enumColumn = columns.Single(c => c.PropertyName == nameof(SpecialTypesModel.EnumProperty));
        enumColumn.ColumnDeclaration.ShouldContain("int not null");
        enumColumn.DataColumn.DataType.ShouldBe(typeof(int));
    }

    [Test]
    [Description("Обеспечивает, что свойства DataColumn (ColumnName, AllowDBNull) устанавливаются согласно характеристикам свойства")]
    public void GetDataColumnList_DataColumnProperties_SetCorrectly()
    {
        // Arrange
        var type = typeof(BasicTypesModel);

        // Act
        var columns = type.GetDataColumnList();

        // Assert
        foreach (var column in columns)
        {
            column.DataColumn.ColumnName.ShouldBe(column.PropertyName);
            column.DataColumn.AllowDBNull.ShouldBe(column.PropertyInfo.PropertyType.CanBeNull());
        }
    }

    [Test]
    [Description("Проверяет, что тип без публичных свойств возвращает пустой массив")]
    public void GetDataColumnList_EmptyType_ReturnsEmptyArray()
    {
        // Arrange
        var type = typeof(EmptyModel);

        // Act
        var columns = type.GetDataColumnList();

        // Assert
        columns.ShouldBeEmpty();
    }

    [Test]
    [Description("Обеспечивает корректную обработку, когда порядок свойств содержит несуществующие имена свойств")]
    public void GetDataColumnList_InvalidPropertyOrder_HandlesGracefully()
    {
        // Arrange
        var type = typeof(BasicTypesModel);
        var propertyOrder = new[] { "NonExistentProperty" };

        // Act
        var columns = type.GetDataColumnList(propertyOrder);

        // Assert
        columns.Length.ShouldBe(3);
        var columnNames = columns.Select(c => c.PropertyName).ToList();
        columnNames.ShouldContain(nameof(BasicTypesModel.IntProperty));
        columnNames.ShouldContain(nameof(BasicTypesModel.StringProperty));
        columnNames.ShouldContain(nameof(BasicTypesModel.BoolProperty));
    }
}
