namespace Moedelo.AccountingV2.Dto.PurseOperation
{
    /// <summary>
    /// Модель для сохранения операции по электронным деньгам
    /// </summary>
    public class PurseOperationDto
    {
        /// <summary>
        /// Идентификатор денежной операции
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор расчётного счёта
        /// </summary>
        public int? SettlementAccountId { get; set; }

        /// <summary>
        /// Личный кошелёк
        /// </summary>
        public int PurseId { get; set; }

        /// <summary>
        /// Сумма
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Дата оплаты
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Номер платежного поручения
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// Назначение платежа
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Показать предупреждение об отсутствии кассовых операций
        /// </summary>
        public bool ShowCashOperationAlert { get; set; }

        /// <summary>
        /// Провести дочерние операции
        /// </summary>
        public bool NeedProvideCashOperations { get; set; }
    }
}