namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto
{
    public class ProfOutsourceContextDto
    {
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
    }
}