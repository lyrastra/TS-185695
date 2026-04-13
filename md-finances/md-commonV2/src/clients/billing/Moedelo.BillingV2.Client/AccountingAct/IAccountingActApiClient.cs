using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BillingV2.Dto.AccountingAct;
using Moedelo.BillingV2.Dto.Billing;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BillingV2.Client.AccountingAct
{
    public interface IAccountingActApiClient : IDI
    {
        Task<List<PaymentAccountingActDto>> GetActsForPaymentAsync(int paymentId);

        [Obsolete("use GetActsFor1CAsync by criteria")]
        Task<List<PaymentAccountingActFor1CDto>> GetActsFor1CAsync(IEnumerable<PaymentHistoryDto> payments);
        
        Task<List<PaymentAccountingActFor1CDto>> GetActsFor1CAsync(AccountingActsCriteriaDto criteriaDto);
    }
}