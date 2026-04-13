using System;
using System.Collections.Generic;

namespace Moedelo.Billing.Abstractions.Dto;

public class ProductConfigurationRequestDto
{
    public int Duration { get; set; }

    public string Code { get; set; }

    public IReadOnlyDictionary<string, ModifierRequestDto> ModifiersValues { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}