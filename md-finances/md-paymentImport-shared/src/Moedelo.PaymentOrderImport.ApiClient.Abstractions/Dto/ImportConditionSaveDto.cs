using Moedelo.PaymentOrderImport.ApiClient.Abstractions.Enums;

namespace Moedelo.PaymentOrderImport.ApiClient.Abstractions.Dto
{
    public class ImportConditionSaveDto
    {
        /// <summary>
        /// Оператор для проверки условия
        /// </summary>
        public RuleConditionOperator Operator { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        public string Value { get; set; }
        
        /// <summary>	
        /// Имя котнрагента	
        /// </summary>
        public string ContractorName { get; set; }
    }
}