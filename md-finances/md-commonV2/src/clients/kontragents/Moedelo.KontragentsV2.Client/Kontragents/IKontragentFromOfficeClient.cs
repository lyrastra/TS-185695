using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.KontragentsV2.Dto;

namespace Moedelo.KontragentsV2.Client.Kontragents
{
    public interface IKontragentFromOfficeClient : IDI
    {
        /// <summary>
        /// Возвращает НЕСОХРАНЕННОГО контрагента с предзаполненными по ИНН реквизитами 
        /// </summary>
        Task<KontragentDto> GetAsync(int firmId, int userId, string inn);
    }
}