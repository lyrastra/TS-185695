using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Helpers;

namespace Moedelo.Infrastructure.SqlDataAccess.Abstractions.Tests;

[TestFixture]
public class SqlQueryBuilderTests
{
    [Test]
    [Description("Проверяет, что конструктор выбрасывает исключение при null sqlTemplate")]
    public void Constructor_NullSqlTemplate_ThrowsArgumentException()
    {
        // Act & Assert
        Should.Throw<ArgumentException>(() => new SqlQueryBuilder(null!));
    }

    [Test]
    [Description("Проверяет, что конструктор выбрасывает исключение при пустом sqlTemplate")]
    public void Constructor_EmptySqlTemplate_ThrowsArgumentException()
    {
        // Act & Assert
        Should.Throw<ArgumentException>(() => new SqlQueryBuilder(string.Empty));
    }

    [Test]
    [Description("Проверяет, что конструктор выбрасывает исключение при sqlTemplate из пробелов")]
    public void Constructor_WhitespaceSqlTemplate_ThrowsArgumentException()
    {
        // Act & Assert
        Should.Throw<ArgumentException>(() => new SqlQueryBuilder("   "));
    }

    [Test]
    [Description("Проверяет, что конструктор корректно инициализирует объект с валидным sqlTemplate")]
    public void Constructor_ValidSqlTemplate_InitializesCorrectly()
    {
        // Arrange
        const string sqlTemplate = "SELECT * FROM Users";

        // Act
        var builder = new SqlQueryBuilder(sqlTemplate);

        // Assert
        builder.ToString().ShouldBe(sqlTemplate);
    }

    [Test]
    [Description("Проверяет, что IncludeLine корректно удаляет комментарий --lineName--")]
    public void IncludeLine_ValidLineName_RemovesComment()
    {
        // Arrange
        const string sqlTemplate = "SELECT * FROM Users --whereClause-- WHERE Id = 1";
        var builder = new SqlQueryBuilder(sqlTemplate);

        // Act
        var result = builder.IncludeLine("whereClause").ToString();

        // Assert
        result.ShouldBe("SELECT * FROM Users  WHERE Id = 1");
    }


    [Test]
    [Description("Проверяет, что IncludeLineIf включает строку при true условии")]
    public void IncludeLineIf_TrueCondition_IncludesLine()
    {
        // Arrange
        const string sqlTemplate = "SELECT * FROM Users --whereClause-- WHERE Id = 1";
        var builder = new SqlQueryBuilder(sqlTemplate);

        // Act
        var result = builder.IncludeLineIf("whereClause", true).ToString();

        // Assert
        result.ShouldBe("SELECT * FROM Users  WHERE Id = 1");
    }

    [Test]
    [Description("Проверяет, что IncludeLineIf не включает строку при false условии")]
    public void IncludeLineIf_FalseCondition_DoesNotIncludeLine()
    {
        // Arrange
        const string sqlTemplate = "SELECT * FROM Users --whereClause-- WHERE Id = 1";
        var builder = new SqlQueryBuilder(sqlTemplate);

        // Act
        var result = builder.IncludeLineIf("whereClause", false).ToString();

        // Assert
        result.ShouldBe("SELECT * FROM Users --whereClause-- WHERE Id = 1");
    }

    [Test]
    [Description("Проверяет, что IncludeBlock корректно удаляет многострочный комментарий")]
    public void IncludeBlock_ValidBlockName_RemovesComment()
    {
        // Arrange
        const string sqlTemplate = "SELECT * FROM Users /*--whereBlock-- WHERE Id = 1 AND Name = 'Test' --whereBlock--*/";
        var builder = new SqlQueryBuilder(sqlTemplate);

        // Act
        var result = builder.IncludeBlock("whereBlock").ToString();

        // Assert
        result.ShouldBe("SELECT * FROM Users  WHERE Id = 1 AND Name = 'Test' ");
    }


    [Test]
    [Description("Проверяет, что IncludeBlockIf включает блок при true условии")]
    public void IncludeBlockIf_TrueCondition_IncludesBlock()
    {
        // Arrange
        const string sqlTemplate = "SELECT * FROM Users /*--whereBlock-- WHERE Id = 1 --whereBlock--*/";
        var builder = new SqlQueryBuilder(sqlTemplate);

        // Act
        var result = builder.IncludeBlockIf("whereBlock", true).ToString();

        // Assert
        result.ShouldBe("SELECT * FROM Users  WHERE Id = 1 ");
    }

    [Test]
    [Description("Проверяет, что IncludeBlockIf не включает блок при false условии")]
    public void IncludeBlockIf_FalseCondition_DoesNotIncludeBlock()
    {
        // Arrange
        const string sqlTemplate = "SELECT * FROM Users /*--whereBlock-- WHERE Id = 1 --whereBlock--*/";
        var builder = new SqlQueryBuilder(sqlTemplate);

        // Act
        var result = builder.IncludeBlockIf("whereBlock", false).ToString();

        // Assert
        result.ShouldBe("SELECT * FROM Users /*--whereBlock-- WHERE Id = 1 --whereBlock--*/");
    }

    [Test]
    [Description("Проверяет, что ReplaceParamByValue с int корректно заменяет параметр")]
    public void ReplaceParamByValue_IntValue_ReplacesParameter()
    {
        // Arrange
        const string sqlTemplate = "SELECT * FROM Users WHERE Id = @userId";
        var builder = new SqlQueryBuilder(sqlTemplate);

        // Act
        var result = builder.ReplaceParamByValue("userId", 123).ToString();

        // Assert
        result.ShouldBe("SELECT * FROM Users WHERE Id = 123");
    }

    [Test]
    [Description("Проверяет, что ReplaceParamByValue с long корректно заменяет параметр")]
    public void ReplaceParamByValue_LongValue_ReplacesParameter()
    {
        // Arrange
        const string sqlTemplate = "SELECT * FROM Users WHERE Id = @userId";
        var builder = new SqlQueryBuilder(sqlTemplate);

        // Act
        var result = builder.ReplaceParamByValue("userId", 123L).ToString();

        // Assert
        result.ShouldBe("SELECT * FROM Users WHERE Id = 123");
    }


    [Test]
    [Description("Проверяет, что ReplaceParamByValue заменяет все вхождения параметра")]
    public void ReplaceParamByValue_MultipleOccurrences_ReplacesAll()
    {
        // Arrange
        const string sqlTemplate = "SELECT * FROM Users WHERE Id = @userId AND ParentId = @userId";
        var builder = new SqlQueryBuilder(sqlTemplate);

        // Act
        var result = builder.ReplaceParamByValue("userId", 123).ToString();

        // Assert
        result.ShouldBe("SELECT * FROM Users WHERE Id = 123 AND ParentId = 123");
    }

    private static readonly object[] ParameterAsPartOfAnotherTestCases = 
    {
        // Параметр в начале другого параметра
        new object[] { "SELECT * FROM Users WHERE Id = @userId123", "userId", 123, "SELECT * FROM Users WHERE Id = @userId123" },
        
        // Параметр в середине другого параметра
        new object[] { "SELECT * FROM Users WHERE Id = @myUserId123", "UserId", 123, "SELECT * FROM Users WHERE Id = @myUserId123" },
        
        // Параметр как часть других параметров
        new object[] { "SELECT * FROM Users WHERE Id = @userId AND Name = @userName", "user", 123, "SELECT * FROM Users WHERE Id = @userId AND Name = @userName" },
        
        // Параметр в конце другого параметра
        new object[] { "SELECT * FROM Users WHERE Id = @myUserId", "Id", 123, "SELECT * FROM Users WHERE Id = @myUserId" },
        
        // Параметр с подчеркиванием как часть другого
        new object[] { "SELECT * FROM Users WHERE user_id = @user_id123", "user_id", 123, "SELECT * FROM Users WHERE user_id = @user_id123" }
    };

    [Test]
    [TestCaseSource(nameof(ParameterAsPartOfAnotherTestCases))]
    [Description("Проверяет, что ReplaceParamByValue не заменяет параметры, которые являются частью других параметров")]
    public void ReplaceParamByValue_ParameterAsPartOfAnother_DoesNotReplace(string sqlTemplate, string paramName, int value, string expectedResult)
    {
        // Arrange
        var builder = new SqlQueryBuilder(sqlTemplate);

        // Act
        var result = builder.ReplaceParamByValue(paramName, value).ToString();

        // Assert
        result.ShouldBe(expectedResult);
    }

    [Test]
    [Description("Проверяет, что ReplaceParamByValue заменяет только точные совпадения параметров")]
    public void ReplaceParamByValue_ExactMatch_ReplacesCorrectly()
    {
        // Arrange
        const string sqlTemplate = "SELECT * FROM Users WHERE Id = @userId AND Name = @userName";
        var builder = new SqlQueryBuilder(sqlTemplate);

        // Act
        var result = builder.ReplaceParamByValue("userId", 123).ToString();

        // Assert
        result.ShouldBe("SELECT * FROM Users WHERE Id = 123 AND Name = @userName");
    }

    [Test]
    [Description("Проверяет, что ReplaceParamByValue работает с параметрами, содержащими подчеркивания")]
    public void ReplaceParamByValue_UnderscoreInName_WorksCorrectly()
    {
        // Arrange
        const string sqlTemplate = "SELECT * FROM Users WHERE user_id = @user_id";
        var builder = new SqlQueryBuilder(sqlTemplate);

        // Act
        var result = builder.ReplaceParamByValue("user_id", 123).ToString();

        // Assert
        result.ShouldBe("SELECT * FROM Users WHERE user_id = 123");
    }

    [Test]
    [Description("Проверяет, что ReplaceParamByValue работает с параметрами, содержащими цифры")]
    public void ReplaceParamByValue_DigitsInName_WorksCorrectly()
    {
        // Arrange
        const string sqlTemplate = "SELECT * FROM Users WHERE id1 = @id1";
        var builder = new SqlQueryBuilder(sqlTemplate);

        // Act
        var result = builder.ReplaceParamByValue("id1", 123).ToString();

        // Assert
        result.ShouldBe("SELECT * FROM Users WHERE id1 = 123");
    }

    [Test]
    [Description("Проверяет, что ReplaceParamByValue не заменяет параметры в середине других параметров")]
    public void ReplaceParamByValue_ParameterInMiddle_DoesNotReplace()
    {
        // Arrange
        const string sqlTemplate = "SELECT * FROM Users WHERE Id = @myUserId123";
        var builder = new SqlQueryBuilder(sqlTemplate);

        // Act
        var result = builder.ReplaceParamByValue("UserId", 123).ToString();

        // Assert
        result.ShouldBe("SELECT * FROM Users WHERE Id = @myUserId123");
    }

    [Test]
    [Description("Проверяет, что ReplaceParamByValue работает с параметрами в разных позициях")]
    public void ReplaceParamByValue_MultipleParameters_DifferentPositions()
    {
        // Arrange
        const string sqlTemplate = "SELECT @param1, @param2 FROM Users WHERE Id = @param1 AND Name = @param2";
        var builder = new SqlQueryBuilder(sqlTemplate);

        // Act
        var result = builder
            .ReplaceParamByValue("param1", 123)
            .ReplaceParamByValue("param2", 456)
            .ToString();

        // Assert
        result.ShouldBe("SELECT 123, 456 FROM Users WHERE Id = 123 AND Name = 456");
    }


    [Test]
    [Description("Проверяет, что ReplaceParamByValue работает с параметрами в конце строки")]
    public void ReplaceParamByValue_AtEndOfString_WorksCorrectly()
    {
        // Arrange
        const string sqlTemplate = "SELECT * FROM Users WHERE Id = @userId";
        var builder = new SqlQueryBuilder(sqlTemplate);

        // Act
        var result = builder.ReplaceParamByValue("userId", 123).ToString();

        // Assert
        result.ShouldBe("SELECT * FROM Users WHERE Id = 123");
    }

    [Test]
    [Description("Проверяет, что ReplaceParamByValue работает с параметрами в начале строки")]
    public void ReplaceParamByValue_AtStartOfString_WorksCorrectly()
    {
        // Arrange
        const string sqlTemplate = "@userId = 123";
        var builder = new SqlQueryBuilder(sqlTemplate);

        // Act
        var result = builder.ReplaceParamByValue("userId", 123).ToString();

        // Assert
        result.ShouldBe("123 = 123");
    }


    [Test]
    [Description("Проверяет, что можно комбинировать несколько операций")]
    public void MultipleOperations_Combined_WorksCorrectly()
    {
        // Arrange
        const string sqlTemplate = "SELECT * FROM Users --whereClause-- WHERE Id = @userId /*--orderBlock-- ORDER BY Name --orderBlock--*/";
        var builder = new SqlQueryBuilder(sqlTemplate);

        // Act
        var result = builder
            .IncludeLine("whereClause")
            .IncludeBlock("orderBlock")
            .ReplaceParamByValue("userId", 123)
            .ToString();

        // Assert
        result.ShouldBe("SELECT * FROM Users  WHERE Id = 123  ORDER BY Name ");
    }

    [Test]
    [Description("Проверяет, что ToString возвращает текущее состояние StringBuilder")]
    public void ToString_ReturnsCurrentState()
    {
        // Arrange
        const string sqlTemplate = "SELECT * FROM Users";
        var builder = new SqlQueryBuilder(sqlTemplate);

        // Act
        var result = builder.ToString();

        // Assert
        result.ShouldBe(sqlTemplate);
    }

    [Test]
    [Description("Проверяет, что можно вызывать методы в любом порядке")]
    public void MethodsInAnyOrder_WorksCorrectly()
    {
        // Arrange
        const string sqlTemplate = "SELECT * FROM Users --whereClause-- WHERE Id = @userId /*--orderBlock-- ORDER BY Name --orderBlock--*/";
        var builder = new SqlQueryBuilder(sqlTemplate);

        // Act
        var result = builder
            .ReplaceParamByValue("userId", 456)
            .IncludeBlock("orderBlock")
            .IncludeLine("whereClause")
            .ToString();

        // Assert
        result.ShouldBe("SELECT * FROM Users  WHERE Id = 456  ORDER BY Name ");
    }

    [Test]
    [Description("Проверяет, что IncludeLine работает с пустой строкой")]
    public void IncludeLine_EmptyString_WorksCorrectly()
    {
        // Arrange
        const string sqlTemplate = "SELECT * FROM Users --emptyLine--";
        var builder = new SqlQueryBuilder(sqlTemplate);

        // Act
        var result = builder.IncludeLine("emptyLine").ToString();

        // Assert
        result.ShouldBe("SELECT * FROM Users ");
    }

    [Test]
    [Description("Проверяет, что IncludeBlock работает с пустым блоком")]
    public void IncludeBlock_EmptyBlock_WorksCorrectly()
    {
        // Arrange
        const string sqlTemplate = "SELECT * FROM Users /*--emptyBlock----emptyBlock--*/";
        var builder = new SqlQueryBuilder(sqlTemplate);

        // Act
        var result = builder.IncludeBlock("emptyBlock").ToString();

        // Assert
        result.ShouldBe("SELECT * FROM Users ");
    }
}
