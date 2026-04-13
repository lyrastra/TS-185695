using System;
using System.Collections.Generic;
using System.Text;
using Moedelo.PaymentOrderImport.ApiClient.Abstractions.Enums;

namespace Moedelo.PaymentOrderImport.ApiClient.Abstractions.Dto
{
    public class ImportFromUserDto
    {
        public string FileId { get; set; }

        public bool CheckDocuments { get; set; }

        public bool CheckSettlementAccount { get; set; }

        /// <summary>
        /// Второй расчетный счет, для создания валютного счета
        /// </summary> 
        public string SecondSettlementAccount { get; set; }

        /// <summary>
        /// Тип счета, указанного в выписке
        /// </summary>
        public SettlementAccountType SettlementAccountType { get; set; }
    }
}
