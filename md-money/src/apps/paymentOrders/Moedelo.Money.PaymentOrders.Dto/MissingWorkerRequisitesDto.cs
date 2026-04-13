namespace Moedelo.Money.PaymentOrders.Dto;

public class MissingWorkerRequisitesDto
{
    /// <summary> ФИО </summary>
    public string Name { get; set; }

    /// <summary> ИНН </summary>
    public string Inn { get; set; }

    /// <summary> Номер расчетного счета </summary>
    public string SettlementNumber { get; set; }
}