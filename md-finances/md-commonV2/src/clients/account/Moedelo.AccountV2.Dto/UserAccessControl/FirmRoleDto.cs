namespace Moedelo.AccountV2.Dto.UserAccessControl
{
    public class FirmRoleDto
    {
        public int UserId { get; set; }
        public string FirmName { get; set; }
        public int FirmId { get; set; }
        public bool IsLegal { get; set; }
        public string RoleName { get; set; }
        public int RoleId { get; set; }
        public string RoleCode { get; set; }
    }
}