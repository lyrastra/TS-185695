using Moedelo.Common.Enums.Enums.Uploaded;

namespace Moedelo.UploadedFiles.Dto
{
    public class CreateUploadedFileDto
    {
        /// <summary>
        /// Имя файла
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Расширение
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// Отображаемое имя файла
        /// </summary>
        public string ScreenName { get; set; }

        /// <summary>
        /// Тип содержимого
        /// </summary>
        public UploadedFileContentType ContentType { get; set; }

        /// <summary>
        /// Тип сущности
        /// </summary>
        public EntityType EntityType { get; set; }

        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public long EntityId { get; set; }

        /// <summary>
        /// Идентификатор фирмы
        /// </summary>
        public int? FirmId { get; set; }
    }
}