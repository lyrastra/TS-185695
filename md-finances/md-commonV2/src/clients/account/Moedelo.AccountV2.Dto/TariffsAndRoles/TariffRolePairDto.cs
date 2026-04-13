using System;
using System.Collections.Generic;

namespace Moedelo.AccountV2.Dto.TariffsAndRoles
{
    public class TariffRolePairDto
    {
        public int TariffId { get; set; }
       
        /// <summary>
        /// Тарифы с доп. услугами
        /// </summary>
        public List<int> AdditionalTariffIds { get; set; }
        
        public int RoleId { get; set; }
        
        /// <summary>
        /// Самая ранняя дата из дат окончания тарифов (для инвалидации кэша)
        /// </summary>
        public DateTime ExpiryDate { get; set; }
    }
}