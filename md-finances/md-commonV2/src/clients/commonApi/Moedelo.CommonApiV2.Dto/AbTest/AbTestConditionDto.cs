using Moedelo.Common.Enums.Enums.AbTest;

namespace Moedelo.CommonApiV2.Dto.AbTest;

public class AbTestConditionDto
{
    public AbTestCriterionType CriterionType { get; set; }

    public string CriterionValue { get; set; }
}