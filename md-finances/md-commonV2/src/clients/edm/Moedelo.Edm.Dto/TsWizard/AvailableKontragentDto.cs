using System;

namespace Moedelo.Edm.Dto.TsWizard
{
    public class AvailableKontragentDto
    {
        public string Name { get; set; }

        public string Guid { get; set; }

        public string Inn { get; set; }

        public string Kpp { get; set; }

        public int EdmSystem { get; set; } //todo подумать над инамом

        public string EdmsSystemName { get; set; }

        public int UnreadCount { get; set; }

        /// <summary>
        /// Если true, то контрагент уже есть в нашей таблице dbo.EdmInvites, переключаться на него нельзя
        /// </summary>
        public bool IsInLocalInvites { get; set; }

        /// <summary>
        /// Если true, то переключение на этого контрагента происходит со статуса 1
        /// </summary>
        public bool IsSwitchRelation { get; set; }

    }
}
