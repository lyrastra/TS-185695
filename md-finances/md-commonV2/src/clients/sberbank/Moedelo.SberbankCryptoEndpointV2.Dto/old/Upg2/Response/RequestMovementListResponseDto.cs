using Moedelo.SberbankCryptoEndpointV2.Dto.old.Upg2.Response.PaymentDocuments;

namespace Moedelo.SberbankCryptoEndpointV2.Dto.old.Upg2.Response
{
    public class RequestMovementListResponseDto : ResponseSberbankCryptoDto
    {
        public MDMovementList MovementList { get; set; } = new MDMovementList();

        public string Message { get; set; }
    }
}
