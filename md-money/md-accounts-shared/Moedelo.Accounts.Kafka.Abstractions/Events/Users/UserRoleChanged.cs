namespace Moedelo.Accounts.Kafka.Abstractions.Events.Users
{
    public class UserRoleChanged
    {
        /// <summary>
        /// идентификатор пользователя
        /// </summary>
        public int UserId { get; set; }
        
        /// <summary>
        /// Id фирмы
        /// </summary>
        public int FirmId { get; set; }
    }
}