using System;
using Moedelo.Common.Enums.Enums.EgrIp;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Base
{
    /// <summary>
    /// Сведения о рождении ФЛ, внесенные в ЕГРЮЛ
    /// </summary>
    public class EgrUlFlBirthInfoDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Место рождения
        /// </summary>
        public string BirthPlace { get; set; }

        /// <summary>
        /// Признак полноты представляемой даты рождения физического лица
        /// </summary>
        public EgrUlSignCompletenessBirthDate SignCompletenessBirthDate { get; set; }
    }
}