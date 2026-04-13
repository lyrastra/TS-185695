using System.Threading.Tasks;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.PaymentOrderImport.ApiClient.Abstractions.Dto;

namespace Moedelo.PaymentOrderImport.ApiClient.Abstractions.Reconciliation;

public interface IReconciliationTempFileApiClient
{
    /// <summary>
    /// Получение текста выписки-сверки
    /// </summary>
    Task<string> GetTextAsync(string path, HttpQuerySetting defaultSettings = null);

    /// <summary>
    /// Сохранение файла выписки-сверки
    /// </summary>
    Task<string> SaveAsync(SaveReconciliationTempFileDto dto);
}