using System;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class UpdateAccountTaxDataCommand
    {
        public int FirmId { get; set; }

        /// <summary>
        /// Дата регистрации в ФНС
        /// </summary>
        public DateTime? FnsRegistrationDate { get; set; }

        /// <summary>
        /// Дата прекращения деятельности
        /// </summary>
        public DateTime? TerminationDate { get; set; }

        /// <summary>
        /// Основной ОКВЭД
        /// </summary>
        public string MainOkvedCode { get; set; }

        /// <summary>
        /// Расшифровка основного ОКВЭД
        /// </summary>
        public string MainOkvedName { get; set; }

        /// <summary>
        /// Выручка (продажи)
        /// </summary>
        public decimal? Revenue { get; set; }

        /// <summary>
        /// Чистая прибыль (убыток)
        /// </summary>
        public decimal? NetProfit { get; set; }

        //Тип организации (ИП/ООО)
        public string OrganizationType { get; set; }

        /// <summary>
        /// Количество сотрудников
        /// </summary>
        public int? EmployeeCount { get; set; }
    }
}