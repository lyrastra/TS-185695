using Moedelo.Money.Enums.PaymentOrders.Snapshot;

namespace Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Snapshot
{
    public class OrderDetailsDto
    {
        public string Name { get; set; }

        public string Inn { get; set; }

        public string Kpp { get; set; }

        /// <summary> Номер расчетного счета </summary>
        public string SettlementNumber { get; set; }

        /// <summary> Полное наименование банка </summary>
        public string BankName { get; set; }

        /// <summary> БИК банка </summary>
        public string BankBik { get; set; }

        /// <summary> Кор.счет банка </summary>
        public string BankCorrespondentAccount { get; set; }

        /// <summary> Город Банка </summary>
        public string BankCity { get; set; }

        /// <summary> Организация или индивидуальный предприниматель </summary>
        public bool IsOoo { get; set; }

        /// <summary> Тип плательщика </summary>
        public KontragentTypes? KontragentType { get; set; }

        /// <summary> Адрес для квитанции </summary>
        public string Address { get; set; }

        public string Okato { get; set; }

        public string Oktmo { get; set; }
    }
}
