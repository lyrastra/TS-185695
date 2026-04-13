namespace Moedelo.OurPartners.Dto.ManagementAccountings
{
    public class ManagementAccountingInfoDto
    {
        /// <summary>
        /// Ссылка для перехода в кабинет партнера
        /// </summary>
        public string Link { get; set; }
        
        /// <summary>
        /// Признак, достаточно ли прав для пользования системой партнёра
        /// </summary>
        public bool CanUseManagementAccounting { get; set; }
        
        
        /// <summary>
        /// Признак, необходимо ли добавить в начало ссылку на аунтификационный сервер
        /// </summary>
        public bool IsAuthorizeLink { get; set; }
    }
}