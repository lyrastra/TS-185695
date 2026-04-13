using System.Threading;
using System.Threading.Tasks;
using Moedelo.PaymentImport.Dto;

namespace Moedelo.PaymentImport.Client.MovementList.Storage
{
    public interface IMovementListUserStorageClient
    {
        /// <summary> Получение текста автовыписки </summary>
        /// <param name="path">Идентификатор файла</param>
        Task<string> GetFileNameAsync(string path);

        /// <summary> Сохранение файла автовыписки </summary>
        Task<string> SaveAsync(SaveMovementListDto dto);

        /// <summary> Получение автовыписки в виде массива байтов </summary>
        /// <param name="path">Идентификатор файла</param>
        /// <param name="cancellationToken"></param>
        Task<byte[]> GetBytesAsync(string path, CancellationToken cancellationToken = default);
    }
}