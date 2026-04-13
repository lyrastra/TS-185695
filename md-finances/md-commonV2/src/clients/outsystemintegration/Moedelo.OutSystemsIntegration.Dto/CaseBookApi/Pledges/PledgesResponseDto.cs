using System;

namespace Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi.Pledges
{
    /// <summary>
    /// Залоги (ответ)
    /// </summary>
    public class PledgesResponseDto
    {
        /// <summary>
        /// Залоги
        /// </summary>
        public PledgeResponseDto[] Pledges { get; set; }

        /// <summary>
        /// Наличие ошибки при запросе
        /// </summary>
        public bool IsError { get; set; }

        /// <summary>
        /// Версия кейзбука
        /// </summary>
        public string CaseBookVersion { get; set; }

        /// <summary>
        /// Версия данных
        /// </summary>
        public string DataVersion { get; set; }

        /// <summary>
        /// Номер страницы
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Текущее кол-во записей
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Кол-во записей всего
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Кол-во страниц
        /// </summary>
        public int Pages { get; set; }
    }
}
