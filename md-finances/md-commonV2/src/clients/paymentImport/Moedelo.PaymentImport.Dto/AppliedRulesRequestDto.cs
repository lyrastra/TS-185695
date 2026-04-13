using System.Collections.Generic;

namespace Moedelo.PaymentImport.Dto
{
    public class AppliedRulesRequestDto
    {
        /// <summary>
        /// Идентификаторы денежных операций
        /// </summary>
        public IReadOnlyList<long> DocumentBaseIds { get; set; }

        /// <summary>
        /// Исключить поиск применённого правила для операций в "жёлтой" таблице
        /// </summary>
        public bool ExcludeOutsourceProcessing { get; set; }
    }
}