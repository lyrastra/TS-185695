using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.KudirOsno
{
    /// <summary>
    /// Данные ЗП для КУДИР ИП-ОСНО 
    /// </summary>
    public class KudirIpOsnoDataDto
    {
        /// <summary>
        /// По сотрудникам и НДФЛ
        /// </summary>
        public List<IncomeAndNdflRowDto> IncomeAndNdflRows { get; set; }

        /// <summary>
        /// По фондам
        /// </summary>
        public List<FundRowDto> FundRows { get; set; }
    }
}