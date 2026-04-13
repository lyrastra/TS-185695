namespace Moedelo.Edm.Dto.Documents
{
    public class EdmLinkedDocumentDto
    {
        /// <summary>
        /// ID документооборота в МД в раздепле ЭДО
        /// </summary>
        public int? WorkflowId { get; set; }

        /// <summary>
        /// ID основного документа в разделе документов
        /// </summary>
        public long DocumentBaseId { get; set; }
    }
}
