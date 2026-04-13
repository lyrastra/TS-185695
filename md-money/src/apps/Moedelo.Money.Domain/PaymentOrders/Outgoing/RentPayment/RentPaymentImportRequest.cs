using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.RentPayment;

public class RentPaymentImportRequest
{
    public DateTime Date { get; set; }

    public string Number { get; set; }

    public decimal Sum { get; set; }

    public string Description { get; set; }

    public int SettlementAccountId { get; set; }

    public KontragentWithRequisites Kontragent { get; set; }

    public long? ContractBaseId { get; set; }

    public long? DuplicateId { get; set; }

    public OperationState OperationState { get; set; }

    public string SourceFileId { get; set; }

    public int ImportId { get; set; }

    public int[] ImportRuleIds { get; set; }

    public int? ImportLogId { get; set; }

    /// <summary>
    /// Признак: на обработке в Ауте ("жёлтая таблица")
    /// </summary>
    public OutsourceState? OutsourceState { get; set; }
}