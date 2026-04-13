using System.Collections.Generic;

namespace Moedelo.Docs.ApiClient.Abstractions.NdsDeduction.Models
{
    public class SaveDeductionDto
    {
        public int Quarter { get; set; }
        
        public int Year { get; set; }
        
        /// <summary>
        /// Если true, вычет учитывает только текущий квартал. Иначе все вычеты документа
        /// </summary>
        public bool IsDeductionQuarterOnly { get; set; }

        public List<SaveDeductionItemDto> Items { get; set; }
    }
}