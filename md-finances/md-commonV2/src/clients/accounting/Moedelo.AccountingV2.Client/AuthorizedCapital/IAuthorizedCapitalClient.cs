using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.AuthorizedCapital;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.AuthorizedCapital
{
    public interface IAuthorizedCapitalClient : IDI
    {
        /// <summary>
        /// Возвращает первоначальный уставный капитал (УК)
        /// </summary>
        Task<AuthorizedCapitalDto> GetAsync(int firmId, int userId);

        /// <summary>
        /// Создает первоначальный уставный капитал (УК)
        /// </summary>
        Task CreateAsync(int firmId, int userId, AuthorizedCapitalDto authorizedCapital);

        /// <summary>
        /// Удаляет первоначальный уставный капитал (УК)
        /// </summary>
        Task DeleteAsync(int firmId, int userId);
    }
}