using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.PaymentImport.Dto;

namespace Moedelo.PaymentImport.Client.MovementList.Storage
{
    public interface IMovementListReconciliationStorageClient : IDI
    {
        /// <summary> Получение текста автовыписки </summary>
        /// <param name="path">Идентификатор файла</param>
        Task<string> GetTextAsync(string path);

        /// <summary> Получение автовыписки в виде массива байтов </summary>
        /// <param name="path">Идентификатор файла</param>
        Task<byte[]> GetBytesAsync(string path);
        
        /// <summary> Сохранение файла автовыписки </summary>
        Task<string> SaveAsync(SaveMovementListDto dto);
    }
}