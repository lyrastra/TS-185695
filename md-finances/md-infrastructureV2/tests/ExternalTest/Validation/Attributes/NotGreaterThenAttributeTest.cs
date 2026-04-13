using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Moedelo.InfrastructureV2.WebApi.Validation.Attributes;
using NUnit.Framework;

namespace ExternalTest.Validation.Attributes
{
    [TestFixture]
    public class NotGreaterThenAttributeTest
    {
        private ValidationResult GetErrorValidationResult(NotGreaterThenAttribute attr)
        {
            return new ValidationResult(attr.FormatErrorMessage(string.Empty));
        }

        [Test]
        public void AttributeInstance_ShouldContainNotEmptyErrorMessage_Anyway()
        {
            var attr = new NotGreaterThenAttribute(nameof(TestingType.Nullable2));
            attr.FormatErrorMessage(string.Empty).Should().NotBeNullOrEmpty("Должно содержать осмысленное непустое сообщение об ошибке");
        }

        [Test]
        [TestCase(null, null, true)]
        [TestCase(null, 10, true)]
        [TestCase(10, null, false)]
        [TestCase(9, 10, true)]
        [TestCase(10, 10, true)]
        [TestCase(11, 10, false)]
        public void GetValidationResult_ForBothNullableFields(int? inspectedValue, int? controlValue, bool shouldBeValid)
        {
            var obj = new TestingType
            {
                Nullable1 = inspectedValue,
                Nullable2 = controlValue
            };

            var attr = new NotGreaterThenAttribute(nameof(TestingType.Nullable2));

            var actual = attr.GetValidationResult(obj.Nullable1, new ValidationContext(obj));
            var expected = shouldBeValid
                ? ValidationResult.Success
                : GetErrorValidationResult(attr);

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        [TestCase(9, 100, true)]
        [TestCase(100, 100, true)]
        [TestCase(110, 100, false)]
        public void GetValidationResult_ForBothNotNullableFields(int inspectedValue, int controlValue, bool shouldBeValid)
        {
            var obj = new TestingType
            {
                NotNullable1 = inspectedValue,
                NotNullable2 = controlValue,
            };

            var attr = new NotGreaterThenAttribute(nameof(TestingType.NotNullable2));

            var actual = attr.GetValidationResult(obj.NotNullable1, new ValidationContext(obj));
            var expected = shouldBeValid
                ? ValidationResult.Success
                : GetErrorValidationResult(attr);

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        [TestCase(null, 20, true)]
        [TestCase(9, 100, true)]
        [TestCase(100, 100, true)]
        [TestCase(110, 100, false)]
        public void GetValidationResult_ForNullableInspectedAndNotNullableControlFields(int? inspectedValue, int controlValue, bool shouldBeValid)
        {
            var obj = new TestingType
            {
                Nullable1 = inspectedValue,
                NotNullable2 = controlValue,
            };

            var attr = new NotGreaterThenAttribute(nameof(TestingType.NotNullable2));

            var actual = attr.GetValidationResult(obj.Nullable1, new ValidationContext(obj));
            var expected = shouldBeValid
                ? ValidationResult.Success
                : GetErrorValidationResult(attr);

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        [TestCase(12, null, false)]
        [TestCase(9, 10, true)]
        [TestCase(10, 10, true)]
        [TestCase(11, 10, false)]
        public void GetValidationResult_ForNotNullableInspectedAndNullableControlFields(int inspectedValue, int? controlValue, bool shouldBeValid)
        {
            var obj = new TestingType
            {
                NotNullable1 = inspectedValue,
                Nullable2 = controlValue,
            };

            var attr = new NotGreaterThenAttribute(nameof(TestingType.Nullable2));

            var actual = attr.GetValidationResult(obj.NotNullable1, new ValidationContext(obj));
            var expected = shouldBeValid
                ? ValidationResult.Success
                : GetErrorValidationResult(attr);

            actual.ShouldBeEquivalentTo(expected);
        }


        private class TestingType
        {
            public int NotNullable1 { get; set; }
            public int NotNullable2 { get; set; }
            public int? Nullable1 { get; set; }
            public int? Nullable2 { get; set; }
        }
    }
}