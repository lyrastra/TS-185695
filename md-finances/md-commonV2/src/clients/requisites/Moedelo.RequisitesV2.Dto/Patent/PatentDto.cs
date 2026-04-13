using System;

namespace Moedelo.RequisitesV2.Dto.Patent
{
    public class PatentDto
    {
        public long Id { get; set; }

        /// <summary>
        /// Код патента
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Описание патента
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Краткое название для быстрого поиска
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Дата начала действия патента
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Срок действия (в месяцах)
        /// </summary>
        [Obsolete("У патентов с 2020 года срок указывается в днях, для рассчёта нужно использовать StartDate и EndDate")]
        public int? ValidityPeriod { get; set; }

        /// <summary>
        /// Дата окончания действия патента
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Базовая доходность
        /// </summary>
        public int BasicProfitability { get; set; }

        /// <summary>
        /// Стоимость патента
        /// </summary>
        public int Cost { get; set; }

        /// <summary>
        /// Патент остановлен?
        /// </summary>
        public bool IsStopped { get; set; }

        /// <summary>
        /// Id кода вида предпринимательской деятельности
        /// </summary>
        public int CodeKindOfBusinessId { get; set; }

        /// <summary>
        /// Код вида предпринимательской деятельности. Поле актуально для Get(id)-метода
        /// AdditionalData
        /// </summary>
        public string CodeKindOfBusiness { get; set; }

        /// <summary>
        /// Дата вида предпринимательской деятельности. Поле актуально для Get(id)-метода
        /// AdditionalData
        /// </summary>
        public DateTime? CodeKindOfBusinessDate { get; set; }

        /// <summary>
        /// ОКУН, если Код вида предпринимательской деятельности="99"
        /// </summary>
        public int? OkunId { get; set; }

        /// <summary>
        /// Территория действия патента
        /// </summary>
        public string Territory { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public int CreateUserId { get; set; }

        /// <summary>
        /// True, если к патенту привязано хотя бы одно Cписание или Поступление из раздела Деньги,
        /// или хотя бы одна карточка Сотрудника. Поле актуально для Get(id)-метода
        /// AdditionalData
        /// </summary>
        public bool IsLinked { get; set; }

        /// <summary>
        /// Последняя дата операции с данным патентом
        /// AdditionalData
        /// </summary>
        public DateTime? LastIncomingOperationDate { get; set; }

        /// <summary>
        /// ОКТМО территории действия патента
        /// </summary>
        public string Oktmo { get; set; }

        /// <summary>
        /// Код бюджетной классификации для патента
        /// </summary>
        public string Kbk { get; set; }
    }
}
