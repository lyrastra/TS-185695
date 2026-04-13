using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.PaymentOrders.Incoming.RefundFromAccountablePerson;

public class RefundFromAccountablePersonImportRequest
{
    public DateTime Date { get; set; }

    public string Number { get; set; }

    public decimal Sum { get; set; }

    public string Description { get; set; }

    public int SettlementAccountId { get; set; }

    public int? EmployeeId { get; set; }

    public string EmployeeName { get; set; }

    public long? DuplicateId { get; set; }

    public OperationState OperationState { get; set; }

    public string SourceFileId { get; set; }

    public int ImportId { get; set; }

    public int[] ImportRuleIds { get; set; }

    /// <summary>
    /// Реквизиты контрагента, если не удалось подставить конкретного сотрудника
    /// </summary>
    public MissingWorkerRequisitesSaveRequest MissingWorkerRequisites { get; set; }

    public int? ImportLogId { get; set; }

    public OutsourceState? OutsourceState { get; set; }
}