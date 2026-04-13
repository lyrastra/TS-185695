using System.Threading.Tasks;
using Moedelo.BackofficeV2.Dto.RegionalPartnerInfo;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BackofficeV2.Client.RegionalPartnerInfo
{
    public interface IRegionalPartnerInfoRequisitesApiClient : IDI
    {
        /// <summary> Возвращает реквизиты </summary>
        Task<RegionalPartnerInfoRequisitesDto> GetAsync(int regionalPartnerInfoId);
        
        Task<RegionalPartnerInfoRequisitesDto> GetMoeDeloAsync();
        
        Task<RegionalPartnerInfoRequisitesDto> GetGlavUchetAsync();

        Task<byte[]> GetStampAsync(int regionalPartnerInfoId);

        Task<byte[]> GetSignAsync(int regionalPartnerInfoId);
        
        Task<byte[]> GetSignAsync(int regionalPartnerInfoId, int index);
        
        Task<byte[]> GetLogoAsync(int regionalPartnerInfoId);
        
        Task<byte[]> GetMoeDeloStampAsync();
        
        Task<byte[]> GetMoeDeloSignAsync(int index);
        
        Task<byte[]> GetMoeDeloLogoAsync();
        
        Task<byte[]> GetGlavUchetStampAsync();
        
        Task<byte[]> GetGlavUchetSignAsync(int index);

        Task<byte[]> GetGlavUchetLogoAsync();
    }
}