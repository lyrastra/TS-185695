using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения о должности ФЛ
    /// </summary>
    public class EgrUlFlPositionInfoDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// Основной государственный регистрационный номер индивидуального предпринимателя - управляющего юридическим лицом
        /// </summary>
        public string OgrnIp { get; set; }

        /// <summary>
        /// Вид должностного лица по справочнику СКФЛЮЛ (указывается код по справочнику)
        /// </summary>
        public string ExecutiveType { get; set; }

        /// <summary>
        /// Наименование вида должностного лица по справочнику СКФЛЮЛ
        /// </summary>
        public string ExecutiveTypeName { get; set; }

        /// <summary>
        /// Наименование должности
        /// </summary>
        public string PositionName { get; set; }
    }
}