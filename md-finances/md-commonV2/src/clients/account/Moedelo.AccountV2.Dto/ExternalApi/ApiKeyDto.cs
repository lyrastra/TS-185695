using System;

namespace Moedelo.AccountV2.Dto.ExternalApi
{
    public class ApiKeyDto
    {
        public long Id { get; set; }

        public string ClientKey { get; set; }

        public int FirmId { get; set; }

        public int ApiUserId { get; set; }

        public int AccountId { get; set; }

        /// <summary>
        /// Дата создания ключа
        /// </summary>
        public DateTime CreateDate { get; set; }

        public int CreateUserId { get; set; }

        /// <summary>
        /// Дата, когда ключ был отозван
        /// </summary>
        public DateTime? RevokeDate { get; set; }

        /// <summary>
        /// Признак того, что ключ отключен
        /// </summary>
        public bool IsDisabled { get; set; }
    }
}