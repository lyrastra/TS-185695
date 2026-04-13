using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders;
using Moedelo.Money.Numeration.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Numeration.DataAccess.Abstractions.PaymentOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Numeration.Business.PaymentOrders
{
    [InjectAsSingleton(typeof(INumberGetter))]
    public class NumberGetter : INumberGetter
    {
        private readonly IPaymentOrderApiClient paymentOrderApiClient;
        private readonly IExecutionInfoContextAccessor context;
        private readonly IPaymentOrderNumberDao dao;

        public NumberGetter(
            IPaymentOrderApiClient paymentOrderApiClient,
            IExecutionInfoContextAccessor context, 
            IPaymentOrderNumberDao dao)
        {
            this.paymentOrderApiClient = paymentOrderApiClient;
            this.context = context;
            this.dao = dao;
        }

        public Task<int> Last(int settlementAccountId, int year)
        {
            var firmId = context.ExecutionInfoContext.FirmId;
            return dao.GetLast((int)firmId, settlementAccountId, year);
        }

        public async Task<(int, IReadOnlyCollection<int>)> LastAndNext(int settlementAccountId, int year, int? count = null)
        {
            if ((count ?? 0) <= 0)
                count = 1;

            var lastNumber = await Last(settlementAccountId, year);
            var currentNumbers = await paymentOrderApiClient.GetOutgoingNumbersAsync(settlementAccountId, year, lastNumber);
            var currentAndNewNumbers = new List<int>(currentNumbers);

            var nextNumbers = new List<int>();
            var next = lastNumber;
            for (int i = 1; i <= count; i++)
            {
                next = CalculateNext(currentAndNewNumbers, next);
                nextNumbers.Add(next);
                currentAndNewNumbers.Add(next);
            }
            return (lastNumber, nextNumbers);
        }

        private int CalculateNext(IReadOnlyCollection<int> numbers, int last)
        {
            var firstNext = last + 1;

            if (!numbers.Any())
                return firstNext;

            var next = numbers.Any(n => n == firstNext) ?
                numbers.FirstOrDefault(num => !numbers.Any(n => n == num + 1)) + 1 :
                firstNext;

            if (next <= 0)
                return firstNext;

            return next;
        }
    }
}