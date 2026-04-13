using System;

namespace Moedelo.Estate.Client.FixedAsetInvestments.Dto
{
    public class FixedAssetInvestmentBalanceSaveDto
    {
        /// <summary>
        /// id субконто (для связи остатков и ВА)
        /// </summary>
        public long SubcontoId { get; set; }

        /// <summary>
        /// Наименивание ВА
        /// </summary>
        public string Name { get; set; }

        /// <summary> 
        /// Дата ввода остатков
        /// </summary>
        public DateTime BalanceDate { get; set; }

        /// <summary>
        /// Сумма вложения
        /// </summary>
        public decimal BalanceSum { get; set; }
    }
}