using Moedelo.Common.Enums.Enums.ElectronicReports;

namespace Moedelo.ErptV2.Dto.Eds
{
    public class RequiredDocument
    {
        public int Id { get; }
        public string Title { get; }
        public string FileName { get; }
        public string EntityId { get; }
        public RequiredDocumentType Type { get; }

        public RequiredDocument(
            int id,
            string title,
            string fileName,
            string entityId = null,
            RequiredDocumentType type = RequiredDocumentType.Unknown)
        {
            this.Id = id;
            this.Title = title;
            this.FileName = fileName;
            Type = type;
            this.EntityId = entityId;
        }
    }
}