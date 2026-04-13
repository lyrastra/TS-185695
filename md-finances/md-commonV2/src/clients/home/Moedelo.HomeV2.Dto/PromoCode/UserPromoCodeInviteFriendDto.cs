using System;

namespace Moedelo.HomeV2.Dto.PromoCode
{
    /// <summary>
    /// Сохранение данных пользователя в одноимённой таблице в схеме mrk, 
    /// который участвует в акции "Приведи друга"
    /// </summary>
    public class UserPromoCodeInviteFriendDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Id пользователя, которому предоставлен промокод
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Id пользователя, который пригласил в сервис
        /// </summary>
        public int? InviterUserId { get; set; }

        /// <summary>
        /// Id промокода из таблицы PromoCode
        /// </summary>
        public int PromocodeId { get; set; }

        /// <summary>
        /// Был ли пользователь учтён для пересчёта скидки пригласившего пользователя
        /// </summary>
        public bool Tracked { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }
    }
}