using System;
using System.Collections.Generic;
using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения о лицензиях, выданных ЮЛ
    /// </summary>
    public class EgrUlLicenseInfoDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// Наименование лицензируемого вида деятельности, на который выдана лицензия
        /// </summary>
        public List<string> LicenseTypeName { get; set; }

        /// <summary>
        /// Сведения об адресе места осуществления лицензируемого вида деятельности
        /// </summary>
        public List<string> PlaceLicenseActivity { get; set; }

        /// <summary>
        /// Наименование лицензирующего органа, выдавшего или переоформившего лицензию
        /// </summary>
        public string Licensor { get; set; }

        /// <summary>
        /// Сведения о приостановлении действия лицензии
        /// </summary>
        public EgrUlLicenseSuspensionInfoDto LicenseSuspension { get; set; }

        /// <summary>
        /// Серия лицензии
        /// </summary>
        public string LicenseSeries { get; set; }

        /// <summary>
        /// Номер лицензии
        /// </summary>
        public string LicenseNumber { get; set; }

        /// <summary>
        /// Вид лицензии
        /// </summary>
        public string LicenseType { get; set; }

        /// <summary>
        /// Дата лицензии
        /// </summary>
        public DateTime LicenseDate { get; set; }

        /// <summary>
        /// Дата начала действия лицензии
        /// </summary>
        public DateTime LicenseStartDate { get; set; }

        /// <summary>
        /// Дата окончания действия лицензии
        /// </summary>
        public DateTime LicenseEndDate { get; set; }

        /// <summary>
        /// Наличие даты окончания действия лицензии
        /// </summary>
        public bool LicenseEndDateSpecified { get; set; }
    }
}