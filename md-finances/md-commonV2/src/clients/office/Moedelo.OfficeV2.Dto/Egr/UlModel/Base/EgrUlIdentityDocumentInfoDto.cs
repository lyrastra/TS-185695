using System;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Base
{
    /// <summary>
    /// Сведения о документе, удостоверяющем личность, внесенные в ЕГРЮЛ
    /// </summary>
    public class EgrUlIdentityDocumentInfoDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// Код вида документа по справочнику СПДУЛ
        /// </summary>
        public string CodeDocType { get; set; }

        /// <summary>
        /// Наименование вида документа
        /// </summary>
        public string NameDocType { get; set; }

        /// <summary>
        /// Серия и номер документа
        /// </summary>
        public string SeriesNumberDoc { get; set; }

        /// <summary>
        /// Дата выдачи
        /// </summary>
        public DateTime IssueDate { get; set; }

        /// <summary>
        /// Кем выдан
        /// </summary>
        public string IssueBy { get; set; }

        /// <summary>
        /// Кем выдан
        /// </summary>
        public string DivisionCode { get; set; }
    }
}
