using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Contracts;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.RentPayment;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.RentPayment
{
    [InjectAsSingleton(typeof(RentPeriodsReader))]
    class RentPeriodsReader
    {
        private readonly RentalPaymentItemReader rentalPaymentItemReader;

        public RentPeriodsReader(
            RentalPaymentItemReader rentalPaymentItemReader)
        {
            this.rentalPaymentItemReader = rentalPaymentItemReader;
        }

        public async Task<IReadOnlyCollection<RentPeriod>> GetAsync(IReadOnlyCollection<RentPeriod> rentPeriods)
        {
            var rentalItemIds = rentPeriods.Select(x => x.RentalPaymentItemId).ToArray();
            var rentalItems = rentalItemIds.Length > 0
                ? await rentalPaymentItemReader.GetByIdsAsync(rentalItemIds)
                : Array.Empty<RentalPaymentItem>();

            // заполним описание периодов
            return FillPeriodDescription(rentPeriods, rentalItems);
        }

        private static IReadOnlyCollection<RentPeriod> FillPeriodDescription(
            IReadOnlyCollection<RentPeriod> rentPeriods,
            IReadOnlyCollection<RentalPaymentItem> rentalItems)
        {
            foreach (var period in rentPeriods)
            {
                var item = rentalItems.FirstOrDefault(x => x.Id == period.RentalPaymentItemId);
                if (item == null)
                {
                    continue;
                }

                var datePresent = item.PaymentDate.HasValue
                   ? item.PaymentDate.Value.ToString("MMM yyyy", CultureInfo.CreateSpecificCulture("ru-RU"))
                   : "";

                period.PaymentType = item.PaymentSum > 0 ? RentPeriodType.Monthly : RentPeriodType.Buyout;
                period.Description = period.PaymentType == RentPeriodType.Monthly
                   ? $"Ежемесячный платеж, {datePresent}"
                   : "Выкупная стоимость";
            }

            return rentPeriods;
        }
    }
}
