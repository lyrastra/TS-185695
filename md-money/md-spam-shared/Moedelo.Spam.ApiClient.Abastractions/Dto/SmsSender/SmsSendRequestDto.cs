using System.Collections.Generic;
using Moedelo.Spam.ApiClient.Abastractions.Enums.Common;

namespace Moedelo.Spam.ApiClient.Abastractions.Dto.SmsSender
{
    public class SmsSendRequestDto
    {
        public IEnumerable<SmsRequestDto> List { get; set; }

        public string SentFromAppModule { get; set; }

        public bool IsForTrial { get; set; }

        public WLProductPartition ProductPartition { get; set; }

        /// <summary> обычная строка или json индивидуальные параметры каждого WL </summary>
        public string WLSpecialParams { get; set; }
    }
}