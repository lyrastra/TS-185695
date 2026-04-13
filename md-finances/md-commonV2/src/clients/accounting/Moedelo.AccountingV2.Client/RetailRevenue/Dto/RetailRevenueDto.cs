using System;
using System.Collections.Generic;

namespace Moedelo.AccountingV2.Client.RetailRevenue.Dto
{
    /// <summary>
    /// Розничкая выручка или БСО (Z-отчет)
    /// </summary>
    public class RetailRevenueDto
    {

        /// <summary>
        /// Числовой идентификатор 
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Id кассы
        /// </summary>
        public long? CashId { get; set; }

        /// <summary>
        /// Номер Z-отчета
        /// </summary>
        public string ZReportNumber { get; set; }

        /// <summary>
        /// Дата Z-отчета
        /// </summary>
        public DateTime DocDate { get; set; }

        /// <summary>
        /// Сумма документа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Оплачено картой (включено в общую сумму)
        /// </summary>
        public decimal? PayByCard { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Сумма дохода, учитываемая в УСН
        /// </summary>
        public decimal? UsnSum { get; set; }

        /// <summary>
        /// Сумма дохода по ЕНВД
        /// </summary>
        public decimal? EnvdSum { get; set; }

        /// <summary>
        /// в т. ч. НДС (только для пользователей на ОСНО)
        /// </summary>
        public Nds Nds { get; set; }

        /// <summary>
        /// Плательщик
        /// </summary>
        public int? PayerId { get; set; }

        /// <summary>
        /// Патенты (код(номер) патента -> сумма выручки по патенту)
        /// </summary>
        public List<ConsideredInPatentDto> ConsideredInPatents { get; set; }
    }
}