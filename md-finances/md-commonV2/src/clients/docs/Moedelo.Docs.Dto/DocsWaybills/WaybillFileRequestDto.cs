namespace Moedelo.Docs.Dto.DocsWaybills
{
    public class WaybillFileRequestDto
    {
        /// <summary>
        /// DocumentBaseId накладной
        /// </summary>
        public long BaseId { get; set; }
        
        /// <summary>
        /// Добавить печать и подпись
        /// </summary>
        public bool? UseStampAndSign { get; set; }
        
        /// <summary>
        /// Добавить экземпляр исполнителя(по умолчанию false)
        /// </summary> 
        public bool? IncludeContractorCopy { get; set; }
        
        /// <summary>
        /// Добавить связанный счет-фактуру (по умолчанию false)
        /// </summary>
        public bool? IncludeLinkedInvoice { get; set; }
    }
}