using Moedelo.Infrastructure.AspNetCore.Validation;

namespace Moedelo.Money.Api.Models
{
    /// <summary>
    /// Установка значения "Резерва"
    /// </summary>
    public class SetReserveDto
    {
        /// <summary>
        /// Сумма резерва
        /// </summary>
        [PositiveNumber (AllowNull = true)]
        public decimal? ReserveSum { get; set; }
    }
}