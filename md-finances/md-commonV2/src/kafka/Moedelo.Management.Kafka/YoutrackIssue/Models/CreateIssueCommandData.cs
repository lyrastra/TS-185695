using System.Collections.Generic;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;

namespace Moedelo.Management.Kafka.YoutrackIssue.Models
{
    public class CreateIssueCommandData : IEntityCommandData
    {
        public string Project { get; set; }
    
        public string Summary { get; set; }
    
        public IReadOnlyCollection<SimpleIssueCustomField> SimpleIssueCustomFields { get; set; }
    
        public IReadOnlyCollection<SingleEnumIssueCustomField> SingleEnumIssueCustomFields { get; set; }
    }
    
    public class SimpleIssueCustomField
    {
        public string Name { get; set; }
    
        public string Value { get; set; }
    }

    public class SingleEnumIssueCustomField
    {
        public string Name { get; set; }
    
        public string Value { get; set; }
    }
}