using System.Threading.Tasks;

namespace Moedelo.LinkedDoocuments.Client.Abstractions.BulkOperations
{
    /// <summary>
    /// Автопривязка документов и денег
    /// </summary>
    /// <note>
    /// Актуальная версия находится в md-linkedDocuments-shared (не может использоваться в net framework)
    /// </note>
    public interface IMoneyAndDocumentsAutoLinkingApiClient
    {
        /// <summary>
        /// Включена ли автопривязка
        /// </summary>
        Task<bool> IsEnabledAsync(int firmId, int userId);
        
        /// <summary>
        /// Пересвязать документы и деньги
        /// </summary>
        Task RelinkAsync(int firmId, int userId);
    }
}