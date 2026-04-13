using System;

namespace Moedelo.CommonApiV2.Dto.AbTest;

public class AbTestVariantDto
{
    public int Id { get; set; }

    public int AbTestId { get; set; }

    public string PageUrl { get; set; }

    public string Description { get; set; }

    public string Name { get; set; }

    public int PercentChance { get; set; }
}