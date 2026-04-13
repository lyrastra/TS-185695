namespace Moedelo.Edm.Dto.Documents
{
    public class SignDocumentsRequest
    {
        public SignDocumentsRequest()
        {
            
        }

        public SignDocumentsRequest(int workflowId, int firmId, int userId)
        {
            FirmId = firmId;
            UserId = userId;
            WorkflowId = workflowId;
        }

        public int FirmId { get; set; }
        public int UserId { get; set; }
        public int WorkflowId { get; set; }
    }
}
