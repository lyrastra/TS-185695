using System;

namespace Moedelo.AccountV2.Dto.Account
{
    public class CompanyWithApiKeyDto
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public int? ApiKeyId { get; set; }

        public string ClientKey { get; set; }

        public int? AccountId { get; set; }

        public int? ApiUserId { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? CreateUserId { get; set; }

        public DateTime? RevokeDate { get; set; }

        public bool IsDisabled { get; set; }
    }
}