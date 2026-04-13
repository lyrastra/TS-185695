using System.ComponentModel.DataAnnotations;

namespace Moedelo.Spam.ApiClient.Abastractions.Dto.Common
{
    public class BasePagedListRequest
    {
        private const int DefaultPagedLimit = 20;
        private const int MinPageRange = 0;
        private const int MaxPagedLimit = 10000;
        private const int MaxPagedOffset = int.MaxValue;

        /// <summary>
        /// Количество пропущенных записей сначала
        /// </summary>
        [Range(MinPageRange, MaxPagedOffset)]
        public int Offset { get; set; }

        /// <summary>
        /// Количество возвращаемых записей
        /// </summary>
        [Range(MinPageRange, MaxPagedLimit, ErrorMessage = "Превышено количество возвращаемых записей")]
        public int Limit { get; set; } = DefaultPagedLimit;
    }
}