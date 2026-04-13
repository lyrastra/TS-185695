namespace Moedelo.AccountingV2.Dto.Bills
{
    /// <summary>
    /// Модель счета, используемая в импорте денег
    /// </summary>
    public class SalesBillFullDto : SalesBillCollectionItemDto
    {
        /// <summary>
        /// BaseId документа (при этом в Id передается идентификатор сущности Bill)
        /// </summary>
        public long DocumentBaseId { get; set; }
    }
}