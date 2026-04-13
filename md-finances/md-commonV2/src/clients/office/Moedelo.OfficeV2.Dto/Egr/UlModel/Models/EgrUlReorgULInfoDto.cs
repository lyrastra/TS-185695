using Moedelo.Common.Enums.Enums.EgrIp;
using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения о юридических лицах, участвующих  в реорганизации
    /// </summary>
    public class EgrUlReorgUlInfoDto
    {
        /// <summary>
        /// ГРН и дата внесения в ЕГРЮЛ записи об исправлении технической ошибки в указанных сведениях
        /// </summary>
        public EgrUlGrnDateInfoDto Grn { get; set; }
        
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
        public string UlFullName { get; set; }

        /// <summary>
        /// Состояние юридического лица после завершения реорганизации
        /// </summary>
        public EgrUlStatuAfterReorg UlStatuAfterReorg { get; set; }

        /// <summary>
        /// Наличие ULStatuAfterReorg
        /// </summary>
        public bool UlStatuAfterReorgSpecified { get; set; }
    }
}