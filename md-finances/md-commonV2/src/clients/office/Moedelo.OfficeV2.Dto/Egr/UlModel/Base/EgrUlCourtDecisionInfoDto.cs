using System;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Base
{
    /// <summary>
    /// Решение суда
    /// </summary>
    public class EgrUlCourtDecisionInfoDto
    {
        /// <summary>
        /// Наименование суда
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date { get; set; }
    }
}
