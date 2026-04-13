using System.Collections.Generic;
using Moedelo.Accounting.Enums.SalaryPayments;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.SalaryPayments
{
    public class CashOrderDto
    {
        /// <summary>
        /// Список сотрудников с суммами выплат
        /// </summary>
        public IReadOnlyList<WorkerInPaybillDto> Workers { get; set; } = new List<WorkerInPaybillDto>();

        /// <summary> Номер платежа </summary>
        public string Number { get; set; }

        /// <summary> Дата платежа </summary>
        public string Date { get; set; }

        /// <summary>
        /// Номер ведомости
        /// </summary>
        public string PaybillNumber { get; set; }

        /// <summary>
        /// Дата ведомости
        /// </summary>
        public string PaybillDate { get; set; }

        /// <summary> Сумма платежа </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Тип выплаты сотруднику
        /// </summary>
        public WorkerPaymentType WorkerType { get; set; }

        /// <summary>
        /// Описание платежа (назначение)
        /// </summary>
        public string Destination { get; set; }

        public bool IsPaybill
        {
            get { return !string.IsNullOrEmpty(PaybillDate) && !string.IsNullOrEmpty(PaybillNumber); }
        }
    }
}