using System;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Moedelo.InfrastructureV2.WebApi.Validation.Attributes;
using NUnit.Framework;

namespace ExternalTest.Validation.Attributes
{
    [TestFixture]
    public class EqualOrLaterThenAttributeTest
    {
        [Test]
        public void AttributeInstance_ShouldContainNotEmptyErrorMessage_Anyway()
        {
            var attr = new EqualOrLaterThenAttribute(nameof(TestingType.NullableDate2));
            attr.ErrorMessage.Should().NotBeNullOrEmpty("Должно содержать осмысленное непустое сообщение об ошибке");
        }

        [Test]
        [TestCase(null, null, true)]
        [TestCase(null, "2016-11-29", false)]
        [TestCase("2016-11-29", null, true)]
        [TestCase("2016-11-28", "2016-11-29", false)]
        [TestCase("2016-11-29", "2016-11-29", true)]
        [TestCase("2016-11-30", "2016-11-29", true)]
        public void GetValidationResult_ForBothNullableFields(string inspectedDate, string controlDate, bool shouldBeValid)
        {
            var obj = new TestingType
            {
                NullableDate1 = inspectedDate == null ? default(DateTime?) : DateTime.Parse(inspectedDate),
                NullableDate2 = controlDate == null ? default(DateTime?) : DateTime.Parse(controlDate),
            };

            var attr = new EqualOrLaterThenAttribute(nameof(TestingType.NullableDate2));

            var actual = attr.GetValidationResult(obj.NullableDate1, new ValidationContext(obj));
            var expected = shouldBeValid
                ? ValidationResult.Success
                : new ValidationResult(attr.ErrorMessage);

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        [TestCase("2016-11-28", "2016-11-29", false)]
        [TestCase("2016-11-29", "2016-11-29", true)]
        [TestCase("2016-11-30", "2016-11-29", true)]
        public void GetValidationResult_ForBothNotNullableFields(string inspectedDate, string controlDate, bool shouldBeValid)
        {
            var obj = new TestingType
            {
                NotNullableDate1 = DateTime.Parse(inspectedDate),
                NotNullableDate2 = DateTime.Parse(controlDate),
            };

            var attr = new EqualOrLaterThenAttribute(nameof(TestingType.NotNullableDate2));

            var actual = attr.GetValidationResult(obj.NotNullableDate1, new ValidationContext(obj));
            var expected = shouldBeValid
                ? ValidationResult.Success
                : new ValidationResult(attr.ErrorMessage);

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        [TestCase(null, "2016-11-29", false)]
        [TestCase("2016-11-28", "2016-11-29", false)]
        [TestCase("2016-11-29", "2016-11-29", true)]
        [TestCase("2016-11-30", "2016-11-29", true)]
        public void GetValidationResult_ForNullableInspectedAndNotNullableControlFields(string inspectedDate, string controlDate, bool shouldBeValid)
        {
            var obj = new TestingType
            {
                NullableDate1 = inspectedDate == null ? default(DateTime?) : DateTime.Parse(inspectedDate),
                NotNullableDate2 = DateTime.Parse(controlDate),
            };

            var attr = new EqualOrLaterThenAttribute(nameof(TestingType.NotNullableDate2));

            var actual = attr.GetValidationResult(obj.NullableDate1, new ValidationContext(obj));
            var expected = shouldBeValid
                ? ValidationResult.Success
                : new ValidationResult(attr.ErrorMessage);

            actual.ShouldBeEquivalentTo(expected);
        }


        [Test]
        [TestCase("2016-11-29", null, true)]
        [TestCase("2016-11-28", "2016-11-29", false)]
        [TestCase("2016-11-29", "2016-11-29", true)]
        [TestCase("2016-11-30", "2016-11-29", true)]
        public void GetValidationResult_ForNotNullableInspectedAndNullableControlFields(string inspectedDate, string controlDate, bool shouldBeValid)
        {
            var obj = new TestingType
            {
                NotNullableDate1 = DateTime.Parse(inspectedDate),
                NullableDate2 = controlDate == null ? default(DateTime?) : DateTime.Parse(controlDate),
            };

            var attr = new EqualOrLaterThenAttribute(nameof(TestingType.NullableDate2));

            var actual = attr.GetValidationResult(obj.NotNullableDate1, new ValidationContext(obj));
            var expected = shouldBeValid
                ? ValidationResult.Success
                : new ValidationResult(attr.ErrorMessage);

            actual.ShouldBeEquivalentTo(expected);
        }

        private class TestingType
        {
            public DateTime NotNullableDate1 { get; set; }
            public DateTime NotNullableDate2 { get; set; }
            public DateTime? NullableDate1 { get; set; }
            public DateTime? NullableDate2 { get; set; }
        }
    }
}