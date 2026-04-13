using System;

namespace Moedelo.AccountV2.Dto.FirmOnSeivice
{
    public class FirmOnServiceDto
    {
        public long Id { get; set; }

        public int AccountId { get; set; }

        public int FirmId { get; set; }

        public DateTime CreateDate { get; set; }

        public int CreateUserId { get; set; }

        public string ServiceGroup { get; set; }
    }
}