using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.RequisitesV2.Dto;
using Moedelo.RequisitesV2.Dto.AccountingPolicy;

namespace Moedelo.RequisitesV2.Client.AccountingPolicy
{
    public interface IAccountingPolicyClient
    {
        Task<AccountingPolicyDto> GetOrCreateAsync(int firmId, int userId, int year);
        Task SaveAsync(int firmId, int userId, AccountingPolicyDto dto);

        /// <summary>
        /// Информация о возможностях смены текущей системы налогообложения
        /// с УСН на ОСНО и наоборот
        /// </summary>
        Task<AccountingPolicyChangeSettingsDto> CanTransferUsnOsnoAsync(int firmId, int userId);

        /// <summary>
        /// Перевести текущую систему налогообложенияя системы
        /// с УСН на ОСНО и наоборот
        /// </summary>
        Task<AccountingPolicyChangeResultDto> TransferUsnOsnoAsync(int firmId, int userId, TaxationSystemType taxType);
    }
}