using System.Collections.Generic;

namespace Moedelo.Edm.Dto.BizToAccTransfer
{
    public class EdmInviteTransferDto
    {
        public int FromFirmId { get; set; }
        public int FromUserId { get; set; }

        public int ToFirmId { get; set; }
        public int ToUserId { get; set; }

        /// <summary>
        /// Словарь соответствий Контрагентов { Source_KontragentId (источник): Target_KontragentId (созданный в новом кабинете) }
        /// </summary>
        public Dictionary<int,int> KontragentsMapping { get; set; }

        /// <summary>
        /// Словарь соответствий Актов из Покупок { Source_DocumentBaseId (источник): Target_DocumentBaseId (созданный в новом кабинете) }
        /// </summary>
        public Dictionary<long, long> PurchasesStatements { get; set; }
    }
}
