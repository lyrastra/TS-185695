using System;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.RegistrationService;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Registration.Dto;

namespace Moedelo.Registration.Client
{
    public interface IRegistrationClient : IDI
    {
        /// <summary>
        /// Проверка занятости логина другим пользователем
        /// </summary>
        Task<bool> IsLoginBusyAsync(string login);

        /// <summary>
        /// Проверка на то, что указанная строка соответствует требованиям к формату логина moedelo.org
        /// </summary>
        Task<bool> IsLoginValidAsync(string login, CancellationToken cancellationToken = default);

        Task<LoginStatus> GetLoginStatusAsync(string login, CancellationToken cancellationToken = default);

        Task<RegistrationResultDto> RegistrationUserFromPartnerAsync(RegistrationFromPartnerUserInfoDto userInfo);

        Task<RegistrationResultDto> RegistrationAsync(RegistrationRequestDto request);

        Task<RegistrationResultDto> RegisterSsoAsync(RegisterSsoRequestDto model);

        Task<RegistrationResultDto> RegistrationFromBankEdsWizard(RegistrationFromBankEdsWizardRequest request);

        Task<GetAuthenticationInfoDto> GetAuthenticationInfoAsync(Guid registrationKey);

        Task<RegistrationResultDto> RegistrationWithoutPhoneValidationAsync(RegistrationRequestDto request);
    }
}
