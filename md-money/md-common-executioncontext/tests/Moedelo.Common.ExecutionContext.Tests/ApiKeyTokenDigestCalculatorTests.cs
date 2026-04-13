using FluentAssertions;
using Moedelo.Common.ExecutionContext.Internals;

namespace Moedelo.Common.ExecutionContext.Tests;

[TestFixture]
public class ApiKeyTokenDigestCalculatorTests
{
    [Test(Description = "Просто тест, проверящий полный расчёт подписи на тестовых данных")]
    public void CalculateDigest_CalculatesDigestProperly()
    {
        var apiKeyToken = "9507b3f1-ecb0-43e8-838d-37943fe7a808";
        var nonce = Guid.Parse("c2bd2bf4-ba31-4255-af6c-429d4919b051");
        var timestamp = DateTime.Parse("2025-05-23T11:58:24.386+03:00");
        const string expected = "ac65945793b3162f3758fcffec22fdcb7acf0245f836b55f9c145b43f980a132";
        var actual = ApiKeyTokenDigestCalculator.CalculateDigest(apiKeyToken, nonce, timestamp);

        actual.Should().Be(expected);
    }
}
