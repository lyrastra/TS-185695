namespace Moedelo.Edm.Dto.Kontragent
{
    public class KontragentWithInvitationDto
    {
        /// <summary>
        /// ID приглашения в dbo.EdmInvites
        /// </summary>
        public int InviteId { get; set; }

        /// <summary>
        /// ID контрагента
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// Текстовое описание состояния приглашения
        /// </summary>
        public string InvitationStatus { get; set; }

        /// <summary>
        /// True, если ЭДО включено
        /// </summary>
        public bool IsEdmOn { get; set; }

        /// <summary>
        /// Наименование контрагента
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ИНН
        /// </summary>
        public string Inn { get; set; }

        /// <summary>
        /// КПП
        /// </summary>
        public string Kpp { get; set; }

        /// <summary>
        /// Идентификатор провайдера ЭДО
        /// </summary>
        public int EdmSystemId { get; set; }

        /// <summary>
        /// Название оператора ЭДО
        /// </summary>
        public string EdmSystemName { get; set; }

        /// <summary>
        /// Глобальный Id контрагента в системе кросспровайдерного взаимодействия
        /// </summary>
        public string KontragentGuid { get; set; }
    }
}
