using System;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto
{
    public class PatentWithoutAdditionalDataDto
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
        /// ОКУН, если Код вида предпринимательской деятельности="99"
        /// </summary>
        public int? OkunId { get; set; }

        /// <summary>
        /// Территория действия патента
        /// </summary>
        public string Territory { get; set; }

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
