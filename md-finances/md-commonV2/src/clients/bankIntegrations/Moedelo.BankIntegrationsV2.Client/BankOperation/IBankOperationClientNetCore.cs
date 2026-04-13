using Moedelo.BankIntegrationsV2.Dto.BankOperation;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrationsV2.Client.BankOperation
{
    public interface IBankOperationClientNetCore : IDI
    {
        /// <summary> Послать основной запрос на выписку от лица всех пользователей интеграции (Многопоточный) </summary>
        Task<bool> SendDailyRequestAsync(DateTime beginDate, DateTime finishDate, int integrationPartner, bool withoutCheckCreated = false);

        Task<bool> SendDailyLagRequestAsync(DateTime beginDate, DateTime finishDate, int integrationPartner);

        /// <summary> Запрос выписки из банка </summary>
        Task<RequestMovementListResponseNetCoreDto> RequestMovementListAsync(RequestMovementListRequestDto dto);

        /// Создание черновика платежного реестра
        Task<CreateSalaryPaymentRegistryResponseDto> CreateSalaryPaymentRegistryAsync(SalaryPaymentRegistryDto dto);
    }

}
