using System.Collections.Generic;

namespace Moedelo.Billing.Abstractions.Dto.BizToAccTransfer.RevertTransfer.Context;

public class RevertTransferContextDto
{
    /// <summary>
    /// УС фирма, на которую ранее был осуществлён перевод
    /// </summary>
    public int AccFirmId { get; set; }

    /// <summary>
    /// БИЗ-фирма - старая фирма, с котороой был осуществлён перевод
    /// </summary>
    public int BizFirmId { get; set; }

    public IReadOnlyDictionary<int, int?> PaymentsMatches { get; set; }

    public IReadOnlyCollection<string> Comments { get; set; }

    public int[] AccFirmPaymentsIds { get; set; }
}