namespace Moedelo.BPM.Registry.Documents.Client.Models
{
    public class RegistryDto
    {
        public int Id { get; set; }

        public int FirmId { get; set; }

        public string CreationDate { get; set; }

        public SourceTypeDto SourceType { get; set; }
        
        public bool Completed { get; set; }

        public string Contractor { get; set; }

        public string DocumentTypeName { get; set; }
        
        public OperationTypeDto OperationType { get; set; }
        
        public string OperationDate { get; set; }
        
        public decimal? OperationSum { get; set; }

        public decimal? SumByRelatedDocuments { get; set; }

        public decimal? FullOperationSum { get; set; }
        
        public string Description { get; set; }
    }
}