using System.Collections.Generic;

namespace Moedelo.Edm.Dto.TsWizard
{
    public class InviteInfoDto
    {
        /// <summary>
        /// Решения
        /// </summary>
        public List<InviteDecisionDto> Decisions { get; set; }

        /// <summary>
        /// Контрагенты с этим ИНН (могут отличаться КПП и провайдером) из астральной книги
        /// </summary>
        public List<AvailableKontragentDto> AvailableKontragents { get; set; }

        /// <summary>
        /// Показывать ли форму отправки инвайта через globalId
        /// </summary>
        public bool ShowGlobalIdInviteForm { get; set; }

        /// <summary>
        /// Идентификатор контрагента
        /// </summary>
        public int KontragentId { get; set; }
    }
}
