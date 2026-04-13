using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Kbks
{
    internal interface IKbkReader
    {
        Task<IReadOnlyCollection<Kbk>> GetAllAsync();
        Task<Kbk> GetByIdAsync(int kbkId);
        Task<IReadOnlyCollection<Kbk>> GetByIdsAsync(IReadOnlyCollection<int> kbkIds);
        Task<Kbk> GetAsync(string kbk, DateTime date);
        Task<Kbk> GetAsync(int kbkId, DateTime periodEndDate, DateTime? date, bool isOoo);
        Task<Kbk[]> GetKbkByAccountCodeAsync(BudgetaryKbkRequest request, bool isOoo = false);
    }
}
