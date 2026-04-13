using Moedelo.Outsource.Dto.PageAccess.Enums;

namespace Moedelo.Outsource.Dto.PageAccess
{
    public class GroupSettingItemPostDto
    {
        /// <summary>
        /// Идентификатор редактируемой настройки.
        /// Если настройка не существует - Id = 0
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Флаг, включена настройка или выключена
        /// </summary>
        public bool Enabled { get; set; }

        public PageType Page { get; set; }

        public string[] ExcludedPaymentMethods { get; set; }
    }
}