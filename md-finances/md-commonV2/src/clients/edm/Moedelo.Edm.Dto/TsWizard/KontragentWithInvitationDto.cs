namespace Moedelo.Edm.Dto.TsWizard
{
    /// <summary>
    /// DTO-шка для контрагента из адресной книги, имеющего запись в dbo.EdmInvites
    /// </summary>
    public class KontragentWithInvitationDto : KontragentBaseDto
    {
        /// <summary>
        /// ID приглашения в dbo.EdmInvites
        /// </summary>
        public int InviteId { get; set; }

        /// <summary>
        /// Текстовое описание состояния приглашения
        /// </summary>
        public string InvitationStatus { get; set; }

        /// <summary>
        /// True, если ЭДО включено
        /// </summary>
        public bool IsEdmOn { get; set; }
    }
}
