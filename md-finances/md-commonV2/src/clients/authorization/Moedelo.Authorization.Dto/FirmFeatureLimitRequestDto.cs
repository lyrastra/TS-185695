using System.Collections.Generic;

namespace Moedelo.Authorization.Dto;

public class FirmFeatureLimitRequestDto
{
    public int FirmId { get; set; }
    public IReadOnlyCollection<FeatureLimitId> FeatureLimitIds { get; set; }
}

public class FirmFeatureLimitByCodesRequestDto
{
    public int FirmId { get; set; }
    public IReadOnlyCollection<string> FeatureLimitCodes { get; set; }
}