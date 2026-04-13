namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.SickList
{
    public static class DisabilityGroupTypeExtension
    {

        public static bool IsLossProfessionalAbilityToWork(this DisabilityGroupType? type)
        {
            if (type == null)
            {
                return false;
            }

            return type.Value == DisabilityGroupType.LossProfessionalAbilityToWork ||
                   type.Value == DisabilityGroupType.LossProfessionalAbilityToWorkV2;
        }
    }
}