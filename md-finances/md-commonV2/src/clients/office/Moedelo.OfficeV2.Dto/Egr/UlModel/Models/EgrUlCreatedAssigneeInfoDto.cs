namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения о ЮЛ, которое было создано в форме слияния с участием правопреемника, или к которому присоединился правопреемник при реорганизации в форме выделения или разделения с одновременным присоединением или слиянием
    /// </summary>
    public class EgrUlCreatedAssigneeInfoDto
    {
        /// <summary>
        /// Основной государственный регистрационный номер юридического лица
        /// </summary>
        public string Ogrn { get; set; }

        /// <summary>
        /// ИНН юридического лица
        /// </summary>
        public string Inn { get; set; }

        /// <summary>
        /// Полное наименование юридического лица
        /// </summary>
        public string FullName { get; set; }
    }
}