using System;
using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения о приостановлении действия лицензии
    /// </summary>
    public class EgrUlLicenseSuspensionInfoDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// Дата приостановления действия лицензии
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Наименование лицензирующего органа, приостановившего действие лицензии
        /// </summary>
        public string Licensor { get; set; }
    }
}