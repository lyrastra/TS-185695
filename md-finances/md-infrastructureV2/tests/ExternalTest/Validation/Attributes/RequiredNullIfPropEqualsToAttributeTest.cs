using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Moedelo.InfrastructureV2.WebApi.Validation.Attributes;
using NUnit.Framework;

// ReSharper disable ExpressionIsAlwaysNull

namespace ExternalTest.Validation.Attributes
{
    [TestFixture]
    public class RequiredNullIfPropEqualsToAttributeTest
    {
        private const string errorMessage = "Error";
        private readonly ValidationResult errorResult = new ValidationResult(errorMessage);

        [Test]
        public void IsValid_WhenOtherPropMustBeNullAndEqualsToNullAndCheckedPropIsNull_ReturnsSuccess()
        {
            int? otherPropRequiredVal = null;
            int? otherPropVal = null;
            int? checkedProp = null;
            var expected = ValidationResult.Success;

            var attr = new RequiredNullIfPropEqualsToAttribute("Prop1", otherPropRequiredVal, errorMessage);

            var actual = attr.GetValidationResult(checkedProp, new ValidationContext(new { Prop1 = otherPropVal }));

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void IsValid_WhenOtherPropMustBeNullAndEqualsToNullAndCheckedPropIsNotNull_ReturnsError()
        {
            int? otherPropRequiredVal = null;
            int? otherPropVal = null;
            int? checkedProp = 5;

            var expected = errorResult;

            var attr = new RequiredNullIfPropEqualsToAttribute("Prop1", otherPropRequiredVal, errorMessage);

            var actual = attr.GetValidationResult(checkedProp, new ValidationContext(new { Prop1 = otherPropVal }));

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void IsValid_WhenOtherPropMustBeNotNullAndEqualsToNullAndCheckedPropIsNull_ReturnSuccess()
        {
            int? otherPropRequiredVal = 1;
            int? otherPropVal = null;
            int? checkedProp = null;

            var expected = ValidationResult.Success;

            var attr = new RequiredNullIfPropEqualsToAttribute("Prop1", otherPropRequiredVal, errorMessage);

            var actual = attr.GetValidationResult(checkedProp, new ValidationContext(new { Prop1 = otherPropVal }));

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void IsValid_WhenOtherPropHasRequiredValueAndCheckedPropIsNotNull_ReturnError()
        {
            int? otherPropRequiredVal = 1;
            int? otherPropVal = 1;
            int? checkedProp = 2;

            var expected = errorResult;

            var attr = new RequiredNullIfPropEqualsToAttribute("Prop1", otherPropRequiredVal, errorMessage);

            var actual = attr.GetValidationResult(checkedProp, new ValidationContext(new { Prop1 = otherPropVal }));

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void IsValid_WhenOtherPropHasRequiredValueAndCheckedPropIsNull_ReturnSuccess()
        {
            int? otherPropRequiredVal = 1;
            int? otherPropVal = 1;
            int? checkedProp = null;

            var expected = ValidationResult.Success;

            var attr = new RequiredNullIfPropEqualsToAttribute("Prop1", otherPropRequiredVal, errorMessage);

            var actual = attr.GetValidationResult(checkedProp, new ValidationContext(new { Prop1 = otherPropVal }));

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void IsValid_WhenOtherPropHasNotRequiredValueAndCheckedPropIsNull_ReturnSuccess()
        {
            int? otherPropRequiredVal = 1;
            int? otherPropVal = 2;
            int? checkedProp = null;

            var expected = ValidationResult.Success;

            var attr = new RequiredNullIfPropEqualsToAttribute("Prop1", otherPropRequiredVal, errorMessage);

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

            var attr = new RequiredNullIfPropEqualsToAttribute("Prop1", otherPropRequiredVal, errorMessage);

            var actual = attr.GetValidationResult(checkedProp, new ValidationContext(new { Prop1 = otherPropVal }));

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void IsValid_WhenOtherPropHasNotRequiredValueAndCheckedPropIsNotNull_ReturnSuccess()
        {
            int? otherPropRequiredVal = 1;
            int? otherPropVal = 2;
            int? checkedProp = 3;

            var expected = ValidationResult.Success;

            var attr = new RequiredNullIfPropEqualsToAttribute("Prop1", otherPropRequiredVal, errorMessage);

            var actual = attr.GetValidationResult(checkedProp, new ValidationContext(new { Prop1 = otherPropVal }));

            actual.ShouldBeEquivalentTo(expected);
        }
    }
}