using NUnit.Framework;
using Microsoft.OpenApi.Models;
using Moedelo.Infrastructure.AspNetCore.Swagger.Filters;

namespace Moedelo.Infrastructure.AspNetCore.Swagger.Tests;

[TestFixture]
public class PrivateEndpointHidingFilterTests
{
    private PrivateEndpointHidingFilter _filter;

    [SetUp]
    public void SetUp()
    {
        _filter = new PrivateEndpointHidingFilter();
    }

    [Test]
    [TestCase("/private/api/v1", ExpectedResult = true)]
    [TestCase("/private/api1", ExpectedResult = true)]
    [TestCase("/private/api3", ExpectedResult = true)]
    [TestCase("/private/3", ExpectedResult = false)]
    [TestCase("/privateapi/1", ExpectedResult = false)]
    [TestCase("/public/api2", ExpectedResult = false)]
    public bool HidingTest(string endpoint)
    {
        // Arrange
        var swaggerDoc = new OpenApiDocument
        {
            Paths = new OpenApiPaths
            {
                [endpoint] = new OpenApiPathItem(),
            }
        };

        // Act
        _filter.Apply(swaggerDoc, null);

        // Assert
        return !swaggerDoc.Paths.ContainsKey(endpoint);
    }
}