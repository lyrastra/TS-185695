using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BackofficeV2.Dto.RegionalPartnerPaymentImport;

namespace Moedelo.BackofficeV2.Client.RegionalPartnerPaymentImport
{
    public interface IRegionalPartnerPaymentImportInfoApiClient
    {
        Task<IReadOnlyCollection<RegionalPartnerPaymentImportInfoDto>> GetByIds(IReadOnlyCollection<int> ids);
    }
}
