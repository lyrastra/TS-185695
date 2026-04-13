using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Products;
using Moedelo.Common.Enums.Enums.Spam;

namespace Moedelo.SpamV2.Dto.SmsSender
{
    public class SmsSendRequestDto
    {
        public IEnumerable<SmsRequestDto> List { get; set; }

        public string SentFromAppModule { get; set; }

        public bool IsForTrial { get; set; }

        public WLProductPartition ProductPartition { get; set; }

        /// <summary> обычная строка или json индивидуальные параметры каждого WL </summary>
        public string WLSpecialParams { get; set; }

        /// <summary>
        /// Тип смс, в каких целях используется отправка СМС (не обязательное поле)
        /// </summary>
        public SmsType? SmsType { get; set; }
    }
}