namespace Moedelo.BPM.CaseDocs.Client.Dtos
{
    using System;

    /// <summary>
    ///     Обращение
    /// </summary>
    public class CaseInfoDto
    {
        /// <summary>
        ///     CRM идентификатор обращения
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Дата создания обращения
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
        ///     Время последнего сообщения в обращении
        /// </summary>
        public DateTime LastCaseUpdateDate { get; set; }

        /// <summary>
        ///     Статус обращения
        /// </summary>
        public CaseStateDto State { get; set; }

        /// <summary>
        ///     Тип обращения
        /// </summary>
        public CaseTypeDto Type { get; set; }
    }
}