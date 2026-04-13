using System;
using Moedelo.Common.Enums.Enums.Integration;

namespace Moedelo.BankIntegrationsV2.Dto.BankOperation
{
    public class RequestMovementListResponseDto
    {
        public RequestMovementListResponseDto() { }

        public RequestMovementListResponseDto(string msg)
        {
            Message = msg;
        }

        /// <summary> Заштрихованная часть телефонного номера для OTP </summary>
        public string PhoneMask { get; set; }

        public string RequestId { get; set; }

        public string Message { get; set; }

        public bool IsSuccess => RequestId != null && RequestId != Guid.Empty.ToString();

        public IntegrationResponseStatusCode Status { get; set; }
    }
}
