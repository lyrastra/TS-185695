using System;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto
{
    public class DocumentContextDto
    {
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// Дата последнего изменения
        /// </summary>
        public DateTime? ModifyDate { get; set; }

        /// <summary>
        /// Пользователь, вносивший изменения в объект последним
        /// </summary>
        public string ModifyUser { get; set; }
    }
}