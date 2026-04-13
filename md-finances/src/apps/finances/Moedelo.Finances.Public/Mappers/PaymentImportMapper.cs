using Moedelo.Finances.Domain.Models.PaymentImport;
using Moedelo.Finances.Public.ClientData.PaymentImport;

namespace Moedelo.Finances.Public.Mappers.Setup
{
    public static class PaymentImportMapper
    {
        public static ImportStatusClientData MapToClient(this ImportStatus status)
        {
            return new ImportStatusClientData
            {
                FileId = status.FileId,
                ExData= status.ExData,
                Status = status.Status
            };
        }
    }
}