namespace Moedelo.BPM.CaseDocs.Client.Dtos
{
    /// <summary>
    ///     Создание документа в аккаунте
    /// </summary>
    public class CreateDocumentForAccountDto
    {
        /// <summary>
        ///     Аккаунт, к которому нужно прикрепить файл
        /// </summary>
        public int FirmId { get; set; }

        /// <summary>
        ///     Наименование файла
        /// </summary>
        public string FileName { get; set; }
    }
}