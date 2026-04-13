using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Requisites;

namespace Moedelo.HomeV2.Client.Phone.Models
{
    public class PhoneSearchByFilterRequestDto
    {
        public IReadOnlyCollection<string> Phones { get; set; }

        public PhoneTypes Type { get; set; }

        public bool ExcludeDeletedFirms { get; set; }

        /// <summary>
        /// Исключает связанные фирмы, если главная и связанная фирмы имеют одинаковый номер телефон
        /// </summary>
        public bool ExcludeLinkedFirms { get; set; }

        public bool OrderByPhoneIdAsc { get; set; }

        public bool OrderByPhoneIdDesc { get; set; }

        /// <summary>
        /// Количество пропущенных записей сначала
        /// </summary>
        public int? Offset { get; set; }

        /// <summary>
        /// Количество возвращаемых записей
        /// </summary>
        public int Limit { get; set; }
    }
}