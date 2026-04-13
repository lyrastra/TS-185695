namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.ChildCare
{
    public class ChildCareLivingConditionsExDto
    {
        public ChildCareLivingConditionsType? LivingConditionsType { get; set; }
        
        public ChildCareLivingPlaceType? LivingPlaceType { get; set; }
        
        public RadiationReasonType? LivingRadiationReasonType { get; set; }
    }
}