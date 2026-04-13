using System;
using System.Collections.Generic;

namespace Moedelo.Money.Domain.Outsource;

public class AllOperationsApprovedRequest
{
    public IReadOnlyCollection<int> FirmIds { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    /// <summary>
    /// Учитывать только оплаченные операции
    /// </summary>
    public bool IsOnlyPaid { get; set; }
}
