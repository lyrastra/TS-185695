namespace Moedelo.AccountingV2.Dto.Cashier
{
    public class CashierDto
    {
        /// <summary>
        /// Id кассы
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Полное название кассы
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Является ли касса основной
        /// </summary>
        public bool IsMain { get; set; }

        /// <summary>
        /// Id субконто
        /// </summary>
        public long? SubcontoId { get; set; }
    }
}