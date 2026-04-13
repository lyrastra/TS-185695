using System.IO;

namespace Moedelo.Money.Import.Domain.Models.PurseOperation
{
    public class PurseOperationImportRequest
    {
        /// <summary>
        /// id контрагента платежной системы
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// Файл-источник
        /// </summary>
        public Stream File { get; set; }
        
        /// <summary>
        /// Текущий год (для тестов)
        /// </summary>
        public int? currentYear { get; set; }
    }
}
