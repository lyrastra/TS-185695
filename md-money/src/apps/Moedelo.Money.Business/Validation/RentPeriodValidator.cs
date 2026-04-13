using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Validation
{
    [InjectAsSingleton(typeof(RentPeriodValidator))]
    internal sealed class RentPeriodValidator
    {
        private readonly RentalPaymentItemReader reader;

        public RentPeriodValidator(RentalPaymentItemReader reader)
        {
            this.reader = reader;
        }

        public async Task ValidateAsync(IReadOnlyCollection<int> ids)
        {
            if (ids?.Any() != true)
            {
                throw new BusinessValidationException("RentPeriods", "Не указаны периоды оплаты аренды");
            }

            var periods = await reader.GetByIdsAsync(ids);
            var exept = ids.Except(periods.Select(x => x.Id).ToArray());

            if (exept.Any())
            {
                throw new BusinessValidationException("RentPeriod.Id", $"Не найдены периоды оплаты аренды с ид {String.Join(",", exept)}");
            }
        }
    }
}
