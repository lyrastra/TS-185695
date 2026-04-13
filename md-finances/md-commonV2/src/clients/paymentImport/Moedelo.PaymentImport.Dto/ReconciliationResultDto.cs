using System;
using System.Collections.Generic;

namespace Moedelo.PaymentImport.Dto
{
    public class ReconciliationResultDto
    {
        public Guid SessionId { get; set; }
        
        public int FirmId { get; set; }
        
        /// <summary> Есть в сервисе, нет в выписке </summary>
        public List<ReconciliationOperationDto> ExcessOperations { get; set; }

        /// <summary> Есть в выписке, нет в сервисе </summary>
        public List<ReconciliationOperationDto> MissingOperations { get; set; }
    }
}