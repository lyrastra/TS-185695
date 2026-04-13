namespace Moedelo.Billing.Abstractions.Legacy.Dto;

/// <summary> Дополнительные данные, используемые при отправке счета </summary>
public class PaymentHistoryExBillDataDto
{
    /// <summary> Идентификатор платежа </summary>
    public int PaymentId { get; set; }

    /// <summary> Плательщик </summary>
    public string Payer { get; set; }

    /// <summary> ФИО получателя </summary>
    public string ToFio { get; set; }

    /// <summary> Дополнительный Email для отправки письма </summary>
    public string AdditionalSendBillEmail { get; set; }

    /// <summary> Дата действия счета </summary>
    public string BillExpirationDate { get; set; }

    /// <summary> Примечание внутри счета </summary>
    public string Note { get; set; }

    /// <summary> Сопроводительное сообщение в письме </summary>
    public string CoveringMessage { get; set; }

    /// <summary> Физическое лицо или нет </summary>
    public bool? IsPhysicalPerson { get; set; }

    /// <summary>
    /// Реквизиты получателя платежа по счёту
    /// </summary>
    public BillPayeeRequisites PayeeRequisites { get; set; }

    public class BillPayeeRequisites
    {
        public int RegionalPartnerInfoId { get; set; }

        public string FirmName { get; set; }

        public string Inn { get; set; }

        public string Kpp { get; set; }

        public string Ogrn { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string PhoneFederal { get; set; }

        public string SettlementAccount { get; set; }

        public string BankName { get; set; }

        public string BankBik { get; set; }

        public string BankAccount { get; set; }

        public string Director { get; set; }

        public bool IsWorkingWithNds { get; set; }
    }
}