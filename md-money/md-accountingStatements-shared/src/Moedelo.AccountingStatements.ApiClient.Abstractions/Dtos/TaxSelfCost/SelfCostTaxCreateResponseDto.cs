namespace Moedelo.AccountingStatements.ApiClient.Abstractions.Dtos.SelfCostTax
{
    public class SelfCostTaxCreateResponseDto
    {
        /// <summary>
        /// Базовый идентификатор
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Номер документа (автоматическая нумерация при создании)
        /// </summary>
        public string Number { get; set; }
    }
}
