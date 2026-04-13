using System;

namespace Moedelo.AccountingV2.Dto.MoneyStatistics
{
    public class ExecuteByFirmIdsRequestDto
    {
        /// <summary>
        /// Список фирм, по которым будет произведен расчет
        /// </summary>
        public int[] FirmIds { get; set; } = [];
        
        /// <summary>
        /// Дата, на которую выполняется расчет/проверка баланса (дата «среза») и так же из данной даты берется год.
        /// Если не задана, используется текущая дата.
        /// </summary>
        public DateTime CalcDate { get; set; } = DateTime.Today;
    }
}