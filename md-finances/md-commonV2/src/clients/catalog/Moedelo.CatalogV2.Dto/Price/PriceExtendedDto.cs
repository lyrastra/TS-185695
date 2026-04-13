using System;

namespace Moedelo.CatalogV2.Dto.Price
{
    public class PriceExtendedDto
    {
        /// <summary> Идентификатор прайс-листа </summary>
        public int Id { get; set; }

        /// <summary> Название прайс-листа </summary>
        public string Name { get; set; }

        /// <summary> Количество месяцев </summary>
        public int MonthCount { get; set; }

        /// <summary> Стоимость </summary>
        public decimal Price { get; set; }

        /// <summary> Доля партнера из стоимости </summary>
        public decimal PricePartner { get; set; }

        /// <summary> Группа </summary>
        public string Group { get; set; }

        /// <summary> Платформа </summary>
        public string Platform { get; set; }

        /// <summary> Тариф из перечисления </summary>
        public Common.Enums.Enums.Billing.Tariff Tariff { get; set; }

        /// <summary> Название тарифа </summary>
        public string TariffName { get; set; }

        /// <summary> Актуальный </summary>
        public bool IsActual { get; set; }

        /// <summary> Для ИП </summary>
        public bool IsForIp { get; set; }

        /// <summary> Для ООО </summary>
        public bool IsForOoo { get; set; }

        /// <summary> Доступный на покупку </summary>
        public bool IsForBuy { get; set; }

        /// <summary> Доступный на продление (и смену тарифа в выставвлении счетов) </summary>
        public bool IsForProlongation { get; set; }

        /// <summary> Доступный для партнеров </summary>
        public bool IsForPartner { get; set; }

        /// <summary> Доступный для добавления оплаты в биллинге </summary>
        public bool IsForBilling { get; set; }

        /// <summary> Доступный для партнерских регистраций </summary>
        public bool IsForPartnerRegistration { get; set; }
    }
}