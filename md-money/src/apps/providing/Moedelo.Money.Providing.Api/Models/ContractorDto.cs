using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;

namespace Moedelo.Money.Providing.Api.Models
{
    public class ContractorDto
    {
        [IdIntValue]
        [RequiredValue]
        public int Id { get; set; }

        /// <summary>
        /// Название (ФИО)
        /// </summary>
        [RequiredValue]
        [ValidateXss]
        public string Name { get; set; }
    }
}
