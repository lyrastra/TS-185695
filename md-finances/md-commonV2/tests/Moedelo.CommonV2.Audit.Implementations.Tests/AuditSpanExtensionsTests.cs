using FluentAssertions;
using Moedelo.CommonV2.Audit.Extensions;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moq;
using NUnit.Framework;

namespace Moedelo.CommonV2.Audit.Implementations.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class AuditSpanExtensionsTests
{
    [Test(Description = "Нормализация IAuditSpan::Name корректно вырезает параметры из урлов в составе названия спана")]
    [TestCase("http://some.url.org/app", "./app")]
    [TestCase("http://some.url.org/APP", "./app")]
    [TestCase("http://some.url.org/apP", "./app")]
    [TestCase("http://some.url.org/App", "./app")]
    [TestCase("https://some.url.org/app", "./app")]
    [TestCase("http://some.url.org/foo/bar", "./foo/bar")]
    [TestCase("http://some.url.org/foo-bar/", "./foo-bar/")]
    [TestCase("http://some.url.org/foo/-/bar/", "./foo/-/bar/")]
    [TestCase("http://some.url.org/foo-bar/bob", "./foo-bar/bob")]
    [TestCase("http://some.url.org/foo-bar/bob123", "./foo-bar/bob123")]
    [TestCase("http://some.url.org/foo-bar/bob-123", "./foo-bar/bob-123")]
    [TestCase("http://some.url.org/foo-bar/bob", "./foo-bar/bob")]
    [TestCase("http://some.url.org/foo-bar/bob/2021-01-01", "./foo-bar/bob/...")]
    [TestCase("http://some.url.org/foo/1123", "./foo/...")]
    [TestCase("http://some.url.org/foo/-1123", "./foo/...")]
    [TestCase("http://some.url.org/foo/1/bar/22", "./foo/.../bar/...")]
    [TestCase("http://some.url.org/foo/-1/bar/-22", "./foo/.../bar/...")]
    [TestCase("http://some.url.org/foo/1/bar/-22", "./foo/.../bar/...")]
    [TestCase("http://some.url.org/foo/-1/bar/22", "./foo/.../bar/...")]
    [TestCase("http://some.url.org/foo/11-12-33/bar/44-33-22", "./foo/.../bar/...")]
    [TestCase("ApiClient.MethodAsync POST ./api/bills/%d0%a1%d0%94%d0%a555/shift", "ApiClient.MethodAsync POST ./api/bills/.../shift")]
    [TestCase("ApiClient.MethodAsync POST ./api/bills/w777%d0%a1%d0%94%d0%a555/shift", "ApiClient.MethodAsync POST ./api/bills/.../shift")]
    [TestCase("ApiClient.MethodAsync GET ./api/bills/%d0%a1%d0%94%d0%a555", "ApiClient.MethodAsync GET ./api/bills/...")]
    [TestCase("ApiClient.MethodAsync GET ./api/bills/123%d0%a1%d0%94%d0%a555", "ApiClient.MethodAsync GET ./api/bills/...")]
    [TestCase("http://domain.org/Documents/Get/11233727_22_11_2021_14_04_54_NO_SRCHIS_7720_7720_5837004553583701001_20150805_5AFB10C1-80AB-40CB-9388-232E24DF3B43.XML",
        "./documents/get/...")]
    public void GetNormalizedName_CutOffParametersProperlyFromUrls(string url, string expected)
    {
        var spanMock = new Mock<IAuditSpan>();
        spanMock
            .Setup(x => x.Name)
            .Returns(url);
        spanMock
            .Setup(x => x.IsNameNormalized)
            .Returns(false);

        var normalized = spanMock.Object.GetNormalizedName();

        normalized.Should().Be(expected);
    }
}