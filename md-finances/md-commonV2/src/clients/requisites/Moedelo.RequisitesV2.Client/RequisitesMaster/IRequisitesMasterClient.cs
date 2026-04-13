using Moedelo.Common.Enums.Enums.Requisites;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System;
using System.Threading.Tasks;

namespace Moedelo.RequisitesV2.Client.RequisitesMaster
{
    public interface IRequisitesMasterClient : IDI
    {
        Task<bool> GetLockingStatus(int firmId);

        [Obsolete("Use Moedelo.RptV2.Client.RequisitesMaster.RequisitesMasterClient")]
        Task<RequisitesMasterStatus> GetStatus(int firmId, int userId);
    }
}
