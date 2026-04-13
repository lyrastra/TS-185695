using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.ExternalPartner;

namespace Moedelo.CommonV2.Auth.Wsse.Domain.Models
{
    public class ExternalPartnerCredential
    {
        /// <summary>
        /// Идентификатор партнёра, которому выданы эти креды
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название партнёра
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Авторизационный секрет
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Доступные права
        /// </summary>
        public List<ExternalPartnerRule> Rules { get; set; }

        /// <summary>
        /// Алгоритм хэш-функции
        /// </summary>
        public SecureHashAlgorithms Algorithm { get; set; }

        /// <summary>
        /// Дата окончания действия кредов
        /// </summary>
        public DateTime? ExpiryDate { get; set; }
    }
}
