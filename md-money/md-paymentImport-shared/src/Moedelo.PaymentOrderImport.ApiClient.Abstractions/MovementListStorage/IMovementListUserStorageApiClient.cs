using System.Threading.Tasks;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.PaymentOrderImport.ApiClient.Abstractions.Dto;

namespace Moedelo.PaymentOrderImport.ApiClient.Abstractions.MovementListStorage;

public interface IMovementListUserStorageApiClient
{
    /// <summary> Получение названия файла </summary>
    /// <param name="path">Идентификатор файла</param>
    Task<string> GetFileNameAsync(string path);

    /// <summary> Сохранение файла автовыписки </summary>
    Task<string> SaveAsync(SaveMovementListDto dto);

    /// <summary> Получение текста автовыписки </summary>
    /// <param name="path">Идентификатор файла</param>
    /// <param name="defaultSettings"></param>
    Task<string> GetTextAsync(string path, HttpQuerySetting defaultSettings = null);
}