namespace Moedelo.BPM.CaseDocs.Client.Dtos
{
    /// <summary>
    ///     Создание документа в аккаунте из ссылки на облачное хранилище
    /// </summary>
    public class CreateDocumentFromCloudLinkDto
    {
        /// <summary>
        ///     Аккаунт, к которому нужно прикрепить файл
        /// </summary>
        public int FirmId { get; set; }

        /// <summary>
        ///     Ссылка на облачное хранилище
        /// </summary>
        public string CloudLink { get; set; }
    }
}