namespace Moedelo.Edm.Dto.TsWizard
{
    /// <summary>
    /// DTO-шка для контрагента из адресной книги, не имеющего запись в dbo.EdmInvite
    /// </summary>
    public class KontragentWithoutInvitationDto : KontragentBaseDto
    {
        /// <summary>
        /// Текстовое описание состояния приглашения
        /// </summary>
        public string InvitationStatus => $"ЭДО включено (только у {this.EdmSystemName})";

        /// <summary>
        /// GlobalId контрагента в адресной книге
        /// </summary>
        public string KontragentEdmId { get; set; }
    }
}
