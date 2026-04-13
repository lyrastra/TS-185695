using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RequisitesV2.Dto.UserAccess;

namespace Moedelo.RequisitesV2.Client.UserAccess
{
    public interface IRequisitesAccessClient : IDI
    {
        /// <summary>
        /// Доступ к информации в подразделе "Деньги" в реквизитах
        /// </summary>
        Task<MoneyAccessDto> GetMoneyAccessAsync(int firmId, int userId);
    }
}