namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment.Models;

public class BudgetaryKbkSaveDto
{
    /// <summary>
    /// Идентификатор КБК
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Номер КБК (104)
    /// </summary>
    public string Number { get; set; }
}