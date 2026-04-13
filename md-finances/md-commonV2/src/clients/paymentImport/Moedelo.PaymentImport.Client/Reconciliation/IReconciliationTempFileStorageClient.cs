using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.PaymentImport.Dto;

namespace Moedelo.PaymentImport.Client.Reconciliation
{
    public interface IReconciliationTempFileStorageClient : IDI
    {
        /// <summary> Получение текста выписки </summary>
        /// <param name="path">Идентификатор файла</param>
        Task<string> GetTextAsync(string path);
        
        /// <summary> Сохранение файла автовыписки </summary>
        Task<string> SaveAsync(SaveReconciliationTempFileDto dto);
        
        /// <summary> Удаление файла автовыписки </summary>
        /// <param name="path">Идентификатор файла</param>
        Task RemoveAsync(string path);
    }
}