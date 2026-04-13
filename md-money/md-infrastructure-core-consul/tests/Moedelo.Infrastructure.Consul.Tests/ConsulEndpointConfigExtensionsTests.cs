using FluentAssertions;
using Moedelo.Infrastructure.Consul.Abstraction;
using Moedelo.Infrastructure.Consul.Extensions;

namespace Moedelo.Infrastructure.Consul.Tests;

public class ConsulEndpointConfigExtensionsTests
{
    [Test]
    [TestCase("localhost", 8500, "key1/key2", "http://localhost:8500/v1/kv/key1/key2")]
    [TestCase("localhost", 8500, "/key1/key2", "http://localhost:8500/v1/kv/key1/key2")]
    [TestCase("localhost", 8500, "key1/key2/", "http://localhost:8500/v1/kv/key1/key2/")]
    [TestCase("localhost", 8500, "/key1/key2/", "http://localhost:8500/v1/kv/key1/key2/")]
    public void GetKeyUrl_WithoutQuery_ReturnsProperlyResult(
        string host, int port,
        string methodPath,
        string expected)
    {
        var address = new ConsulServiceAddress(host, port);

        var actual = address.GetKeyUrl(methodPath);

        actual.Should().Be(expected);
    }

    [Test]
    [TestCase("localhost", 8500, "key1/key2", null, "http://localhost:8500/v1/kv/key1/key2")]
    [TestCase("localhost", 8500, "key1/key2", "all=true", "http://localhost:8500/v1/kv/key1/key2?all=true")]
    [TestCase("localhost", 8500, "/key1/key2", "all=true", "http://localhost:8500/v1/kv/key1/key2?all=true")]
    [TestCase("localhost", 8500, "key1/key2/", "all=true", "http://localhost:8500/v1/kv/key1/key2/?all=true")]
    [TestCase("localhost", 8500, "/key1/key2/", "all=true", "http://localhost:8500/v1/kv/key1/key2/?all=true")]
    [TestCase("localhost", 8500, "key1/key2", "?all=true", "http://localhost:8500/v1/kv/key1/key2?all=true")]
    [TestCase("localhost", 8500, "/key1/key2", "?all=true", "http://localhost:8500/v1/kv/key1/key2?all=true")]
    [TestCase("localhost", 8500, "key1/key2/", "?all=true", "http://localhost:8500/v1/kv/key1/key2/?all=true")]
    [TestCase("localhost", 8500, "/key1/key2/", "?all=true", "http://localhost:8500/v1/kv/key1/key2/?all=true")]
    public void GetKeyUrl_WithQuery_ReturnsProperlyResult(
        string host, int port,
        string keyPath, string? uriQuery,
        string expected)
    {
        var address = new ConsulServiceAddress(host, port);

        var actual = address.GetKeyUrl(keyPath, uriQuery);

        actual.Should().Be(expected);
    }

    [Test]
    [TestCase("localhost", 8500, "area/method", "http://localhost:8500/v1/agent/area/method")]
    [TestCase("localhost", 8500, "/area/method", "http://localhost:8500/v1/agent/area/method")]
    [TestCase("localhost", 8500, "area/method/", "http://localhost:8500/v1/agent/area/method/")]
    [TestCase("localhost", 8500, "/area/method/", "http://localhost:8500/v1/agent/area/method/")]
    public void GetAgentApiMethodUrl_WithoutQuery_ReturnsProperlyResult(
        string host, int port,
        string methodPath,
        string expected)
    {
        var address = new ConsulServiceAddress(host, port);

        var actual = address.GetAgentApiMethodUrl(methodPath);

        actual.Should().Be(expected);
    }

    [Test]
    [TestCase("localhost", 8500, "area/method", null, "http://localhost:8500/v1/agent/area/method")]
    [TestCase("localhost", 8500, "area/method", "all=true", "http://localhost:8500/v1/agent/area/method?all=true")]
    [TestCase("localhost", 8500, "/area/method", "all=true", "http://localhost:8500/v1/agent/area/method?all=true")]
    [TestCase("localhost", 8500, "area/method/", "all=true", "http://localhost:8500/v1/agent/area/method/?all=true")]
    [TestCase("localhost", 8500, "/area/method/", "all=true", "http://localhost:8500/v1/agent/area/method/?all=true")]
    [TestCase("localhost", 8500, "area/method", "?all=true", "http://localhost:8500/v1/agent/area/method?all=true")]
    [TestCase("localhost", 8500, "/area/method/", "?all=true", "http://localhost:8500/v1/agent/area/method/?all=true")]
    public void GetAgentApiMethodUrl_WithQuery_ReturnsProperlyResult(
        string host, int port,
        string methodPath, string? uriQuery,
        string expected)
    {
        var address = new ConsulServiceAddress(host, port);

        var actual = address.GetAgentApiMethodUrl(methodPath, uriQuery);

        actual.Should().Be(expected);
    }

    [Test]
    [TestCase("localhost", 8500, "area/method", "http://localhost:8500/v1/catalog/area/method")]
    [TestCase("localhost", 8500, "/area/method", "http://localhost:8500/v1/catalog/area/method")]
    [TestCase("localhost", 8500, "area/method/", "http://localhost:8500/v1/catalog/area/method/")]
    [TestCase("localhost", 8500, "/area/method/", "http://localhost:8500/v1/catalog/area/method/")]
    public void GetCatalogApiMethodUrl_WithoutQuery_ReturnsProperlyResult(
        string host, int port,
        string methodPath,
        string expected)
    {
        var address = new ConsulServiceAddress(host, port);

        var actual = address.GetCatalogApiMethodUrl(methodPath);

        actual.Should().Be(expected);
    }

    [Test]
    [TestCase("localhost", 8500, "area/method", null, "http://localhost:8500/v1/catalog/area/method")]
    [TestCase("localhost", 8500, "area/method", "all=true", "http://localhost:8500/v1/catalog/area/method?all=true")]
    [TestCase("localhost", 8500, "/area/method", "all=true", "http://localhost:8500/v1/catalog/area/method?all=true")]
    [TestCase("localhost", 8500, "area/method/", "all=true", "http://localhost:8500/v1/catalog/area/method/?all=true")]
    [TestCase("localhost", 8500, "/area/method/", "all=true", "http://localhost:8500/v1/catalog/area/method/?all=true")]
    [TestCase("localhost", 8500, "area/method", "?all=true", "http://localhost:8500/v1/catalog/area/method?all=true")]
    [TestCase("localhost", 8500, "/area/method", "?all=true", "http://localhost:8500/v1/catalog/area/method?all=true")]
    [TestCase("localhost", 8500, "area/method/", "?all=true", "http://localhost:8500/v1/catalog/area/method/?all=true")]
    [TestCase("localhost", 8500, "/area/method/", "?all=true", "http://localhost:8500/v1/catalog/area/method/?all=true")]
    public void GetCatalogApiMethodUrl_WithQuery_ReturnsProperlyResult(
        string host, int port,
        string methodPath, string? uriQuery,
        string expected)
    {
        var address = new ConsulServiceAddress(host, port);

        var actual = address.GetCatalogApiMethodUrl(methodPath, uriQuery);

        actual.Should().Be(expected);
    }
}
