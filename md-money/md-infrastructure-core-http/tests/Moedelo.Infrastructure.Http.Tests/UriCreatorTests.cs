using FluentAssertions;

namespace Moedelo.Infrastructure.Http.Tests;

public class UriCreatorTests
{
    private UriCreator uriCreator;

    [SetUp]
    public void Setup()
    {
        uriCreator = new UriCreator();
    }

    #region Create(string host, string path, string query) Tests

    [Test]
    public void Create_WithHostPathAndQuery_ShouldReturnCorrectUri()
    {
        // Arrange
        var host = "http://api.example.com";
        var path = "/users";
        var query = "page=1&limit=10";

        // Act
        var result = uriCreator.Create(host, path, query);

        // Assert
        result.Should().Be("http://api.example.com:80/users?page=1&limit=10");
    }

    [Test]
    public void Create_WithEmptyQuery_ShouldReturnUriWithoutQuery()
    {
        // Arrange
        var host = "http://api.example.com";
        var path = "/users";
        var query = string.Empty;

        // Act
        var result = uriCreator.Create(host, path, query);

        // Assert
        result.Should().Be("http://api.example.com:80/users");
    }

    [Test]
    public void Create_WithNullQuery_ShouldReturnUriWithoutQuery()
    {
        // Arrange
        var host = "http://api.example.com";
        var path = "/users";
        string? query = null;

        // Act
        var result = uriCreator.Create(host, path, query);

        // Assert
        result.Should().Be("http://api.example.com:80/users");
    }

    #endregion

    #region Create(string host, string path) Tests

    [Test]
    public void Create_WithHostAndPathOnly_ShouldReturnCorrectUri()
    {
        // Arrange
        var host = "http://api.example.com";
        var path = "/users";

        // Act
        var result = uriCreator.Create(host, path);

        // Assert
        result.Should().Be("http://api.example.com:80/users");
    }

    [Test]
    public void Create_WithQueryInPath_ShouldExtractQueryFromPath()
    {
        // Arrange
        var host = "http://api.example.com";
        var path = "/users?page=1&limit=10";

        // Act
        var result = uriCreator.Create(host, path);

        // Assert
        result.Should().Be("http://api.example.com:80/users?page=1&limit=10");
    }

    [Test]
    public void Create_WithEmptyPath_ShouldReturnHostOnly()
    {
        // Arrange
        var host = "http://api.example.com";
        var path = string.Empty;

        // Act
        var result = uriCreator.Create(host, path);

        // Assert
        result.Should().Be("http://api.example.com:80/");
    }

    [Test]
    public void Create_WithNullPath_ShouldReturnHostOnly()
    {
        // Arrange
        var host = "http://api.example.com";
        string? path = null;

        // Act
        var result = uriCreator.Create(host, path);

        // Assert
        result.Should().Be("http://api.example.com:80/");
    }

    #endregion

    #region Create(string host, string path, IReadOnlyCollection<KeyValuePair<string, object>>) Tests

    [Test]
    public void Create_WithKeyValuePairs_ShouldReturnCorrectUri()
    {
        // Arrange
        var host = "http://api.example.com";
        var path = "/users";
        var queryParams = new List<KeyValuePair<string, object>>
        {
            new("page", 1),
            new("limit", 10),
            new("name", "John Doe")
        };

        // Act
        var result = uriCreator.Create(host, path, queryParams);

        // Assert
        result.Should().Be("http://api.example.com:80/users?page=1&limit=10&name=John+Doe");
    }

    [Test]
    public void Create_WithNullKeyValueCollection_ShouldReturnUriWithoutQuery()
    {
        // Arrange
        var host = "http://api.example.com";
        var path = "/users";
        IReadOnlyCollection<KeyValuePair<string, object>>? queryParams = null;

        // Act
        var result = uriCreator.Create(host, path, queryParams);

        // Assert
        result.Should().Be("http://api.example.com:80/users");
    }

    [Test]
    public void Create_WithEmptyKeyValueCollection_ShouldReturnUriWithoutQuery()
    {
        // Arrange
        var host = "http://api.example.com";
        var path = "/users";
        var queryParams = new List<KeyValuePair<string, object>>();

        // Act
        var result = uriCreator.Create(host, path, queryParams);

        // Assert
        result.Should().Be("http://api.example.com:80/users");
    }

    [Test]
    public void Create_WithNullValues_ShouldIgnoreNullParams()
    {
        // Arrange
        var host = "http://api.example.com";
        var path = "/users";
        var queryParams = new List<KeyValuePair<string, object?>>
        {
            new("page", 1),
            new("name", null),
            new("limit", 10)
        };

        // Act
        var result = uriCreator.Create(host, path, queryParams);

        // Assert
        result.Should().Be("http://api.example.com:80/users?page=1&limit=10");
    }

    [Test]
    public void Create_WithPrimitiveArray_ShouldCreateMultipleParams()
    {
        // Arrange
        var host = "http://api.example.com";
        var path = "/users";
        var queryParams = new List<KeyValuePair<string, object>>
        {
            new("ids", new[] { 1, 2, 3 })
        };

        // Act
        var result = uriCreator.Create(host, path, queryParams);

        // Assert
        result.Should().Be("http://api.example.com:80/users?ids=1&ids=2&ids=3");
    }

    [Test]
    public void Create_WithPrimitiveList_ShouldCreateMultipleParams()
    {
        // Arrange
        var host = "http://api.example.com";
        var path = "/users";
        var queryParams = new List<KeyValuePair<string, object>>
        {
            new("ids", new List<int> { 1, 2, 3 })
        };

        // Act
        var result = uriCreator.Create(host, path, queryParams);

        // Assert
        result.Should().Be("http://api.example.com:80/users?ids=1&ids=2&ids=3");
    }

    [Test]
    public void Create_WithPrimitiveCollectionExpression_ShouldCreateMultipleParams()
    {
        // Arrange
        var host = "http://api.example.com";
        var path = "/users";
        IReadOnlyCollection<int> ids = [1, 2, 3]; 
        
        var queryParams = new List<KeyValuePair<string, object>>
        {
            new("ids", ids)
        };

        // Act
        var result = uriCreator.Create(host, path, queryParams);

        // Assert
        result.Should().Be("http://api.example.com:80/users?ids=1&ids=2&ids=3");
    }

    [Test]
    public void Create_WithPrimitiveCollectionExpressionWithOneItem_ShouldCreateMultipleParams()
    {
        // Arrange
        var host = "http://api.example.com";
        var path = "/users";
        IReadOnlyCollection<int> ids = [1]; 
        
        var queryParams = new List<KeyValuePair<string, object>>
        {
            new("ids", ids)
        };

        // Act
        var result = uriCreator.Create(host, path, queryParams);

        // Assert
        result.Should().Be("http://api.example.com:80/users?ids=1");
    }
 
    [Test]
    public void Create_WithStringArray_ShouldCreateOneParam()
    {
        // Arrange
        var host = "http://api.example.com";
        var path = "/users";
        var queryParams = new List<KeyValuePair<string, object>>
        {
            new("tags", new[] { "admin", "user", "guest" })
        };

        // Act
        var result = uriCreator.Create(host, path, queryParams);

        // Assert
        result.Should().Be("http://api.example.com:80/users?tags=admin&tags=user&tags=guest");
    }

    [Test]
    public void Create_WithStringList_ShouldCreateMultipleParams()
    {
        // Arrange
        var host = "http://api.example.com";
        var path = "/users";
        var queryParams = new List<KeyValuePair<string, object>>
        {
            new("tags", new List<string> { "admin", "user", "guest" })
        };

        // Act
        var result = uriCreator.Create(host, path, queryParams);

        // Assert
        result.Should().Be("http://api.example.com:80/users?tags=admin&tags=user&tags=guest");
    }

    [Test]
    public void Create_WithStringCollectionExpressionWithOneElement_ShouldCreateOneParam()
    {
        // Arrange
        var host = "http://api.example.com";
        var path = "/users";

        IReadOnlyCollection<string> tags = ["admin"];

        var queryParams = new List<KeyValuePair<string, object>>
        {
            new("tags", tags)
        };

        // Act
        var result = uriCreator.Create(host, path, queryParams);

        // Assert
        result.Should().Be("http://api.example.com:80/users?tags=admin");
    }

    [Test]
    public void Create_WithStringCollectionExpression_ShouldCreateMultipleParams()
    {
        // Arrange
        var host = "http://api.example.com";
        var path = "/users";

        IReadOnlyCollection<string> tags = ["admin", "user", "guest"];

        var queryParams = new List<KeyValuePair<string, object>>
        {
            new("tags", tags)
        };

        // Act
        var result = uriCreator.Create(host, path, queryParams);

        // Assert
        result.Should().Be("http://api.example.com:80/users?tags=admin&tags=user&tags=guest");
    }

    [Test]
    public void Create_WithComplexObjectArray_ShouldCreateIndexedParams()
    {
        // Arrange
        var host = "http://api.example.com";
        var path = "/users";
        var complexObjects = new[]
        {
            new { Name = "John", Age = 30 },
            new { Name = "Jane", Age = 25 }
        };
        var queryParams = new List<KeyValuePair<string, object>>
        {
            new("users", complexObjects)
        };

        // Act
        var result = uriCreator.Create(host, path, queryParams);

        // Assert
        result.Should().Contain("users[0]")
            .And.Contain("users[1]");
    }

    [Test]
    public void Create_WithComplexObject_ShouldCreateNestedParams()
    {
        // Arrange
        var host = "http://api.example.com";
        var path = "/users";
        var complexObject = new { Name = "John", Age = 30 };
        var queryParams = new List<KeyValuePair<string, object>>
        {
            new("user", complexObject)
        };

        // Act
        var result = uriCreator.Create(host, path, queryParams);

        // Assert
        result.Should().Contain("user.Name=John")
            .And.Contain("user.Age=30");
    }

    [Test]
    public void Create_WithDateTime_ShouldFormatAsIso()
    {
        // Arrange
        var host = "http://api.example.com";
        var path = "/users";
        var dateTime = new DateTime(2023, 12, 15, 14, 30, 45, DateTimeKind.Utc);
        var queryParams = new List<KeyValuePair<string, object>>
        {
            new("createdAt", dateTime)
        };

        // Act
        var result = uriCreator.Create(host, path, queryParams);

        // Assert
        result.Should().Contain("createdAt=2023-12-15T14%3a30%3a45Z");
    }

    [Test]
    public void Create_WithSpecialCharacters_ShouldUrlEncode()
    {
        // Arrange
        var host = "http://api.example.com";
        var path = "/users";
        var queryParams = new List<KeyValuePair<string, object>>
        {
            new("search", "hello world & more"),
            new("filter", "status=active")
        };

        // Act
        var result = uriCreator.Create(host, path, queryParams);

        // Assert
        result.Should().Contain("search=hello+world+%26+more")
            .And.Contain("filter=status%3dactive");
    }

    [Test]
    public void Create_WithComplexObjectArrayContainingNulls_ShouldPreserveIndexes()
    {
        // Arrange
        var host = "http://api.example.com";
        var path = "/users";
        var complexObjects = new[]
        {
            new { Name = "John", Age = 30 },
            null,
            new { Name = "Jane", Age = 25 }
        };
        var queryParams = new List<KeyValuePair<string, object>>
        {
            new("users", complexObjects)
        };

        // Act
        var result = uriCreator.Create(host, path, queryParams);

        // Assert
        result.Should().Contain("users[0].Name=John")
            .And.Contain("users[0].Age=30")
            .And.NotContain("users[1].Name")
            .And.NotContain("users[1].Age")
            .And.Contain("users[2].Name=Jane")
            .And.Contain("users[2].Age=25");
    }

    #endregion

    #region Create(string host, string path, object queryParams) Tests

    [Test]
    public void Create_WithObjectParams_ShouldReturnCorrectUri()
    {
        // Arrange
        var host = "http://api.example.com";
        var path = "/users";
        var queryParams = new { Page = 1, Limit = 10, Name = "John Doe" };

        // Act
        var result = uriCreator.Create(host, path, queryParams);

        // Assert
        result.Should().Be("http://api.example.com:80/users?Page=1&Limit=10&Name=John+Doe");
    }

    [Test]
    public void Create_WithNullObjectParams_ShouldReturnUriWithoutQuery()
    {
        // Arrange
        var host = "http://api.example.com";
        var path = "/users";
        object queryParams = null;

        // Act
        var result = uriCreator.Create(host, path, queryParams);

        // Assert
        result.Should().Be("http://api.example.com:80/users");
    }

    [Test]
    public void Create_WithObjectContainingDateTime_ShouldFormatDateTime()
    {
        // Arrange
        var host = "http://api.example.com";
        var path = "/users";
        var dateTime = new DateTime(2023, 12, 15, 14, 30, 45, DateTimeKind.Utc);
        var queryParams = new { CreatedAt = dateTime, Status = "active" };

        // Act
        var result = uriCreator.Create(host, path, queryParams);

        // Assert
        result.Should().Contain("CreatedAt=2023-12-15T14%3a30%3a45Z")
            .And.Contain("Status=active");
    }

    [Test]
    public void Create_WithEnumerableObject_ShouldThrowArgumentException()
    {
        // Arrange
        var host = "http://api.example.com";
        var path = "/users";
        var queryParams = new List<string> { "item1", "item2" };

        // Act & Assert
        var action = () => uriCreator.Create(host, path, queryParams);
        action.Should().Throw<ArgumentException>()
            .WithMessage("Collection parameter is not supported.");
    }

    [Test]
    public void Create_WithObjectContainingComplexProperties_ShouldCreateNestedParams()
    {
        // Arrange
        var host = "http://api.example.com";
        var path = "/users";
        var queryParams = new 
        { 
            Page = 1, 
            Filter = new { Status = "active", Role = "admin" } 
        };

        // Act
        var result = uriCreator.Create(host, path, queryParams);

        // Assert
        result.Should().Contain("Page=1")
            .And.Contain("Filter.Status=active")
            .And.Contain("Filter.Role=admin");
    }

    [Test]
    public void Create_WithVariousDataTypes_ShouldHandleAllTypes()
    {
        // Arrange
        var host = "http://api.example.com";
        var path = "/test";
        var queryParams = new 
        { 
            IntValue = 42,
            DoubleValue = 3.14,
            BoolValue = true,
            StringValue = "test"
        };

        // Act
        var result = uriCreator.Create(host, path, queryParams);

        // Assert
        result.Should().Contain("IntValue=42")
            .And.Contain("DoubleValue=3%2c14") // Note: uses comma due to localization
            .And.Contain("BoolValue=True")
            .And.Contain("StringValue=test");
    }

    #endregion

    #region Edge Cases

    [Test]
    public void Create_WithEmptyStringValues_ShouldIncludeEmptyParams()
    {
        // Arrange
        var host = "http://api.example.com";
        var path = "/users";
        var queryParams = new { Name = "", Age = 25 };

        // Act
        var result = uriCreator.Create(host, path, queryParams);

        // Assert
        result.Should().Contain("Name=")
            .And.Contain("Age=25");
    }

    [Test]
    public void Create_WithLocalDateTime_ShouldConvertToUtc()
    {
        // Arrange
        var host = "http://api.example.com";
        var path = "/users";
        var localDateTime = new DateTime(2023, 12, 15, 14, 30, 45, DateTimeKind.Local);
        var queryParams = new { CreatedAt = localDateTime };

        // Act
        var result = uriCreator.Create(host, path, queryParams);

        // Assert
        result.Should().Contain("CreatedAt=")
            .And.Contain("Z"); // Should end with Z indicating UTC
    }

    #endregion

    #region HTTPS Protocol Tests (for coverage)

    [Test]
    public void Create_WithHttpsProtocol_ShouldAddDefaultPort()
    {
        // Arrange
        var host = "https://api.example.com";
        var path = "/users";

        // Act
        var result = uriCreator.Create(host, path);

        // Assert
        result.Should().Be("https://api.example.com:443/users");
    }

    [Test]
    public void Create_WithHttpsAndQuery_ShouldReturnCorrectUri()
    {
        // Arrange
        var host = "https://api.example.com";
        var path = "/users";
        var query = "secure=true";

        // Act
        var result = uriCreator.Create(host, path, query);

        // Assert
        result.Should().Be("https://api.example.com:443/users?secure=true");
    }

    #endregion

    #region HTTP Protocol Tests (default behavior)

    [Test]
    public void Create_WithHttpProtocol_ShouldAddDefaultPort()
    {
        // Arrange
        var host = "http://api.example.com";
        var path = "/users";

        // Act
        var result = uriCreator.Create(host, path);

        // Assert
        result.Should().Be("http://api.example.com:80/users");
    }

    [Test]
    public void Create_WithCustomPort_ShouldPreservePort()
    {
        // Arrange
        var host = "https://api.example.com:8080";
        var path = "/users";

        // Act
        var result = uriCreator.Create(host, path);

        // Assert
        result.Should().Be("https://api.example.com:8080/users");
    }

    #endregion
}
