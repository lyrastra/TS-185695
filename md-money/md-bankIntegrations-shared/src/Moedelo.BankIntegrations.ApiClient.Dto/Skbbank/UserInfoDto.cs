using Moedelo.BankIntegrations.Enums.Skb;
using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.Skbbank
{
    public class UserInfoDto
    {
        /// <summary> id договора в ДБО </summary>
        public string Id { get; set; }

        /// <summary> Признак активной услуги </summary>
        public bool ServActive { get; set; }

        /// <summary> id услуги </summary>
        public string ServId { get; set; }

        public string Inn { get; set; }

        public string Kpp { get; set; }

        /// <summary> Система налогообложения  </summary>
        public List<string> FirmTaxation { get; set; }

        /// <summary> ФИО руководителя  </summary>
        public string FioHead { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public ClientTariffs Tariff { get; set; }

        /// <summary> Дата активации услуги </summary>
        public string ActivateDate { get; set; }

        /// <summary> Дата окончания услуги </summary>
        public string EndDate { get; set; }

        /// <summary> Признак бесплатного обслуживания </summary>
        public bool TariffFree { get; set; }

        /// <summary> Актуальный список р/сч </summary>
        public List<SkbAccountDto> Accounts { get; set; }

        public TokenDto Token { get; set; }

    }
}
