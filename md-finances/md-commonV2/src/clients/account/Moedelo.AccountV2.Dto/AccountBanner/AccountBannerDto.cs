using System;

namespace Moedelo.AccountV2.Dto.AccountBanner
{
    public class AccountBannerDto
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public int? PublishFor { get; set; }
        public int CreateUserId { get; set; }
        public string CreateUserLogin { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
