using System.Collections.Generic;

namespace Moedelo.CatalogV2.Dto.Price
{
    /// <summary> Параметр для отбора расширенных данных о прайс-листе </summary>
    public class GetPriceExtendedCriterionDto
    {
        /// <summary> Инициализирует новый параметр для отбора расширенных данных о прайс-листе </summary>
        public GetPriceExtendedCriterionDto()
        {
            OnlyGroups = new List<string>();
            OnlyPlatforms = new List<string>();
            OnlyMonthCounts = new List<int>();
            OnlyTariffs = new List<Common.Enums.Enums.Billing.Tariff>();
        }

        /// <summary> Только актуальные или все, если не указано </summary>
        public bool IsOnlyActual { get; set; }

        /// <summary> Только для ИП или все, если не указано </summary>
        public bool IsOnlyForIp { get; set; }

        /// <summary> Только для ООО или все, если не указано </summary>
        public bool IsOnlyForOoo { get; set; }

        /// <summary> Только доступные на покупку или все, если не указано </summary>
        public bool IsOnlyForBuy { get; set; }

        /// <summary> Только доступные на продление (и смену тарифа в выставвлении счетов) или все, если не указано </summary>
        public bool IsOnlyForProlongation { get; set; }

        /// <summary> Только доступные для партнеров или все, если не указано </summary>
        public bool IsOnlyForPartner { get; set; }

        /// <summary> Только доступные для добавления оплаты в биллинге, если не указано </summary>
        public bool IsOnlyForBilling { get; set; }

        /// <summary> Только доступные для партнерских регистраций или все, если не указано </summary>
        public bool IsOnlyForPartnerRegistration { get; set; }

        /// <summary> Только указанные группы или все, если не указано </summary>
        public List<string> OnlyGroups { get; set; }

        /// <summary> Только указанные платформы или все, если не указано </summary>
        public List<string> OnlyPlatforms { get; set; }

        /// <summary> Только указанные количества месяцев или все, если не указано </summary>
        public List<int> OnlyMonthCounts { get; set; }

        /// <summary> Только указанные тарифы или все, если не указано </summary>
        public List<Common.Enums.Enums.Billing.Tariff> OnlyTariffs { get; set; }
    }
}