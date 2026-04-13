using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.Money;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.PayrollV2.Client.ChargePayments.DTO;
using Moedelo.PayrollV2.Client.Payments;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Finances.Business.Services.Money.Operations
{
    [InjectAsSingleton]
    public class MoneyOperationSalaryChargePaymentsReader : IMoneyOperationSalaryChargePaymentsReader
    {
        private readonly IChargePaymentsApiClient chargePaymentsApiClient;

        public MoneyOperationSalaryChargePaymentsReader(
            IChargePaymentsApiClient chargePaymentsApiClient)
        {
            this.chargePaymentsApiClient = chargePaymentsApiClient;
        }

        public async Task<Dictionary<long, decimal>> GetSalaryChargePaymentSumByIdsAsync(IUserContext userContext, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds.Count == 0)
            {
                return new Dictionary<long, decimal>();
            }
            var chargePayments = await chargePaymentsApiClient.GetByDocumentBaseIdsAsync(userContext.FirmId, userContext.UserId, baseIds).ConfigureAwait(false);
            if (chargePayments == null)
            {
                return new Dictionary<long, decimal>();
            }
            return chargePayments.Where(x => x.DocumentBaseId.HasValue)
                .ToDictionary(x => x.DocumentBaseId.Value, GetChargePaymentsSum); 
        }

        private static decimal GetChargePaymentsSum(WorkerChargePaymentsListDto chargePayments)
        {
            return chargePayments.WorkerChargePayments
                .SelectMany(c => c.ChargePayments)
                .Sum(c => c.Sum);
        }
    }
}