using System.Threading.Tasks;
using Moedelo.Docs.Dto;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Docs.Client.DocsTemplates
{
    public interface IDocsTemplatesApiClient : IDI
    {
        /// <summary>
        /// Возвращает признак наличия пользовательского шаблона для счёта
        /// </summary>
        Task<bool> SimpleBillExistsAsync(int firmId, int userId);
        
        /// <summary>
        /// Возвращает признак наличия пользовательского шаблона для счёта-договора
        /// </summary>
        Task<bool> BillContractExistsAsync(int firmId, int userId);
        
        /// <summary>
        /// Возвращает признак наличия пользовательского шаблона для акта
        /// </summary>
        Task<bool> StatementExistsAsync(int firmId, int userId);

        /// <summary>
        /// Возвращает имя пользовательского шаблона для счёта
        /// </summary>
        Task<string> SimpleBillGetFileNameAsync(int firmId, int userId);
        
        /// <summary>
        /// Возвращает имя пользовательского шаблона для счёта-договора
        /// </summary>
        Task<string> BillContractGetFileNameAsync(int firmId, int userId);
        
        /// <summary>
        /// Возвращает имя пользовательского шаблона для акта
        /// </summary>
        Task<string> StatementGetFileNameAsync(int firmId, int userId);
    }
}