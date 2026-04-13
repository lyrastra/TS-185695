using System.Collections.Generic;
using Moedelo.Payroll.Shared.Enums.Charge;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData;

public class ChargeTypeDto
{
    public int Id { get; set; }
    public ChargeParentTypeDto ParentType { get; set; }
    public bool IsApplyNorthRise { get; set; }
    public bool IsApplyRegionalRate { get; set; }
    public bool HasNdfl { get; set; }
    public decimal? NdflRate { get; set; }
    public int NdflCode { get; set; }
    public ChargeType Type { get; set; }
    public IReadOnlyCollection<ChargeTypeSettingDto> Settings { get; set; }
}