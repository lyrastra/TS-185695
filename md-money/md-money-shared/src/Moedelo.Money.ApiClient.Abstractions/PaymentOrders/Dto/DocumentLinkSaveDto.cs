namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;

public class DocumentLinkSaveDto
{
    /// <summary>
    /// Идентификатор первичного документа
    /// </summary>
    public long DocumentBaseId { get; set; }

    /// <summary>
    /// Учитываемая сумма
    /// </summary>
    public decimal Sum { get; set; }
}