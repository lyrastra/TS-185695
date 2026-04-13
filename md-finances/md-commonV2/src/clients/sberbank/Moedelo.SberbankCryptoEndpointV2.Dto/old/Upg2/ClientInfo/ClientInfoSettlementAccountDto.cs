using System;
using System.Collections.Generic;

namespace Moedelo.SberbankCryptoEndpointV2.Dto.old.Upg2.ClientInfo
{
    public class ClientInfoSettlementAccountDto
    {
        public string Bic { get; set; }
        /// <summary> Приостановления операций по счету выше очередности (блокировки по очередности) </summary>
        public List<ClientInfoAccountBlockInfoDto> BlockedQueuesInfo { get; set; }
        /// <summary> Приостановления операций по счету выше очередности на сумму </summary>
        public List<ClientInfoAccountBlockInfoDto> BlockedSumQueuesInfo { get; set; }
        /// <summary> Содержит информацию о расчетных документах, ожидающих акцепта. Количество документов </summary>
        public int? CdiAcptDocQnt { get; set; }
        /// <summary> Содержит информацию о расчетных документах, ожидающих акцепта. Сумма документов </summary>
        public double? CdiAcptDocSum { get; set; }
        /// <summary> Содержит информацию о расчетных документах, помещенных в картотеку к счету 90902 (картотека 2). Количество документов </summary>
        public int? CdiCart2DocQnt { get; set; }
        /// <summary> Содержит информацию о расчетных документах, помещенных в картотеку к счету 90902 (картотека 2). Сумма документов </summary>
        public double? CdiCart2DocSum { get; set; }
        /// <summary> Содержит информацию о расчетных документах, ожидающих разрешения на проведение операции. Количество документов </summary>
        public int? CdiPermDocQnt { get; set; }
        /// <summary> Содержит информацию о расчетных документах, ожидающих разрешения на проведение операции. Сумма документов </summary>
        public double? CdiPermDocSum { get; set; }
        /// <summary> Дата закрытия счета </summary>
        public DateTime? CloseDate { get; set; }
        /// <summary> Номер счета (20 сиволов) </summary>
        public string Number { get; set; }
        /// <summary> Дата открытия счета </summary>
        public DateTime OpenDate { get; set; }
        /// <summary> Состояние счета = [OPEN, BLOCKED, CLOSED] </summary>
        public string State { get; set; }
    }
}