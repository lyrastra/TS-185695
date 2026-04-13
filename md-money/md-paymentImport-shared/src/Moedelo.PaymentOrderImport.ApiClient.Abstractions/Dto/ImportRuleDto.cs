using Moedelo.PaymentOrderImport.Enums;

namespace Moedelo.PaymentOrderImport.ApiClient.Abstractions.Dto
{
    public class ImportRuleDto
    {
        /// <summary>
        /// Идентификатор правила
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название правила
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Что меняет правило
        /// </summary>
        public RuleActionType ActionType { get; set; }
    }
}
