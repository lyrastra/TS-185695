using System;

namespace Moedelo.HomeV2.Dto.PromoCode
{
    public class MrkActionsDto
    {
        public int Id { get; set; }

        /// <summary> Название акции </summary>
        public string Name { get; set; }

        /// <summary> Описание акции </summary>
        public string Description { get; set; }

        /// <summary> Дата начала действия </summary>
        public DateTime DateStart { get; set; }

        /// <summary> Дата окончания действия </summary>
        public DateTime? DateExpiration { get; set; }

        /// <summary> Префикс для промо-кодов </summary>
        public string Prefix { get; set; }
    }
}
