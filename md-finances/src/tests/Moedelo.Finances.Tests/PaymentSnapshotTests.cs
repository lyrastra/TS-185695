using FluentAssertions;
using Moedelo.Finances.Domain.Models.Money.Operations.PaymentOrders;
using NUnit.Framework;
using System.IO;
using System.Xml.Serialization;

namespace Moedelo.Finances.Tests
{
    [TestFixture]
    public class PaymentSnapshotTests
    {
        [Test(Description = "Snapshot deserialize")]
        public void SnapshotDeserialize ()
        {
            PaymentSnapshot snapshot = null;
            var serializer = new XmlSerializer(typeof(PaymentSnapshot));
            using (var reader = new StringReader(Resources.payment_snapshot))
            {
                snapshot = serializer.Deserialize(reader) as PaymentSnapshot;
            }

            snapshot.Kbk.Should().BeEquivalentTo("18210202010061010160");
        }
    }
}
