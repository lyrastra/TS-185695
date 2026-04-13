using FluentAssertions;
using Moedelo.Common.Audit.Writers.Kafka.Extensions;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Common.Audit.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class KafkaAuditSpanExtensionsTests
{
    [Test(Description = "В тэгах cвойства с чувствительной информацией маскируются")]
    [TestCaseSource(nameof(TestCaseSource))]
    public void TagsToJson_MasksSensitivePropertyValues(
        IReadOnlyDictionary<string, List<object>> spanTags, string passwordValue)
    {
        var json = spanTags.TagsToJson();

        json.Should().NotContain(passwordValue,
            "значение пароля должно быть удалено");
    }

    private static IEnumerable<object> TestCaseSource = new[]
    {
        new object[]
        {
            new Dictionary<string, List<object>>
            {
                ["tag1"] = new List<object>
                {
                    new
                    {
                        Login = "Login",
                        Password = "XxxXxxxYYYxx"
                    }
                }
            },
            "XxxXxxxYYYxx"
        },
        new object[]
        {
            new Dictionary<string, List<object>>
            {
                ["tag1"] = new List<object>
                {
                    new
                    {
                        login = "Login",
                        password = ";kdjafds@\"\"dfsdaf"
                    }
                }
            },
            ";kdjafds@\\\"\\\"dfsdaf"
        },
        new object[]
        {
            new Dictionary<string, List<object>>
            {
                ["tag1"] = new List<object>
                {
                    new
                    {
                        Body = new
                        {
                            Login = "Login",
                            Password = "XxxXxxxYYYxx"
                        }.ToJsonString()
                    }
                }
            },
            "XxxXxxxYYYxx"
        },
        new object[]
        {
            new Dictionary<string, List<object>>
            {
                ["tag1"] = new List<object>
                {
                    new
                    {
                        Body = new
                        {
                            login = "Login",
                            password = ";kdjafds@\"\"dfsdaf"
                        }.ToJsonString()
                    }
                }
            },
            @";kdjafds@\\\""\\\""dfsdaf"
        },
        new object[]
        {
            new Dictionary<string, List<object>>
            {
                // строка подключения к редис
                ["tag1"] = new (){ new { ConnectionString = "172.16.172.211,password=9xbJxJRkmssBz,abortConnect=false" }}
            },
            "9xbJxJRkmssBz"
        },
        new object[]
        {
            new Dictionary<string, List<object>>
            {
                // строка подключения к postgres
                ["tag1"] = new (){ new { ConnectionString = "Host=172.16.172.131;Port=6432;Database=authorization;User Id=app;Password=app123456;Maximum Pool Size=20;Timeout=120;" }}
            },
            "app123456"
        },
        new object[]
        {
            new Dictionary<string, List<object>>
            {
                // подключение к mongo
                ["tag1"] = new (){ new { ConnectionString = "mongodb://kafkamanager:ja4mQbXRFu4b@172.16.172.160,172.16.172.161/kafka_transfer" }}
            },
            "ja4mQbXRFu4b"
        },
        new object[]
        {
            new Dictionary<string, List<object>>
            {
                // подключение к http
                ["tag1"] = new (){ new { ConnectionString = "http://kafkamanager:ja4mQbXRFu4b@172.16.172.160,172.16.172.161/kafka_transfer?connectTimeoutMS=100000" }}
            },
            "ja4mQbXRFu4b"
        },
        new object[]
        {
            new Dictionary<string, List<object>>
            {
                // подключение к https
                ["tag1"] = new (){ new { ConnectionString = "https://kafkamanager:ja4mQbXRFu4b@172.16.172.160,172.16.172.161/kafka_transfer?connectTimeoutMS=100000" }}
            },
            "ja4mQbXRFu4b"
        }
    };
}
