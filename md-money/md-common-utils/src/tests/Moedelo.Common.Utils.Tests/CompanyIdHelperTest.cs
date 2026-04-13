using NUnit.Framework;
using FluentAssertions;

namespace Moedelo.Common.Utils.Tests;

[TestFixture]
public class CompanyIdHelperTest
{
    [Test]
    public void TestUrlWithoutParams()
    {
        var url = "/Test";
        var firmId = 1;
        var result = CompanyIdHelper.GetUrlWithCompanyId(url, firmId);

        result.Should().BeEquivalentTo("/Test?_companyId=1");
    }

    [Test]
    public void TestUrlWithParams()
    {
        var url = "/Test?p=2";
        var firmId = 1;
        var result = CompanyIdHelper.GetUrlWithCompanyId(url, firmId);

        result.Should().BeEquivalentTo("/Test?p=2&_companyId=1");
    }

    [Test]
    public void TestUrlWithHash()
    {
        var url = "/Test#test";
        var firmId = 1;
        var result = CompanyIdHelper.GetUrlWithCompanyId(url, firmId);

        result.Should().BeEquivalentTo("/Test?_companyId=1#test");
    }

    [Test]
    public void TestUrlWithHashAndParams()
    {
        var url = "/Test?p=1#test";
        var firmId = 1;
        var result = CompanyIdHelper.GetUrlWithCompanyId(url, firmId);

        result.Should().BeEquivalentTo("/Test?p=1&_companyId=1#test");
    }

    [Test]
    public void TestUrlWithFirm0()
    {
        var url = "/Test?p=2";
        var firmId = 0;
        var result = CompanyIdHelper.GetUrlWithCompanyId(url, firmId);

        result.Should().BeEquivalentTo("/Test?p=2");
    }
}
