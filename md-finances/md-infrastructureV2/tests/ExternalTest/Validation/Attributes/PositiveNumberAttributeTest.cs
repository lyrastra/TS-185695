using FluentAssertions;
using Moedelo.InfrastructureV2.WebApi.Validation.Attributes;
using NUnit.Framework;

namespace ExternalTest.Validation.Attributes
{
    [TestFixture]
    public class PositiveNumberAttributeTest
    {
        private PositiveNumberAttribute attr;

        [SetUp]
        public void SetUp()
        {
            attr = new PositiveNumberAttribute();
        }

        [Test(Description = "null-значение не проходит валидацию")]
        public void IsValid_ReturnsFalse_IfNullPassedAndNullIsNotAllowed()
        {
            attr.IsValid(default(int?)).Should().BeFalse();
        }

        [Test(Description = "null-значение проходит валидацию, если это разрешено")]
        public void IsValid_ReturnsFalse_IfNullPassedAndNullIsAllowed()
        {
            attr.AllowNull = true;
            attr.IsValid(default(int?)).Should().BeTrue();
        }

        [Test(Description = "целочисленные значения")]
        [TestCase(-1, false, Reason = "Отрицательное значение невалдино")]
        [TestCase(0, false, Reason = "Нулевое значение невалидно")]
        [TestCase(1, true, Reason = "Положительное значение валидно")]
        public void IsValid_ReturnsProperValue_ForInteger(int value, bool result)
        {
            attr.IsValid(value).Should().Be(result);
        }

        [Test(Description = "decimal-значения")]
        [TestCase(-0.1, false, Reason = "Отрицательное значение невалдино")]
        [TestCase(0.0, false, Reason = "Нулевое значение невалидно")]
        [TestCase(0.1, true, Reason = "Положительное значение (0<x<0.5) валидно")]
        [TestCase(0.9, true, Reason = "Положительное значение (0.5<x<1) валидно")]
        [TestCase(1.2, true, Reason = "Положительное значение больше 1 валидно")]
        public void IsValid_ReturnsProperValue_ForDecimal(decimal value, bool result)
        {
            attr.IsValid(value).Should().Be(result);
        }

        [Test(Description = "float-значения")]
        [TestCase(-0.1f, false, Reason = "Отрицательное значение невалдино")]
        [TestCase(0.0f, false, Reason = "Нулевое значение невалидно")]
        [TestCase(0.1f, true, Reason = "Положительное значение (0<x<0.5) валидно")]
        [TestCase(0.9f, true, Reason = "Положительное значение (0.5<x<1) валидно")]
        [TestCase(1.2f, true, Reason = "Положительное значение больше 1 валидно")]
        public void IsValid_ReturnsProperValue_ForFloat(float value, bool result)
        {
            attr.IsValid(value).Should().Be(result);
        }

        [Test(Description = "double-значения")]
        [TestCase(-0.1, false, Reason = "Отрицательное значение невалидно")]
        [TestCase(0.0, false, Reason = "Нулевое значение невалидно")]
        [TestCase(0.1, true, Reason = "Положительное значение (0<x<0.5) валидно")]
        [TestCase(0.9, true, Reason = "Положительное значение (0.5<x<1) валидно")]
        [TestCase(1.2, true, Reason = "Положительное значение больше 1 валидно")]
        public void IsValid_ReturnsProperValue_ForFDouble(double value, bool result)
        {
            attr.IsValid(value).Should().Be(result);
        }
    }
}