using System.Threading.Tasks;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.PaymentOrderImport.ApiClient.Abstractions.Dto;

namespace Moedelo.PaymentOrderImport.ApiClient.Abstractions.MovementListStorage;

public interface IMovementListIntegrationStorageApiClient
{
    /// <summary> Получение текста автовыписки </summary>
    /// <param name="path">Идентификатор файла</param>
    Task<string> GetTextAsync(string path, HttpQuerySetting defaultSettings = null);

    /// <summary> Получение автовыписки в виде массива байтов </summary>
    /// <param name="path">Идентификатор файла</param>
    Task<byte[]> GetBytesAsync(string path);

    /// <summary> Сохранение файла автовыписки </summary>
    Task<string> SaveAsync(SaveMovementListDto dto);

    /// <summary> Удаление файла автовыписки </summary>
    /// <param name="path">Идентификатор файла</param>
    Task RemoveAsync(string path);

    /// <summary> Получение информации о файлах по идентификатору организации </summary>
    /// <param name="firmId">Идентификатор организации</param>
    Task<MovementFileInfoDto[]> GetAsync(int firmId);
}