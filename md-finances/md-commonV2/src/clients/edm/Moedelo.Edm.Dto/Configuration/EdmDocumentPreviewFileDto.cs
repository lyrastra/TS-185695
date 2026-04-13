namespace Moedelo.Edm.Dto.Configuration
{
    /// <summary>
    /// Provider choice statement preview
    /// </summary>
    public class EdmDocumentPreviewFileDto
    {
        public EdmDocumentPreviewFileDto()
        {
        }

        public EdmDocumentPreviewFileDto(byte[] data)
        {
            Data = data;
        }

        /// <summary>
        /// JPEG preview
        /// </summary>
        public byte[] Data { get; set; }
    }
}
