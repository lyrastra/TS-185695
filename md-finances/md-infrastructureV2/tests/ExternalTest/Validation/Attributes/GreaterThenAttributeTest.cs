// ReSharper disable ExpressionIsAlwaysNull

using System.ComponentModel.DataAnnotations;
using NUnit.Framework;
using FluentAssertions;
using Moedelo.InfrastructureV2.WebApi.Validation.Attributes;

namespace ExternalTest.Validation.Attributes
{
    [TestFixture]
    public class GreaterThenAttributeTest
    {
        private readonly ValidationResult errorResult = new ValidationResult("error");

        [Test]
        public void IsValid_CheckedPropValGreaterThenOtherValue_ReturnSuccess()
        {
            decimal? checkedPropVal = 66.25m;
            decimal? otherPropVal = 59.5m;
            var expected = ValidationResult.Success;

            var attr = new GreaterThenAttribute("Prop1");
            var actual = attr.GetValidationResult(checkedPropVal, new ValidationContext(new { Prop1 = otherPropVal }));

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void IsValid_CheckedPropValEqualsOtherValue_ReturnError()
        {
            decimal? checkedPropVal = 16.25m;
            decimal? otherPropVal = 16.25m;
            var expected = errorResult;

            var attr = new GreaterThenAttribute("Prop1") { ErrorMessage = errorResult.ErrorMessage};
            var actual = attr.GetValidationResult(checkedPropVal, new ValidationContext(new { Prop1 = otherPropVal }));

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void IsValid_CheckedPropValLessThanOtherValue_ReturnError()
        {
            decimal? checkedPropVal = 16.25m;
            decimal? otherPropVal = 76.4m;
            var expected = errorResult;

            var attr = new GreaterThenAttribute("Prop1") {ErrorMessage = errorResult.ErrorMessage};
            var actual = attr.GetValidationResult(checkedPropVal, new ValidationContext(new { Prop1 = otherPropVal }));

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        [TestCase(89)]
        [TestCase(0)]
        [TestCase(-7500)]
        [TestCase(null)]
        public void IsValid_CheckedPropValIsNull_ReturnSuccess(decimal? otherVal)
        {
            decimal? checkedPropVal = null;
            decimal? otherPropVal = otherVal;
            var expected = ValidationResult.Success;

            var attr = new GreaterThenAttribute("Prop1") {ErrorMessage = errorResult.ErrorMessage};
            var actual = attr.GetValidationResult(checkedPropVal, new ValidationContext(new { Prop1 = otherPropVal }));

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void IsValid_CheckedPropValLessThanOtherPropValueAndOtherPropTypeIsInt_ReturnError()
        {
            decimal? checkedPropVal = 3.55m;
            int? otherPropVal = 15;
            var expected = errorResult;

            var attr = new GreaterThenAttribute("Prop1") {ErrorMessage = errorResult.ErrorMessage};
            var actual = attr.GetValidationResult(checkedPropVal, new ValidationContext(new { Prop1 = otherPropVal }));

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        [TestCase(89)]
        [TestCase(0)]
        [TestCase(-7500)]
        public void IsValid_OtherPropValIsNullAndChechedPropHasValue_ReturnSuccess(decimal curValue)
        {
            decimal? checkedPropVal = curValue;
            decimal? otherPropVal = null;
            var expected = ValidationResult.Success;

            var attr = new GreaterThenAttribute("Prop1") { ErrorMessage = errorResult.ErrorMessage };
            var actual = attr.GetValidationResult(checkedPropVal, new ValidationContext(new { Prop1 = otherPropVal }));

            actual.ShouldBeEquivalentTo(expected);
        }
    }
}