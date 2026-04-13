using Moedelo.Common.Types;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.ProductIncome.Models;
using System.Threading.Tasks;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.ProductIncome
{
    /// <summary>
    /// Клиент для "Прихода без документов"
    /// </summary>
    public interface IPurchasesProductIncomeApiClient
    {
        /// <summary>
        /// Создает новый документ
        /// </summary>
        /// <param name="firmId"></param>
        /// <param name="userId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task CreateAsync(FirmId firmId, UserId userId, ProductIncomeCreateDto dto);
    }
}