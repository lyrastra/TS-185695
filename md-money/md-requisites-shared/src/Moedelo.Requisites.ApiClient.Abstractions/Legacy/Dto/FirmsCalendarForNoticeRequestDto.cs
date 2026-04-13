using System;
using System.Collections.Generic;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

public class FirmsCalendarForNoticeRequestDto
{
    public IReadOnlyCollection<int> FirmIds { get; set; }

    public bool IsSmsNotice { get; set; }

    public string OnDate { get; set; }
    
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    public DateTime[]? EndDates { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
}