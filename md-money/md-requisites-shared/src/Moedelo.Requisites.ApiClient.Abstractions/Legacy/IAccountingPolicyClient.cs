using System.Threading.Tasks;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy
{
    /// <summary>
    /// https://github.com/moedelo/md-commonV2/blob/4387633dccf1baa6fba070ffb286f42248e75fbd/src/clients/requisites/Moedelo.RequisitesV2.Client/AccountingPolicy/AccountingPolicyClient.cs
    /// </summary>
    public interface IAccountingPolicyClient
    {
        Task<AccountingPolicyDto> GetOrCreateAsync(int firmId, int userId, int year);
    }
}