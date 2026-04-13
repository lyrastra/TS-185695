using System;
using System.Collections.Generic;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.SalesWaybills.Models
{
    public class DocsSalesWaybillsByCriteriaRequestDto
    {
        /// <summary>
        /// Пропустить кол-во накладных (по умолчанию 0) 
        /// </summary>
        public int Offset { get; set; } = 0;

        /// <summary>
        /// Кол-во накладных (по умолчанию 20)
        /// </summary>
        public int Limit { get; set; } = 20;
        
        // -- Фильтры --

        /// <summary>
        /// (опционально) Фильтр по дате документа: дата больше или равна переданному значению 
        /// </summary>
        public DateTime? StartDate { get; set; }
        
        /// <summary>
        /// (опционально) Фильтр по дате документа: дата меньше или равна переданному значению 
        /// </summary>
        public DateTime? EndDate { get; set; }
        
        /// <summary>
        /// (опционально) Фильтр по подстроке: в номере документа или в имени контрагента или в сумме
        /// </summary>
        public string Query { get; set; }
        
        /// <summary>
        /// (опционально) Фильтр по подстроке: в позициях накладных
        /// </summary>
        public string ItemQuery { get; set; }

        /// <summary>
        /// (опционально) Фильтр по контрагенту: идентификатор контрагента равен хотя бы одному значению из списка
        /// </summary>
        public List<int> KontragentIds { get; set; } = null;
        
        /// <summary>
        /// (опционально) Фильтр по договору: идентификатор договора равен хотя бы одному значению из списка
        /// </summary>
        public List<long> ContractIds { get; set; } = null;

        /// <summary>
        /// (опционально) Фильтр по сумме документа: сумма больше или равна переданному значению
        /// </summary>
        public decimal? MinDocumentSum { get; set; }
        
        /// <summary>
        /// (опционально) Фильтр по сумме документа: сумма меньше или равна переданному значению
        /// </summary>
        public decimal? MaxDocumentSum { get; set; }

        /// <summary>
        /// (опционально) Фильтр по статусу "Проведен в бухучете"
        /// </summary>
        public bool? ProvideInAccounting { get; set; } = null;

        /// <summary>
        /// (опционально) Фильтр по статусу "Подписан": статус равен хотя бы одному значению из списка
        /// </summary>
        public List<SignStatus> SignStatuses { get; set; } = null;

        /// <summary>
        /// (опционально) Фильтр по типу накладной: тип равен хотя бы одному значению
        /// 103 - Возврат поставщику
        /// 200 - Продажа
        /// 201 - Безвозмездная передача
        /// 202 - Возврат от покупателя
        /// </summary>
        public List<WaybillTypeCode> Types { get; set; } = null;

        // -- Сортировки --
        
        /// <summary>
        /// Направление сортировки (по умолчанию asc)
        /// asc - по возрастанию
        /// desc - по убыванию
        /// </summary>
        public string OrderBy { get; set; } = "asc";

        /// <summary>
        /// Поле сортировки (по умолчанию DocumentDate)
        /// documentdate - дата документа
        /// documentnumber - номер документа
        /// sum - сумма документа
        /// kontragentname - имя контрагента
        /// provideinaccounting - проведен в БУ
        /// signstatus - признак "подписан"
        /// </summary>
        public string SortBy { get; set; } = "documentdate";
    }
}