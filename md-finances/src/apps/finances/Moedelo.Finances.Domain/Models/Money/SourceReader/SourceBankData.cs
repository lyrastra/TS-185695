using Moedelo.Common.Enums.Enums.Catalog;

namespace Moedelo.Finances.Domain.Models.Money.SourceReader
{
    public class SourceBankData
    {
        public string FullName { get; set; }
        public string Bik { get; set; }
        public bool IsActive { get; set; }
        public BankRegistrationNumber RegistrationNumber { get; set; }

        public string IconUrl { get; set; }
    }
}
