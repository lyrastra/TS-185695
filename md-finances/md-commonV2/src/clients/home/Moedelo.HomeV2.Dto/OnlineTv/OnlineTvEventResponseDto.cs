using System;
using Moedelo.Common.Enums.Enums.OnlineTv;

namespace Moedelo.HomeV2.Dto.OnlineTv
{
    public class OnlineTvEventResponseDto
    {
        public int Id { get; set; }

        /// <summary> Имя вебинара </summary>
        public string Name { get; set; }

        public string ShortDescription { get; set; }

        /// <summary> Текстовое описание </summary>
        public string Description { get; set; }

        /// <summary> Дата и время начала трансляции </summary>
        public DateTime StartDateTime { get; set; }

        /// <summary> Тип: онлайн-трансляция или вебинар </summary>
        public OnlineTvEventTypes Type { get; set; }
    }
}