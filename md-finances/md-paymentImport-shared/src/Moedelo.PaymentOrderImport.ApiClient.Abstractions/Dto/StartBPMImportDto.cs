
namespace Moedelo.PaymentOrderImport.ApiClient.Abstractions.Dto
{
    public class StartBPMImportDto
    {
        public int FirmId { get; set; }
        public byte[] File { get; set; }
    }
}
