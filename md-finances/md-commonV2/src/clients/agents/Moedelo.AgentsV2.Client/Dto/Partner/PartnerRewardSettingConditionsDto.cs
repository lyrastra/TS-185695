using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Billing;

namespace Moedelo.AgentsV2.Client.Dto.Partner
{
    public class PartnerRewardSettingConditionsDto
    {
        // для кого настройка ИП или ООО
        public bool IsOoo { get; set; }
        //Пустой список означает доступность настройки для всех тарифов
        public List<Tariff> ListAvailableTariffs { get; set; }
    }
}