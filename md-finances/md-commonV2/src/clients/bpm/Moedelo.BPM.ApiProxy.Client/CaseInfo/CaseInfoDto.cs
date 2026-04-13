using System;

namespace Moedelo.BPM.ApiProxy.Client.CaseInfo
{
    public enum CaseInfoStatus
    {
        Closed,
        Processing,
        AwaitUser
    }
    public enum CaseTypeDto
    {
        Outsourcing,
        Consulting,
        JuridicalConsulting,
        None
    }
    public enum DocumentType
    {
        Document,
        Note
    }
    //TODO: отрефакторить, дто должны отдаваться в контроллере, сейчас не связаны модели и дто
    public class CaseInfoDto
    {
        /// <summary>
        ///     CRM идентификатор обращения
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     CRM идентификатор обращения
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        ///     Тема обращения
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        ///     Описание обращения
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Номер обращения
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        ///     Время последнего обновления в обращении
        /// </summary>
        public DateTime LastUpdate { get; set; }

        /// <summary>
        ///     Статус обращения
        /// </summary>
        public CaseInfoStatus State { get; set; }

        /// <summary>
        ///     Тип обращения
        /// </summary>
        public CaseTypeDto Type { get; set; }
    }

    /// <summary>
    ///     Документы обращения
    /// </summary>
    public class CaseDocumentsDto
    {
        /// <summary>
        ///     CRM идентификатор обращения
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Документы обращения, исключая документы, прикрепленные к сообщениям
        /// </summary>
        public DocShortInfo[] Documents { get; set; }
    }

    public class CaseMessageDto
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string CaseId { get; set; }
        public DateTime LastUpdate { get; set; }
        public string AuthorId { get; set; }
        public DocShortInfo[] Documents { get; set; }
    }

    public class MessageAuthorDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string AvatarId { get; set; }
    }

    public class DocShortInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DocumentType Type { get; set; }
    }
}