namespace Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.FifoSelfCost
{
    public class FifoSelfCostProductBalanceDto
    {
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// Стоимость всех поступлений за исключением "Приходов без документов", "Безвозмездных получений", "Розничных возвратов" и "Возвратов по отчётам комиссионера" на момент перехода со средней с/с
        /// </summary>
        public decimal IncomeSum { get; set; }

        /// <summary>
        /// Кол-во поступлений за исключением "Приходов без документов", "Безвозмездных получений", "Розничных возвратов" и "Возвратов по отчётам комиссионера" на момент перехода со средней с/с
        /// </summary>
        public decimal IncomeCount { get; set; }

        /// <summary>
        /// Кол-во всех "Приходов без документов" на момент перехода со средней с/с
        /// </summary>
        public decimal IncomeWithoutDocsCount { get; set; }

        /// <summary>
        /// Кол-во всех "Безвозмездных получений" на момент перехода со средней с/с
        /// </summary>
        public decimal DebitGratuitousCount { get; set; }

        /// <summary>
        /// Сумма всех "Безвозмездных получений" на момент перехода со средней с/с
        /// </summary>
        public decimal DebitGratuitousSum { get; set; }
        
        /// <summary>
        /// Себестоимость ВСЕХ списаний на момент перехода со средней с/с
        /// </summary>
        public decimal WriteOffSum { get; set; }

        /// <summary>
        /// Количество ВСЕХ списаний на момент перехода со средней с/с
        /// </summary>
        public decimal WriteOffCount { get; set; }

        /// <summary>
        /// Себестоимость списаний ПЕРЕМЕЩЕНИЕ СО СКЛАДА на момент перехода со средней с/с
        /// </summary>
        public decimal MovementWriteOffSum { get; set; }

        /// <summary>
        /// Себестоимость списаний В РАМКАХ КОМПЛЕКТАЦИИ на момент перехода со средней с/с
        /// </summary>
        public decimal BundlingWriteOffSum { get; set; }

        /// <summary>
        ///  Количество списаний В РАМКАХ КОМПЛЕКТАЦИИ на момент перехода со средней с/с
        /// </summary>
        public decimal BundlingWriteOffCount { get; set; }

        /// <summary>
        /// Кол-во всех Розничных возвратов + Возвратов по отчётам комиссионера на момент перехода со средней с/с
        /// </summary>
        public decimal RefundCount { get; set; }

        /// <summary>
        /// Сумма всех Розничных возвратов + Возвратов по отчётам комиссионера на момент перехода со средней с/с
        /// </summary>
        public decimal RefundSum { get; set; }
    }
}