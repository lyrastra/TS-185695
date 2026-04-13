using System;

namespace Moedelo.AccountV2.Dto.UserFirmData
{
    public class UserFirmDataDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int FirmId { get; set; }

        public int LoginCount { get; set; }

        public DateTime DateOfLastLogin { get; set; }

        public int? RoleId { get; set; }

        public int? CreateUserId { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? ModifyUserId { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}