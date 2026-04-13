namespace Moedelo.Docs.Dto.SalesUpd
{
    public class SalesUpdRequestDto
    {
        /// <summary>
        /// Смещение начальной позиции чтения в страницах
        /// </summary>
        public uint PageNumber { get; set; }
        
        /// <summary>
        /// Размер страницы в позициях
        /// </summary>
        public uint PageSize { get; set; }
    }
}