namespace Moedelo.HomeV2.Dto.Social
{
    public class UserSocialInfoDto
    {
        public int UserId { get; set; }

        public string Fio { get; set; }

        /// <summary>
        /// Ссылка на скачивание фотографии: (моё дело или соцсеть)
        /// проставляется в <img src="Photo"/>
        /// </summary>
        public string Photo { get; set; }
    }
}