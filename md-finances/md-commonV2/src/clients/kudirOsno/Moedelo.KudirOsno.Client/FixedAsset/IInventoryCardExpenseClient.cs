using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.KudirOsno.Client.FixedAsset.Dto;

namespace Moedelo.KudirOsno.Client.FixedAsset
{
    public interface IInventoryCardExpenseClient : IDI
    {
        /// <summary>
        /// Краткая информация по расходам на амортизацию в НУ из КУДИР ИП-ОСНО
        /// <param name="onDate">на какую дату рассчитать амортизацию</param>
        /// <param name="inventoryCardBaseId">фильтр по конкрентой ИК (без указания - по всем ИК)</param>
        /// </summary>
        Task<InventoryCardExpenseSummaryDto[]> GetSummaryAsync(int firmId, int userId, DateTime onDate, long? inventoryCardBaseId = null);
    }
}