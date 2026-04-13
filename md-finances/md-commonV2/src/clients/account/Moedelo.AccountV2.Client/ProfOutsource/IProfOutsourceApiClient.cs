using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountV2.Dto;
using Moedelo.AccountV2.Dto.Account;
using Moedelo.AccountV2.Dto.Filter;
using Moedelo.AccountV2.Dto.FirmOnSeivice;
using Moedelo.AccountV2.Dto.ProfOutsource;
using Moedelo.AccountV2.Dto.Role;
using Moedelo.AccountV2.Dto.UserAccessControl;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountV2.Client.ProfOutsource
{
    public interface IProfOutsourceApiClient : IDI
    {
        Task<ProfOutsourceContextDto> GetOutsourceContextAsync(int firmId, int userId, CancellationToken cancellationToken = default);

        Task<List<InviteDto>> GetNewInvites(int firmId, int userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Отправить приглашение пользователю на аутсорс бухгалтерии
        /// </summary>
        Task<InviteDto> SendInviteAsync(int firmId, int userId, InviteDto dto);

        /// <summary>
        /// Принять приглашение на аутсорс бухгалтерии
        /// </summary>
        Task AcceptInviteAsync(int firmId, int userId, int inviteId);

        Task<List<string>> ServiceGroupAutocompleteAsync(int firmId, int userId, string query = null, int count = 10);

        Task<List<FirmRolesDto>> GetFirmRolesAsync(int firmId, int userId, IReadOnlyCollection<int> firmIds);

        Task<ListWithTotalCount<SlaveFirmDto>> MySlaveFirmsAsync(int firmId, int userId, FilterRequestDto<FirmFilterField> request);

        /// <summary>
        /// Возвращает количество доступных фирм для пользователя.
        /// Учитывает переносы фирм: пара связанных переносом фирм считается одной фирмой.
        /// </summary>
        /// <param name="userId">идентификатор пользователя</param>
        /// <param name="ct">токен отмены операции</param>
        /// <returns>количество фирм</returns>
        Task<int> CountUserAccessibleFirmsAsync(int userId, CancellationToken ct);

        /// <summary>
        /// Возвращает информацию о ПА, под которым находится фирма @slaveFirmId.
        /// Если фирма @slaveFirmId не находится под аутсорсом, возвращается null.
        /// </summary>
        Task<AccountDto> GetProfOutsourceForFirmAsync(int firmId, int userId, int slaveFirmId, CancellationToken cancellationToken = default);

        Task<List<FirmOnServiceDto>> GetFirmsOnServiceAsync(int firmId, int userId, IReadOnlyCollection<int> slaveFirmIds);

        Task<IReadOnlyCollection<FirmOnServiceDto>> GetProfOutsourceFirmsOnServiceByFirmIdsAsync(
            IReadOnlyCollection<int> firmIds,
            CancellationToken cancellationToken = default);

        Task<List<AutocompleteFirmDto>> MyFirmsAutocompleteAsync(int firmId, int userId, string query, int count);

        Task<Result> RemovePpaAsync(int firmId, int userId, IReadOnlyCollection<int> ids);

        Task RejectInvitesAsync(int firmId, int userId, IReadOnlyCollection<int> inviteIds);

        Task DetachSlaveFirmsAsync(int firmId, int userId, IReadOnlyCollection<int> slaveFirmIds);

        Task<Result> SetServiceGroupAsync(int firmId, int userId, int slaveFirmId, string name);

        Task<FirmInfoDto> GetSlaveFirmInfoAsync(int firmId, int userId, int slaveFirmId);

        /// <summary>
        /// Сохраняет личные данные профессионального аутсорсера
        /// </summary>
        Task<SaveOutsourceDto> SaveAsync(int firmId, int? userId, ProfOutsourceDto outsourceDto);

        Task<List<TariffDto>> MyOutsourceTariffsAsync(int firmId, int userId);

        Task<ListWithCountDto<ProfOutsourceDto>> GetAllAsync(int offset = 0, int count = 20);

        Task<ProfOutsourceDto> GetAsync(int id);

        /// <summary>
        /// Возвращает проф. аутсорсера партнёра 
        /// </summary>
        Task<ListWithCountDto<ProfOutsourceDto>> GetAvailableByRegionalPartnerIdAsync(
            int regionalPartnerId,
            int offset = 0,
            int count = 20);

        /// <summary>
        /// Возвращает проф. аутсорсеров, которые не закреплены за партнёром 
        /// </summary>
        Task<ListWithCountDto<ProfOutsourceDto>> GetAvailableAsync(int offset = 0, int count = 20);

        /// <summary>
        /// Обновляет проф. аутсорсера партнёра 
        /// </summary>
        /// <param name="regionalPartnerId">Идентификатор регионального партнёра</param>
        /// <param name="professionalOutsourcerId">Идентификатор проф. аутсорсера</param>
        /// <returns></returns>
        Task AttachAsync(int regionalPartnerId, int professionalOutsourcerId);

        /// <summary>
        /// Удаляет (отвязывает) проф. аутсорсера партнёра
        /// </summary>
        /// <param name="regionalPartnerId"></param>
        /// <returns></returns>
        Task DetachFromRegionalPartnerAsync(int regionalPartnerId);

        /// <summary>
        /// Возвращает проф. аутсорсеров ГлавУчет
        /// </summary>
        /// <returns></returns>
        Task<List<ProfOutsourceDto>> GetAttachedToGuAsync();
    }
}
