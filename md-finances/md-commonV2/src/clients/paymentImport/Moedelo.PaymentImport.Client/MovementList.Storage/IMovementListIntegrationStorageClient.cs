using System.Threading;
using System.Threading.Tasks;
using Moedelo.PaymentImport.Dto;

namespace Moedelo.PaymentImport.Client.MovementList.Storage
{
    public interface IMovementListIntegrationStorageClient
    {
        /// <summary> Получение текста автовыписки </summary>
        /// <param name="path">Идентификатор файла</param>
        Task<string> GetTextAsync(string path);

        /// <summary> Получение автовыписки в виде массива байтов </summary>
        /// <param name="path">Идентификатор файла</param>
        /// <param name="cancellationToken"></param>
        Task<byte[]> GetBytesAsync(string path, CancellationToken cancellationToken = default);
        
        /// <summary> Сохранение файла автовыписки </summary>
        Task<string> SaveAsync(SaveMovementListDto dto);

        /// <summary> Удаление файла автовыписки </summary>
        /// <param name="path">Идентификатор файла</param>
        Task RemoveAsync(string path);
        
        /// <summary> Получение информации о файлах по идентификатору организации </summary>
        /// <param name="firmId">Идентификатор организации</param>
        Task<MovementFileInfoDto[]> GetAsync(int firmId);
    }
}