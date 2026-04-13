using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Moedelo.CommonV2.Xss;
using NUnit.Framework;

[TestFixture]
public class XssValidatorTests
{
    // collections of xss vectors https://gist.github.com/JohannesHoppe/5612274

    [Test]
    public void ShouldPassWhenInputIsNullString()
    {
        Action validate = () => XssValidator.Validate(null);

        validate.Should().NotThrow<XssValidationException>();
    }

    [Test]
    public void ShouldPassWhenInputIsNullObject()
    {
        Action validate = () => XssValidator.Validate(default(object));

        validate.Should().NotThrow<XssValidationException>();
    }

    [Test]
    public void ShouldPassWhenInputIsValueType()
    {
        const double input = 34.32;

        Action validate = () => XssValidator.Validate(input);

        validate.Should().NotThrow<XssValidationException>();
    }

    [Test]
    public void ShouldPassWhenInputIsObjectThatHasValidString()
    {
        var input = new { Value = 34.32, Name = "some valid string" };

        Action validate = () => XssValidator.Validate(input);

        validate.Should().NotThrow<XssValidationException>();
    }

    [Test]
    public void ShouldFallWhenInputIsObjectThatHasInvalidString()
    {
        var input = new { Value = 34.32, Name = "<script>alert('xss')</script>" };

        Action validate = () => XssValidator.Validate(input);

        validate.Should().Throw<XssValidationException>();
    }

    [Test]
    public void ShouldPassWhenInputIsValidString()
    {
        const string input = "some valid string";

        Action validate = () => XssValidator.Validate(input);

        validate.Should().NotThrow<XssValidationException>();
    }

    [Test]
    public void ShouldPassWhenInputHasValidTag()
    {
        const string input = "some <b>valid</b> string";

        Action validate = () => XssValidator.Validate(input);

        validate.Should().NotThrow<XssValidationException>();
    }

    [Test]
    [TestCase("<body onMouseEnter body onMouseEnter=\"alert('xss')\"></body onMouseEnter>")]
    [TestCase("<embed src=\"alert('xss')\">")]
    [TestCase("<frame src=\"document.cookie=true;\">")]
    [TestCase("<script>alert('xss')</script>")]
    [TestCase("<frameset onScroll frameset onScroll=\"alert('xss')\"></frameset onScroll>")]
    [TestCase("<iframe onLoad=\"alert('xss')\"></iframe onLoad>")]
    [TestCase("<img src=1 href=1 onerror=\"alert('xss')\"></img>")]
    [TestCase("<style onLoad=\"alert('xss')\"></style onLoad>")]
    [TestCase("<layer src=\"alert('xss');\"></layer>")]
    [TestCase("<link rel=\"stylesheet\" href=\"alert('xss');\">")]
    [TestCase("<ilayer src=\"alert('xss');\"></ilayer>")]
    [TestCase("<meta http-equiv=\"link\" content=\"<http://someurl.org/xss.css>; rel=stylesheet\">")]
    [TestCase("<object src=1 href=1 onerror=\"alert('xss')\"></object>")]
    [TestCase("<input type=text autofocus=true onfocus=alert(xss)>")]
    [TestCase("<ilayer src=\"alert('xss');\"></ilayer>")]
    public void ShouldFallWhenInputContainsDangerousTag(string input)
    {
        Action validate = () => XssValidator.Validate(input);
        validate.Should().Throw<XssValidationException>();
    }

    [Test]
    public void ShouldFallWhenInputContainsDangerousTagMaskedWithSlash()
    {
        const string input = "<SCRIPT/SRC=\"http://some/xss.js\"></SCRIPT>";

        Action validate = () => XssValidator.Validate(input);

        validate.Should().Throw<XssValidationException>();
    }

    [Test]
    public void ShouldFallWhenInputContainsDangerousTagMaskedWithUrlEncode()
    {
        const string input = "%3Cscript>alert('xss')</script>";

        Action validate = () => XssValidator.Validate(input);

        validate.Should().Throw<XssValidationException>();
    }

    [Test]
    public void ShouldFallWhenInputContainsDangerousTagMaskedWithHtmlEncode()
    {
        const string input = "&lt;script>alert('xss')</script>";

        Action validate = () => XssValidator.Validate(input);

        validate.Should().Throw<XssValidationException>();
    }

    [Test]
    public void ShouldFallWhenInputContainsDangerousTagMaskedWithUtf8()
    {
        const string input = "\x3Cs\x63r\x69pt\x3E alert('xss')</script>";

        Action validate = () => XssValidator.Validate(input);

        validate.Should().Throw<XssValidationException>();
    }

    [Test]
    public void ShouldFallWhenInputContainsDangerousTagEncodedByBase64()
    {
        const string input = "<script>alert('xss')</script>";

        var bytes = Encoding.UTF8.GetBytes(input);
        var base64 = Convert.ToBase64String(bytes);

        Action validate = () => XssValidator.Validate(base64);

        validate.Should().Throw<XssValidationException>();
    }

    [Test]
    public void ShouldFailWhenInputIsArrayContainingBadString()
    {
        var input = new[] { "good", "bad one <script>" };

        Action validate = () => XssValidator.Validate(input);

        validate.Should().Throw<XssValidationException>();
    }

    [Test]
    public void ShouldFailWhenInputIsGenericEnumerableContainingBadString()
    {
        var input = new List<string> { "good", "bad one <script>" };

        Action validate = () => XssValidator.Validate(input);

        validate.Should().Throw<XssValidationException>();
    }

    [Test]
    public void ShouldFailWhenInputArrayContainsBadStringWithProperPath()
    {
        var input = new[] { "good", "another good", "bad one <script>" };

        Action validate = () => XssValidator.Validate(input);

        var expected = new XssValidationException.SuspiciousField("[2]", input[2]);

        validate.Should().Throw<XssValidationException>()
            .And.Suspicious.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void ShouldThrowExceptionWithProperPathToSuspiciousField()
    {
        var input = new {good = "good string", bad = "some bad string <script>"};

        Action validate = () => XssValidator.Validate(input);

        var expected = new XssValidationException.SuspiciousField("bad", input.bad);

        validate.Should().Throw<XssValidationException>()
            .And.Suspicious.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void ShouldThrowExceptionWithProperPathToSuspiciousFieldInObjectField()
    {
        var input = new { field1 = new { good = "good string", bad = "some bad string <script>" }};

        var expected = new XssValidationException.SuspiciousField("field1.bad", input.field1.bad);

        Action validate = () => XssValidator.Validate(input);
        validate.Should().Throw<XssValidationException>()
            .And.Suspicious.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void ShouldThrowExceptionWithProperPathToSuspiciousFieldInArrayField()
    {
        var input = new { array1 = new object[] { new {}, new { good = "good string", bad = "some bad string <script>" } , new {}} };

        var expected = new XssValidationException.SuspiciousField("array1[1].bad", ((dynamic)input.array1[1]).bad);

        Action validate = () => XssValidator.Validate(input);
        validate.Should().Throw<XssValidationException>()
            .And.Suspicious.Should().BeEquivalentTo(expected);
    }

    [Test]
    // TS-35521, TS-35573
    public void ShouldPassWhenDangerousStringIsNotATag()
    {
        const string input = "<xmp:MetadataDate>2014-07-11T17:19:59+04:00</xmp:MetadataDate>";

        Action validate = () => XssValidator.Validate(input);

        validate.Should().NotThrow<XssValidationException>();
    }

    [Test]
    // TS-35521, TS-35573
    public void ShouldPassWhenDangerousStringIsNotATag2()
    {
        const string input = "<MetadataDate>2014-07-11T17:19:59+04:00</MetadataDate>";

        Action validate = () => XssValidator.Validate(input);

        validate.Should().NotThrow<XssValidationException>();
    }

    [Test]
    [TestCase("BGSOUND SRC=\"javascript: alert('XSS');\"")]
    [TestCase("REL=\"stylesheet\" HREF=\"javascript: alert('XSS');\"")]
    [TestCase("HTTP-EQUIV=\"Link\" Content=\"< http://xss.rocks/xss.css>; REL=stylesheet\"")]
    [TestCase("IMG STYLE=\"xss: expr/*XSS*/ession(alert('XSS'))\"")]
    [TestCase("TABLE BACKGROUND=\"javascript: alert('XSS')\"")]
    [TestCase("TYPE=\"text/x-scriptlet\" DATA=\"http://xss.rocks/scriptlet.html\"")]
    [TestCase("img onload=\"eval(atob('ZG9jdW1lbnQubG9jYXRpb249Imh0dHA6Ly9saXN0ZXJuSVAvIitkb2N1bWVudC5jb29raWU='))\"")]
    [TestCase("type=text autofocus=true onfocus=console.log(1)")]
    [TestCase("img src=x onerror=\"alert(1)\"")]
    public void ShouldFallWhenInputContainsDangerousAttribute(string input)
    {
        Action validate = () => XssValidator.Validate(input);
        validate.Should().Throw<XssValidationException>();
    }
}