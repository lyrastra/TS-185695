using FluentAssertions;
using Moedelo.Common.Audit.Writers.Kafka.Extensions;

namespace Moedelo.Common.Audit.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class StringExtensionsTests
{
    [Test(Description="В json строке cвойства с чувствительной информацией маскируются")]
    [TestCase(null, null)]
    [TestCase("", "")]
    [TestCase("password:valueOfPassword", "password:valueOfPassword", Description = "такой кейс не заменяем - непонятный формат")]
    [TestCase("172.16.172.211,password=9xbJxJRkmssBz,abortConnect=false", "172.16.172.211,password=***,abortConnect=false", Description = "строка подключения к редис")]
    [TestCase("Host=172.16.172.131;Port=6432;Database=authorization;User Id=app;Password=app123456;Maximum Pool Size=20;Timeout=120;", "Host=172.16.172.131;Port=6432;Database=authorization;User Id=app;Password=***;Maximum Pool Size=20;Timeout=120;", Description = "строка подключения к postgres")]
    [TestCase("mongodb://user:password@172.16.172.160,172.16.172.161/kafka_transfer?kafka@bip", "mongodb://***:***@172.16.172.160,172.16.172.161/kafka_transfer?kafka@bip", Description = "строка подключения к mongo")]
    [TestCase("mongodb://kafkamanager:ja4mQbXRFu4b@172.16.172.160,172.16.172.161/kafka_transfer", "mongodb://***:***@172.16.172.160,172.16.172.161/kafka_transfer", Description = "строка подключения к mongo")]
    [TestCase("mongodb://filemanager:Dkjf($aAd#Y4^N234)s@192.168.222.171,192.168.222.172/files?connectTimeoutMS=100000",
        "mongodb://***:***@192.168.222.171,192.168.222.172/files?connectTimeoutMS=100000", Description = "строка подключения к mongo со спецсимволами в пароле")]
    [TestCase("http://kafkamanager:ja4mQbXRFu4b@172.16.172.160,172.16.172.161/kafka_transfer", "http://***:***@172.16.172.160,172.16.172.161/kafka_transfer", Description = "строка подключения по http")]
    [TestCase("https://kafkamanager:ja4mQbXRFu4b@172.16.172.160,172.16.172.161/kafka_transfer", "https://***:***@172.16.172.160,172.16.172.161/kafka_transfer", Description = "строка подключения по https")]
    [TestCase("\"password\":\"sensitive-value123*4532452543\"", "\"password\":\"***\"")]
    [TestCase("\"Password\":\"1233some\\Kindvalue123*4532452543\"", "\"Password\":\"***\"")]
    [TestCase(",\"Password\":\"1233some\\Kindvalue123*4532452543\"", ",\"Password\":\"***\"")]
    [TestCase(",\"Password\":\"1233some\\Kindvalue123*4532452543\"", ",\"Password\":\"***\"")]
    [TestCase("\\\"password\\\":\\\";kahfdakfdsls!dsf\\\"", "\\\"password\\\":\\\"***\\\"")]
    [TestCase("\\\"Password\\\":\\\";kahfdakfdsls!dsf\\\",\\\"anotherField\\\":123", "\\\"Password\\\":\\\"***\\\",\\\"anotherField\\\":123")]
    [TestCase(",\\\"password\\\":\\\";kahfdakfdsls!dsf\\\"", ",\\\"password\\\":\\\"***\\\"")]
    [TestCase(
        "{\"Request.Body\":[\"{\\\"Login\\\": \\\"flex06@mail.ru\\\", \\\"Password\\\": \\\"Wilington\\\", \\\"Source\\\": \\\"subtotal\\\"}\"]}",
        "{\"Request.Body\":[\"{\\\"Login\\\": \\\"flex06@mail.ru\\\", \\\"Password\\\": \\\"***\\\", \\\"Source\\\": \\\"subtotal\\\"}\"]}",
        Description = "Реальный кейс из Home.Public.Api")]
    public void MaskSensitiveJsonFields_WorksProperly(string? input, string? expected)
    {
        var actual = input.MaskSensitiveJsonFields();

        actual.Should().Be(expected);
    }
}
