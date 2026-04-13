using System;

namespace Moedelo.SberbankCryptoEndpointV2.Dto.old.Upg2.Request
{
    public class RequestMovementListRequestDto
    {
        public RequestMovementListRequestDto(
            DateTime beginDate, 
            DateTime endDate, 
            string bik, 
            string settlementNumber, 
            int firmId
        )
        {
            BeginDate = beginDate;
            EndDate = endDate;
            Bik = bik;
            SettlementNumber = settlementNumber;
            FirmId = firmId;
        }

        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Bik { get; set; }
        public string SettlementNumber { get; set; }
        public int FirmId { get; set; }
    }
}