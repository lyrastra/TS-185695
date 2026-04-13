using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Moedelo.InfrastructureV2.WebApi.Validation.Attributes;
using NUnit.Framework;

// ReSharper disable ExpressionIsAlwaysNull

namespace ExternalTest.Validation.Attributes
{
    [TestFixture]
    public class RequiredIfPropEqualsToAttributeTest
    {
        private const string errorMessage = "Error";
        private readonly ValidationResult errorResult = new ValidationResult(errorMessage);

        [Test]
        public void IsValid_WhenOtherPropMustBeNullAndEqualsToNullAndCheckedPropIsNull_ReturnsError()
        {
            int? otherPropRequiredVal = null;
            int? otherPropVal = null;
            int? checkedProp = null;
            var expected = errorResult;

            var attr = new RequiredIfPropEqualsToAttribute("Prop1", otherPropRequiredVal, errorMessage);

            var actual = attr.GetValidationResult(checkedProp, new ValidationContext(new { Prop1 = otherPropVal }));

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void IsValid_WhenOtherPropMustBeNullAndEqualsToNullAndCheckedPropIsNotNull_ReturnsSuccess()
        {
            int? otherPropRequiredVal = null;
            int? otherPropVal = null;
            int? checkedProp = 5;

            var expected = ValidationResult.Success;

            var attr = new RequiredIfPropEqualsToAttribute("Prop1", otherPropRequiredVal, errorMessage);

            var actual = attr.GetValidationResult(checkedProp, new ValidationContext(new { Prop1 = otherPropVal }));

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void IsValid_WhenOtherPropMustBeNullAndEqualsToNullAndCheckedPropHasValidValue_ReturnsSuccess()
        {
            int? otherPropRequiredVal = null;
            int? otherPropVal = null;
            int? checkedProp = 5;
            object[] validValues = {1, 2, 3, 5};

            var expected = ValidationResult.Success;

            var attr = new RequiredIfPropEqualsToAttribute("Prop1", otherPropRequiredVal, errorMessage)
            {
                ValidValues = validValues
            };

            var actual = attr.GetValidationResult(checkedProp, new ValidationContext(new { Prop1 = otherPropVal }));

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void IsValid_WhenOtherPropMustBeNullAndEqualsToNullAndCheckedPropHasNotValidValue_ReturnError()
        {
            int? otherPropRequiredVal = null;
            int? otherPropVal = null;
            int? checkedProp = 100;
            object[] validValues = {1, 2, 3, 5};

            var expected = errorResult;

            var attr = new RequiredIfPropEqualsToAttribute("Prop1", otherPropRequiredVal, errorMessage)
            {
                ValidValues = validValues
            };

            var actual = attr.GetValidationResult(checkedProp, new ValidationContext(new { Prop1 = otherPropVal }));

            actual.ShouldBeEquivalentTo(expected, opt => opt.Excluding(x => x.ErrorMessage));
            actual?.ErrorMessage.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void IsValid_WhenOtherPropMustBeNotNullAndEqualsToNullAndCheckedPropIsNull_ReturnSuccess()
        {
            int? otherPropRequiredVal = 1;
            int? otherPropVal = null;
            int? checkedProp = null;

            var expected = ValidationResult.Success;

            var attr = new RequiredIfPropEqualsToAttribute("Prop1", otherPropRequiredVal, errorMessage);

            var actual = attr.GetValidationResult(checkedProp, new ValidationContext(new { Prop1 = otherPropVal }));

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void IsValid_WhenOtherPropHasRequiredValueAndCheckedPropIsNotNull_ReturnSuccess()
        {
            int? otherPropRequiredVal = 1;
            int? otherPropVal = 1;
            int? checkedProp = 2;

            var expected = ValidationResult.Success;

            var attr = new RequiredIfPropEqualsToAttribute("Prop1", otherPropRequiredVal, errorMessage);

            var actual = attr.GetValidationResult(checkedProp, new ValidationContext(new { Prop1 = otherPropVal }));

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void IsValid_WhenOtherPropHasRequiredValueAndCheckedPropIsNull_ReturnError()
        {
            int? otherPropRequiredVal = 1;
            int? otherPropVal = 1;
            int? checkedProp = null;

            var expected = errorResult;

            var attr = new RequiredIfPropEqualsToAttribute("Prop1", otherPropRequiredVal, errorMessage);

            var actual = attr.GetValidationResult(checkedProp, new ValidationContext(new { Prop1 = otherPropVal }));

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        [TestCase(null)]
        [TestCase(3)]
        public void IsValid_WhenOtherPropHasNotRequiredValueAndCheckedPropHasAnyValue_ReturnSuccess(int? checkedProp)
        {
            int? otherPropRequiredVal = 1;
            int? otherPropVal = 2;

            var expected = ValidationResult.Success;

            var attr = new RequiredIfPropEqualsToAttribute("Prop1", otherPropRequiredVal, errorMessage);

            var actual = attr.GetValidationResult(checkedProp, new ValidationContext(new { Prop1 = otherPropVal }));

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void IsValid_WhenOtherPropHasNotRequiredNullValueAndCheckedPropIsNull_ReturnSuccess()
        {
            int? otherPropRequiredVal = 1;
            int? otherPropVal = null;
            int? checkedProp = null;

            var expected = ValidationResult.Success;

            var attr = new RequiredIfPropEqualsToAttribute("Prop1", otherPropRequiredVal, errorMessage);

            var actual = attr.GetValidationResult(checkedProp, new ValidationContext(new { Prop1 = otherPropVal }));

            actual.ShouldBeEquivalentTo(expected);
        }
    }
}