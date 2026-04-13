namespace Moedelo.AccountV2.Dto.UserAccessControl
{
    public class ChangeAccessRequest
    {
        /// <summary>
        /// Пользователь, которому нужно установить роль
        /// </summary>
        public int UserId { get; set; }
        
        /// <summary>
        /// Компания, доступ к которой нужно установить
        /// </summary>
        public int FirmId { get; set; }
        
        /// <summary>
        /// Какую роль установить
        /// </summary>
        public int RoleId { get; set; }
    }
}