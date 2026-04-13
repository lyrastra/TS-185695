using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.Domain.MissingEmployee;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.MissingEmployee
{
    /// <summary>
    /// Начитывание неопознанных при импорте платёжек
    /// </summary>
    public interface IPaymentOrdersWithMissingEmployeeReader
    {
        /// <summary>
        /// Начитывание неопознанных при импорте платёжек по сотруднику
        /// </summary>
        Task<List<MissingEmployeePayment>> GetByEmployeeIdAsync(int employeeId);
    }
}