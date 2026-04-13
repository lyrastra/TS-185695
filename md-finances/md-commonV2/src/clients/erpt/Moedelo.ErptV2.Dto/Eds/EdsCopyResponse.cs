namespace Moedelo.ErptV2.Dto.Eds
{
    /// <summary>Какие данные были скопированы/перемещены успешно</summary>
    public class EdsCopyResponse
    {
        public EdsOperationStatuses Copy { get; set; }
        /// <summary>Удалялась ли подпись с фирмы, с которой происходит копирование?</summary>
        public EdsOperationStatuses Clear { get; set; }
        public bool Success => Copy.Success && (Clear == null || Clear.Success);
    }
    
    public class EdsOperationStatuses
    {
        public bool Status { get; set; }
        public bool AllHistory { get; set; }
        public bool Phone { get; set; }
        public bool AdditionalFnsPfrFssFsgs { get; set; }
        public bool PfrEdm { get; set; }
        public bool Success => Status && AllHistory && Phone && AdditionalFnsPfrFssFsgs && PfrEdm;
    }
}