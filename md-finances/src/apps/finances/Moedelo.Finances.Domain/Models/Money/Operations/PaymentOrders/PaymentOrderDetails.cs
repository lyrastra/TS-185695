using Moedelo.Finances.Domain.Enums.Money.Operations.PaymentOrders;

namespace Moedelo.Finances.Domain.Models.Money.Operations.PaymentOrders
{
    public class PaymentOrderDetails
    {
        /// <summary> Имя  </summary>
        public string Name { get; set; }

        /// <summary> ИНН </summary>
        public string Inn { get; set; }

        /// <summary> КПП </summary>
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
        public PaymentOrderKontragentType? KontragentType { get; set; }

        /// <summary> Адрес для квитанции </summary>
        public string Address { get; set; }

        /// <summary> ОКАТО </summary>
        public string Okato { get; set; }

        /// <summary> Оkmto </summary>
        public string Oktmo { get; set; }

        public PaymentOrderDetails()
        {
            Name = string.Empty;
            BankCorrespondentAccount = string.Empty;
            Address = string.Empty;
            Okato = string.Empty;
            BankBik = string.Empty;
            BankName = string.Empty;
            Inn = string.Empty;
            Kpp = string.Empty;
            SettlementNumber = string.Empty;
        }
    }
}
