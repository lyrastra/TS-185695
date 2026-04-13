using System;

namespace Moedelo.OutSystemsIntegrationV2.Dto
{
    public class CheckKontragentRequestDto
    {
        private string inn;

        private string kpp;

        private DateTime? transactionDate;

        public string Inn
        {
            get
            {
                return this.inn;
            }

            set
            {
                this.inn = string.IsNullOrWhiteSpace(value) ? null : value.Replace(" ", string.Empty);
            }
        }

        public string Kpp
        {
            get
            {
                return this.kpp;
            }

            set
            {
                this.kpp = string.IsNullOrWhiteSpace(value) ? null : value.Replace(" ", string.Empty);
            }
        }

        public DateTime TransactionDate
        {
            get
            {
                return this.transactionDate.GetValueOrDefault(DateTime.Now);
            }

            set
            {
                this.transactionDate = value;
            }
        }
    }
}