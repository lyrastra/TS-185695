using System;

namespace Moedelo.RequisitesV2.Dto.Patent
{
    public class CodeKindOfBusinessDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Код вида предпринимательской деятельности
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Описание вида предпринимательской деятельности
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Дата начала патента
        /// </summary>
        public DateTime? StartDate { get; set; }
    }
}
