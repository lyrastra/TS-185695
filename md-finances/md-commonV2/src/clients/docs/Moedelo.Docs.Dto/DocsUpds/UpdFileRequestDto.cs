namespace Moedelo.Docs.Dto.DocsUpds
{
    public class UpdFileRequestDto
    {
        /// <summary>
        /// DocumentBaseId накладной
        /// </summary>
        public long BaseId { get; set; }
        
        /// <summary>
        /// Добавить печать и подпись
        /// </summary>
        public bool? UseStampAndSign { get; set; }
    }
}