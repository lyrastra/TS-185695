using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Mailing;

public class SetWorkerMailingsDto
{
    public IReadOnlyCollection<int> MailingIds { get; set; }

    public int WorkerId { get; set; }

    public string Email { get; set; }

    public string FullName { get; set; }

    public string PositionName { get; set; }
}