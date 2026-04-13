using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.ElectronicReports;

namespace Moedelo.ErptV2.Dto.Eds
{
    public class EdsHistoryWithRequisitesDto
    {
        /// <summary>Идентификатор фирмы</summary>
        public int FirmId { get; set; }

        /// <summary>Дата события</summary>
        public DateTime EventDate { get; set; }

        /// <summary>Тип события</summary>
        public EdsHistoryEvent EventType { get; set; }

        /// <summary>Дополнительная информация (содержимое пакета отправки или причина отклонения/проблем)</summary>
        public string Data { get; set; }

        /// <summary>Идентификатор документооборота для событий отправки</summary>
        public string PacketId { get; set; }

        /// <summary>Дополнительная информация для проблемных (предыдущий статус)</summary>
        public SignatureStatus PrevStatus { get; set; }

        /// <summary>Логин пользователя, совершившего действие над ЭЦП</summary>
        public string PerformedBy { get; set; }

        
        public EdsRequisitesDto Requisites { get; set; }
        /// <summary>Список направлений в ЭП</summary>
        public List<EReportsDirectionDto> Directions { get; set; }
    }
}
