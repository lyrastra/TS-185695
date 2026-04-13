using System.Collections.Generic;
using Moedelo.Money.Api.Models.Registry;

namespace Moedelo.Money.Api.Models
{
    public class ApiPageResponseDto
    {
        public IReadOnlyCollection<OperationResponseDto> Data { get; set; }
        /// <summary>
        /// Количество пропущенных записей сначала
        /// </summary>
        public int Offset { get; set; }
        /// <summary>
        /// Количество возвращаемых записей
        /// </summary>
        public int Limit { get; set; }
        /// <summary>
        /// Общее количество записей
        /// </summary>
        public int TotalCount { get; set; }
    }
}
