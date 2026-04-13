using System;

namespace Moedelo.SberbankCryptoEndpointV2.Dto.old.Upg2.ClientInfo
{
    public class ClientInfoAccountBlockInfoDto
    {
        /// <summary> Дата начала действия ограничения </summary>
        public DateTime? BeginDate { get; set; }
        /// <summary> Наибольшая разрешенная очередность платежей (от 1 до 5). Указывается, если есть блокировка по очередности. Пример значение 3 означает, что заблокированы очередности 4 - 6 </summary>
        public int? BlockedQueues { get; set; }
        /// <summary> Основание ареста </summary>
        public string Cause { get; set; }
        /// <summary> Дата снятия ограничения </summary>
        public DateTime? EndDate { get; set; }
        /// <summary> Наименование органа, наложившего арест </summary>
        public string Initiator { get; set; }
        /// <summary> Заблокированная (арестованная) сумма на счете </summary>
        public decimal? Sum { get; set; }
        /// <summary> Код налогового органа, наложившего арест </summary>
        public string TaxAuthorityCode { get; set; }
    }
}