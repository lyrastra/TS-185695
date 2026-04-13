namespace Moedelo.AccountV2.Dto.ProfOutsource
{
    public class ProfOutsourceContextDto
    {
        public int FirmId { get; set; }
        public int UserId { get; set; }
        /// <summary>
        /// Находится ли фирма под управлением профессионального аутсорсера
        /// </summary>
        public bool IsFirmOnService { get; set; }

        /// <summary>
        /// Является ли пользователь профессиональным аутсорсером
        /// </summary>
        public bool IsUserOutsourcer { get; set; }

        /// <summary>
        /// Имя ПО акаунта, у которого фирма находится на обслуживании
        /// </summary>
        public string OutsourceAccountName { get; set; }

        /// <summary>
        /// Идентификатор ПО акаунта, у которого фирма находится на обслуживании
        /// </summary>
        public int? OutsourceAccountId { get; set; }
    }
}