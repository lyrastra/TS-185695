using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Helpers;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Tests.TestingTypes;

namespace Moedelo.Infrastructure.SqlDataAccess.Abstractions.Tests;

[TestFixture]
public class SqlHelperTests
{
    private static readonly object[] RegularTypesTestCases = 
    {
        new object[] { typeof(int), typeof(int) },
        new object[] { typeof(string), typeof(string) },
        new object[] { typeof(bool), typeof(bool) },
        new object[] { typeof(DateTime), typeof(DateTime) },
        new object[] { typeof(Guid), typeof(Guid) },
        new object[] { typeof(decimal), typeof(decimal) },
        new object[] { typeof(float), typeof(float) },
        new object[] { typeof(double), typeof(double) },
        new object[] { typeof(byte), typeof(byte) },
        new object[] { typeof(short), typeof(short) },
        new object[] { typeof(long), typeof(long) },
        new object[] { typeof(TimeSpan), typeof(TimeSpan) },
        new object[] { typeof(byte[]), typeof(byte[]) },
        new object[] { typeof(char[]), typeof(char[]) }
    };

    private static readonly object[] NullableTypesTestCases = 
    {
        new object[] { typeof(int?), typeof(int) },
        new object[] { typeof(bool?), typeof(bool) },
        new object[] { typeof(DateTime?), typeof(DateTime) },
        new object[] { typeof(Guid?), typeof(Guid) },
        new object[] { typeof(decimal?), typeof(decimal) },
        new object[] { typeof(float?), typeof(float) },
        new object[] { typeof(double?), typeof(double) },
        new object[] { typeof(byte?), typeof(byte) },
        new object[] { typeof(short?), typeof(short) },
        new object[] { typeof(long?), typeof(long) },
        new object[] { typeof(TimeSpan?), typeof(TimeSpan) }
    };

    [Test]
    [TestCaseSource(nameof(RegularTypesTestCases))]
    [Description("Проверяет, что обычные типы возвращаются без изменений")]
    public void GetColumnDataType_RegularTypes_ReturnsSameType(Type inputType, Type expectedType)
    {
        // Act
        var result = inputType.GetColumnDataType();

        // Assert
        result.ShouldBe(expectedType);
    }

    [Test]
    [TestCaseSource(nameof(NullableTypesTestCases))]
    [Description("Проверяет, что nullable-типы возвращают базовый тип")]
    public void GetColumnDataType_NullableTypes_ReturnsUnderlyingType(Type inputType, Type expectedType)
    {
        // Act
        var result = inputType.GetColumnDataType();

        // Assert
        result.ShouldBe(expectedType);
    }

    private static readonly object[] EnumTypesTestCases = 
    {
        new object[] { typeof(TestEnum), typeof(int) },
        new object[] { typeof(ByteEnum), typeof(byte) },
        new object[] { typeof(LongEnum), typeof(long) }
    };

    private static readonly object[] NullableEnumTypesTestCases = 
    {
        new object[] { typeof(TestEnum?), typeof(int) },
        new object[] { typeof(ByteEnum?), typeof(byte) },
        new object[] { typeof(LongEnum?), typeof(long) }
    };

    [Test]
    [TestCaseSource(nameof(EnumTypesTestCases))]
    [Description("Проверяет, что enum-типы возвращают их базовый тип")]
    public void GetColumnDataType_EnumTypes_ReturnsUnderlyingType(Type inputType, Type expectedType)
    {
        // Act
        var result = inputType.GetColumnDataType();

        // Assert
        result.ShouldBe(expectedType);
    }

    [Test]
    [TestCaseSource(nameof(NullableEnumTypesTestCases))]
    [Description("Проверяет, что nullable enum-типы возвращают базовый тип enum")]
    public void GetColumnDataType_NullableEnumTypes_ReturnsUnderlyingType(Type inputType, Type expectedType)
    {
        // Act
        var result = inputType.GetColumnDataType();

        // Assert
        result.ShouldBe(expectedType);
    }

    [Test]
    [Description("Проверяет, что вложенные nullable-типы корректно обрабатываются")]
    public void GetColumnDataType_NestedNullableTypes_ReturnsCorrectUnderlyingType()
    {
        // Arrange
        var nullableIntType = typeof(int?);

        // Act & Assert
        // Проверяем, что метод корректно обрабатывает только один уровень nullable
        nullableIntType.GetColumnDataType().ShouldBe(typeof(int));
        
        // В C# нет вложенных nullable типов (int?? не существует)
        // Метод должен корректно обрабатывать только один уровень Nullable<>
    }

    [Test]
    [Description("Проверяет, что сложные generic-типы, не являющиеся Nullable, возвращаются без изменений")]
    public void GetColumnDataType_NonNullableGenericTypes_ReturnsSameType()
    {
        // Arrange & Act & Assert
        typeof(List<int>).GetColumnDataType().ShouldBe(typeof(List<int>));
        typeof(Dictionary<string, int>).GetColumnDataType().ShouldBe(typeof(Dictionary<string, int>));
        typeof(IEnumerable<string>).GetColumnDataType().ShouldBe(typeof(IEnumerable<string>));
    }

    private static readonly object[] CanBeNullTestCases = 
    {
        // Nullable value types - should return true
        new object[] { typeof(int?), true },
        new object[] { typeof(bool?), true },
        new object[] { typeof(DateTime?), true },
        new object[] { typeof(Guid?), true },
        new object[] { typeof(decimal?), true },
        new object[] { typeof(float?), true },
        new object[] { typeof(double?), true },
        new object[] { typeof(byte?), true },
        new object[] { typeof(short?), true },
        new object[] { typeof(long?), true },
        new object[] { typeof(TimeSpan?), true },
        
        // String type - should return true
        new object[] { typeof(string), true },
        
        // Non-nullable value types - should return false
        new object[] { typeof(int), false },
        new object[] { typeof(bool), false },
        new object[] { typeof(DateTime), false },
        new object[] { typeof(Guid), false },
        new object[] { typeof(decimal), false },
        new object[] { typeof(float), false },
        new object[] { typeof(double), false },
        new object[] { typeof(byte), false },
        new object[] { typeof(short), false },
        new object[] { typeof(long), false },
        new object[] { typeof(TimeSpan), false },
        
        // Reference types (except string) - should return false
        new object[] { typeof(object), false },
        new object[] { typeof(List<int>), false },
        new object[] { typeof(Dictionary<string, int>), false },
        new object[] { typeof(IEnumerable<string>), false },
        new object[] { typeof(byte[]), false },
        new object[] { typeof(char[]), false },
        
        // Enum types - should return false
        new object[] { typeof(TestEnum), false },
        new object[] { typeof(ByteEnum), false },
        new object[] { typeof(LongEnum), false },
        
        // Nullable enum types - should return true
        new object[] { typeof(TestEnum?), true },
        new object[] { typeof(ByteEnum?), true },
        new object[] { typeof(LongEnum?), true }
    };

    [Test]
    [TestCaseSource(nameof(CanBeNullTestCases))]
    [Description("Проверяет, что метод CanBeNull корректно определяет возможность null для различных типов")]
    public void CanBeNull_VariousTypes_ReturnsCorrectResult(Type inputType, bool expectedResult)
    {
        // Act
        var result = inputType.CanBeNull();

        // Assert
        result.ShouldBe(expectedResult);
    }

    [Test]
    [Description("Проверяет, что метод CanBeNull выбрасывает ArgumentNullException при null входном параметре")]
    public void CanBeNull_NullInput_ThrowsArgumentNullException()
    {
        // Arrange
        Type? nullType = null;

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => nullType!.CanBeNull());
    }

    [Test]
    [Description("Проверяет, что метод CanBeNull корректно работает с generic-типами")]
    public void CanBeNull_GenericTypes_ReturnsCorrectResult()
    {
        // Arrange & Act & Assert
        // Generic типы, не являющиеся Nullable<>
        typeof(List<int>).CanBeNull().ShouldBeFalse();
        typeof(Dictionary<string, int>).CanBeNull().ShouldBeFalse();
        typeof(IEnumerable<string>).CanBeNull().ShouldBeFalse();
        
        // Nullable generic типы
        typeof(Nullable<int>).CanBeNull().ShouldBeTrue();
        typeof(Nullable<bool>).CanBeNull().ShouldBeTrue();
    }

    [Test]
    [Description("Проверяет, что метод CanBeNull корректно работает с интерфейсами")]
    public void CanBeNull_Interfaces_ReturnsCorrectResult()
    {
        // Arrange & Act & Assert
        typeof(IEnumerable<int>).CanBeNull().ShouldBeFalse();
        typeof(IDisposable).CanBeNull().ShouldBeFalse();
        typeof(IComparable).CanBeNull().ShouldBeFalse();
    }

    [Test]
    [Description("Проверяет, что метод CanBeNull корректно работает с массивами")]
    public void CanBeNull_Arrays_ReturnsCorrectResult()
    {
        // Arrange & Act & Assert
        typeof(int[]).CanBeNull().ShouldBeFalse();
        typeof(string[]).CanBeNull().ShouldBeFalse();
        typeof(byte[]).CanBeNull().ShouldBeFalse();
        typeof(char[]).CanBeNull().ShouldBeFalse();
    }

}
