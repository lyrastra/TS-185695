using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using System.IO;

namespace Moedelo.BPM.CaseDocs.Client.Dtos
{
    /// <summary>
    ///     Создание документа в обращении
    /// </summary>
    public class CreateDocDto
    {
        /// <summary>
        ///     Идентификатор обращения в CRM
        /// </summary>
        public string CaseId { get; set; }

        /// <summary>
        ///     Название документа.
        ///     Если не указано, то будет браться из имени прикрепленного файла
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Владелец документа.
        ///     Если не указан, то берется из обращения
        /// </summary>
        public string OwnerId { get; set; }

        /// <summary>
        ///     Идентификатор почтового адреса из CRM/
        ///     Необязательно
        /// </summary>
        public string EmailId { get; set; }

        /// <summary>
        ///     Идентификатор сообщения CRM, к которому надо прикрепить документ.
        ///     Если не указан, то документ прикрепится к обращению.
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        ///     Идентификатор файлового хранилища
        ///     Необязательно
        /// </summary>
        public int? FileStorageId { get; set; }

        /// <summary>
        ///     Информация об источнике.
        ///     Необязательно
        /// </summary>
        public string SourceInfo { get; set; }

        /// <summary>
        ///     Идентификатор фирмы МД
        /// </summary>
        public int? FirmId { get; set; }

        /// <summary>
        ///     Идентифкатор пользователя МД
        /// </summary>
        public int? UserId { get; set; }
    }
}