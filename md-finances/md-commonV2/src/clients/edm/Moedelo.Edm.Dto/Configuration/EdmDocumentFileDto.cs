namespace Moedelo.Edm.Dto.Configuration
{
    /// <summary>
    /// Provider choice statement file
    /// </summary>
    public class EdmDocumentFileDto
    {
        public EdmDocumentFileDto()
        {
            Filename = "No file";
        }

        public EdmDocumentFileDto(string filename, byte[] data)
        {
            Filename = filename;
            Data = data;
        }

        public string Filename { get; set; }

        public byte[] Data { get; set; }
    }
}
