namespace Moedelo.Edm.Dto.TsWizard
{
    public sealed class SimilarKontragentDto
    {
        /// <summary>
        /// Идентификатор контрагента
        /// </summary>
        public int Id { get; set; }

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
        /// Текстовое описание состояния приглашения
        /// </summary>
        public string InvitationStatus { get; set; }
    }
}