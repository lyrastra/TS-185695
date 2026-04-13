using System;
using System.Threading.Tasks;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.ClosedCashGroup;
using Moedelo.Finances.Domain.Interfaces.DataAccess.CloseCashGroup;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;

namespace Moedelo.Finances.Business.Services.ClosedCashGroup
{
    [InjectAsSingleton]
    public class CloseCashGroupService : ICloseCashGroupService
    {
        private readonly ICloseCashGroupDao _closeCashGroupDao;

        public CloseCashGroupService(ICloseCashGroupDao closeCashGroupDao)
        {
            _closeCashGroupDao = closeCashGroupDao;
        }

        public async Task<DateTime?> GetLastClosedCashDateAsync(int firmId)
        {
            return await _closeCashGroupDao.GetLastClosedCashDateAsync(firmId).ConfigureAwait(false);
        }
    }
}
