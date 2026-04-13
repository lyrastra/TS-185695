using System;

namespace Moedelo.Common.Enums.Enums.Email
{
    [Obsolete("Use Enums/Products/WLProductPartition")]
    public enum ProductPartition
    {
        /// <summary>Письма с обычным логотипом Моё дело</summary>
        DefaultBiz = 0,

        /// <summary>Письма с логотипмо Бюро </summary>
        Buro = 1,

        /// <summary>Письма для сбербанка с логотипом Моя бухгалтерия</summary>
        Sberbank = 2,

        /// <summary>Письма для СКБ банка с логотипом ДелоБанк</summary>
        SkbBank = 3
    }
}