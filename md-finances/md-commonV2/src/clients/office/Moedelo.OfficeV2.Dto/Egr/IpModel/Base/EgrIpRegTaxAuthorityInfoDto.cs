using System;

namespace Moedelo.OfficeV2.Dto.Egr.IpModel.Base
{
    /// <summary>
    /// Сведения о регистрирующем органе
    /// </summary>
    public class EgrIpRegTaxAuthorityInfoDto
    {
        /// <summary>
        /// Код вида документа по справочнику СПДУЛ
        /// </summary>
        public string CodeDocType { get; set; }

        /// <summary>
        /// Наименование документа по справочнику СПДУЛ
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

        public bool IssueDateSpecified { get; set; }

        /// <summary>
        /// Кем выдан документ
        /// </summary>
        public string IssueBy { get; set; }

        /// <summary>
        /// Код подразделения, выдавшего документ
        /// </summary>
        public string DivisionCode { get; set; }
    }
}
