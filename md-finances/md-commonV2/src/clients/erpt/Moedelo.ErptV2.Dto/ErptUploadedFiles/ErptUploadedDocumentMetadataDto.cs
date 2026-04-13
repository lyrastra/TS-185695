namespace Moedelo.ErptV2.Dto.ErptUploadedFiles
{
    public class ErptUploadedDocumentMetadataDto
    {
        public int FileId { get; set; }
        public int ReportId { get; set; }
        /// <summary>
        /// Отображаемое имя
        /// </summary>
        public string ScreenName { get; set; }
        /// <summary>
        /// Изначальное имя (иногда включает локальный путь внутри папки)
        /// Используй Path.GetFileName, чтобы гарантированно получить имя файла
        /// </summary>
        public string FileName { get; set; }

        public ErptUploadedFileType FileType { get; set; }
    }
}
