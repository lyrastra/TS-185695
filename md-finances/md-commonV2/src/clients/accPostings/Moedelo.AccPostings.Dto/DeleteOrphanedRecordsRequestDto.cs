using System;

namespace Moedelo.AccPostings.Dto
{
    public class DeleteOrphanedRecordsRequestDto
    {
        /// <summary>
        /// Проводки из периода с указанной датой начала (опционально)
        /// </summary>
        public DateTime? StartDate { get; set; }
    }
}