namespace Moedelo.AccountV2.Dto.UserAccessControl
{
    public class FirmUserDto
    {
        public int Id { get; set; }

        public string Fio { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }


        /// <summary>
        /// Профессиональный аутсорсер?
        /// </summary>
        public bool IsMainUser { get; set; }

        /// <summary>
        /// Владелец компании?
        /// </summary>
        public bool IsLegalUser { get; set; }

        /// <summary>
        /// Пользователь профессионального аутсорсера - администратор?
        /// </summary>
        public bool IsAdmin { get; set; }
    }
}