namespace Moedelo.AccountingV2.Dto.Requsites
{
    // full copy of Moedelo.KontragentsV2.Dto.KontragentSettlementAccountDto
    // assembly with that dto impl should be referenced to use Moedelo.AccountingV2.Dto.Payments.CreatePaymentDto for api call
    // using original version from KontragentsV2.Dto leads to wrong projects references in project solutions

    public class KontragentSettlementAccountDto
    {
        public long Id { get; set; }
        public int KontragentId { get; set; }
        public string Number { get; set; }
        public int? BankId { get; set; }
        public string NonResidentBankName { get; set; }
        public string Comment { get; set; }
        public bool IsActive { get; set; }
    }
}