using Moedelo.Money.Api.Controllers.PaymentOrders.Outgoing;
using Moedelo.Money.Enums;
using NUnit.Framework;

namespace Moedelo.Money.Api.Tests.PaymentOrders.Outgoing
{
    [TestFixture]
    public class PaymentDetailTests
    {
        /// <summary>
        /// Если добавляется новое значение "списания" в enum OperationType, то его нужно добавить в switch контроллера <see cref="PaymentDetailController"/> и сообщить команде интеграции
        /// Если направление нового значения не "списание", просто подправить тест
        /// </summary>
        [Test]
        public void OperationType_ShouldNotHaveNewValues()
        {
            // Arrange
            var expectedCount = 73; // Количество значений в OperationType на момент написания теста
            
            // Act
            var actualCount = Enum.GetValues(typeof(OperationType)).Length;

            // Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }
    }
}