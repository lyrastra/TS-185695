using FluentAssertions;
using Moedelo.Money.PaymentOrders.Business.Tests.Snapshots;
using Moedelo.Money.PaymentOrders.Domain.Models.Snapshot;
using NUnit.Framework;

namespace Moedelo.Money.PaymentOrders.Business.Tests
{
    [TestFixture]
    public class SnapshotTests
    {
        [Test]
        [Ignore("Dont work on ci")]
        public void Deserialize_PaymentToSupplier_ShouldNotNull ()
        {
            var snapshot = XmlHelper.Deserialize<PaymentOrderSnapshot>(Resource.PaymentToSupplier);
            snapshot.Should().NotBeNull();
        }
    }
}