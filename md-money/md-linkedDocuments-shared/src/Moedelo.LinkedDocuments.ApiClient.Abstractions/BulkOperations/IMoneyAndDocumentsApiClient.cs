using System;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.LinkedDocuments.ApiClient.Abstractions.BulkOperations
{
    /// <summary>
    /// Автосвязывание документов и платежей
    /// </summary>
    public interface IMoneyAndDocumentsApiClient
    {
        /// <summary>
        /// Включено ли автосвязывание
        /// </summary>
        Task<bool> IsEnabledAsync();

        /// <summary>
        /// Запустить автосвязывание
        /// 
        /// Внимание! Тяжелая операция: длится до нескольких минут, влечет перепроведение большого кол-ва документов.
        /// </summary>
        /// <param name="endDate">до указанной даты</param>
        /// <param name="setting"></param>
        Task RelinkAsync(DateTime? endDate = null, HttpQuerySetting setting = null);
    }
}