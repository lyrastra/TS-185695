using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy
{
    public interface IRequisitesAccessClient
    {
        Task<MoneyAccessDto> GetMoneyAccessAsync(FirmId firmId, UserId userId);
    }
}