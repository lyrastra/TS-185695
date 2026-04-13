using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.ReceiptStatements
{
    public interface ILinkedPaymentOrderUpdater
    {
        /// <summary>
        /// Убирает признак Основной контрагент у платежек
        /// и изменяет проводки
        /// </summary>
        Task UpdatePaymentsAsync(IReadOnlyCollection<long> ids);
    }
}
