using System.ComponentModel;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.ChildCare
{
    public enum ChildCareLivingPlaceType
    {
        [Description("В зоне с правом на отселение")]
        ResettlementRightZone = 1,
        [Description("В зоне с льготным социально - экономическим статусом")]
        PrivilegeStatusZone = 2,
        [Description("В зоне отселения")]
        ResettlementZone = 3,
        [Description("В населенных пунктах, подвергшихся радиоактивному загрязнению вследствие аварии в 1957 г. на ПО \"Маяк\"")]
        RadioactiveZone = 4
    }
}

