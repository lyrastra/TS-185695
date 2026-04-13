using System;
using System.Collections.Generic;

namespace Moedelo.AccountV2.Dto.ProfOutsource
{
    public class ProfOutsourceDto
    {
        public int AccountId { get; set; }

        public string Login { get; set; }

        public string FullName { get; set; }

        public string OutsourceName { get; set; }

        public string Phone { get; set; }

        public int CreateUserId { get; set; }

        public string CreateUserLogin { get; set; }

        public DateTime CreateDate { get; set; }

        public ISet<ProfOutsourceTariffDto> Tariffs { get; set; }
    }
}
