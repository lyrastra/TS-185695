using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.AccountingStatement;

namespace Moedelo.AccountingV2.Client.AccountingStatement
{
    /// <summary>
    /// Учёт фиксированного и дополнительного взносов для ИП на УСН Доходы-Расходы
    /// </summary>
    public interface IUsnFundPaymentExpensesApiClient
    {
        /// <summary>
        /// Возвращает статус бухгалтерской справки с расходом в НУ (ФВ и ДВ)
        /// </summary>
        Task<UsnFundPaymentExpensesStatementStatus> GetStatusAsync(int firmId, int userId, int year);
    }
}
