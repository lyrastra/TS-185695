using System;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Base
{
    public class EgrUlGrnDateInfoDto
    {
        /// <summary>
        /// Государственный регистрационный номер записи ЕГРЮЛ
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата внесения записи в ЕГРЮЛ
        /// </summary>
        public DateTime Date { get; set; }
    }
}
