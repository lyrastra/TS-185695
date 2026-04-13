namespace Moedelo.FileStorageV2.Dto
{
    public class FileUploadingResultDto
    {
        /// <summary>
        /// Имя файла, указанное при выгрузке файла
        /// </summary>
        public string SourceFileName { get; set; }
        /// <summary>
        /// Присвоенное имя файла
        /// </summary>
        public string UploadedFileName { get; set; }
    }
}
