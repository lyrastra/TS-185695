using Moedelo.Edm.Backend.Domain.Enums;

namespace Moedelo.Edm.Dto.Documents
{
    public sealed class EdsWizardSendingInfoResponse
    {
        public bool IsSuccess { get; }
        public int Id { get; }
        public string Name { get; }
        public DocumentStatus Status { get; }

        public EdsWizardSendingInfoResponse(bool isSuccess, int id, string name, DocumentStatus status)
        {
            IsSuccess = isSuccess;
            Id = id;
            Name = name;
            Status = status;
        }
    }
}
