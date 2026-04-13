using System.ComponentModel;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.ChildCare
{
    public enum ChildCareAdoptionDecisionType
    {
        [Description("Опека")]
        Guardianship = 1,
        [Description("Усыновление")]
        Adoption = 2
    }
}