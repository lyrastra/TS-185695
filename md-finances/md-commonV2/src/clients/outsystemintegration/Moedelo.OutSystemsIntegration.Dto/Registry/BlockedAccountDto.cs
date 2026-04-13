using System;

namespace Moedelo.OutSystemsIntegrationV2.Dto.Registry
{
    public class BlockedAccountDto
    {
        public int Number { get; set; }

        public DateTime Date { get; set; }

        public int Ifns { get; set; }

        public string Bik { get; set; }

        public string BankName { get; set; }
    }
}
