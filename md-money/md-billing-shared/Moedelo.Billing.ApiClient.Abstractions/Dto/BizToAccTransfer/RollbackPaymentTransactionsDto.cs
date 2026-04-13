using System.Collections.Generic;

namespace Moedelo.Billing.Abstractions.Dto.BizToAccTransfer;

public class RollbackPaymentTransactionsDto
{
    /// <summary>
    /// Идентификатор исходной фирмы (БИЗ) в запросе переноса 
    /// </summary>
    public int FromFirmId { get; set; }

    /// <summary>
    /// Идентификатор целевой фирмы (УС) в запросе переноса
    /// </summary>
    public int ToFirmId { get; set; }

    /// <summary>
    /// Соответствия платежей: источник - новый 
    /// </summary>
    public List<TransferedPaymentMapDto> PaymentMaps { get; set; }
}