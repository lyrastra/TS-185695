using System;
using System.Collections.Generic;

namespace Moedelo.Edm.Dto.TsWizard
{
    /// <summary>
    /// DTO-шка, отдаваемая методом получения списка контрагентов
    /// </summary>
    public class KontragentsListDto
    {
        /// <summary>
        /// Список контрагентов из адресной книги Астрала, для которых есть записи в dbo.EdmInvites
        /// </summary>
        public List<KontragentWithInvitationDto> KontragentsWithInvitations { get; set; }

        /// <summary>
        /// Эти контрагенты из адресной книги Астрала есть у пользователя, но для них нет записей в dbo.EdmInvites
        /// </summary>
        public List<KontragentWithoutInvitationDto> ExistingKontragentsWithoutInvitations { get; set; }

        /// <summary>
        /// Этих контрагентов из адресной книги Астрала нет у пользователя, и для них нет записей в dbo.EdmInvites
        /// </summary>
        public List<KontragentWithoutInvitationDto> NotExistingKontragentsWithoutInvitations { get; set; }
    }
}
