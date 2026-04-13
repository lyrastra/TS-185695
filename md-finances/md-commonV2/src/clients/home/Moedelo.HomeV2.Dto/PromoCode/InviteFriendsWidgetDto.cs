namespace Moedelo.HomeV2.Dto.PromoCode
{
    public class InviteFriendsWidgetDto
    {
        /// <summary>
        /// Количество оставшихся приглашений
        /// </summary>
        public int InvitesCount { get; set; }

        /// <summary>
        /// Был ли пользователь приглашён в сервис по программе "Пригласи друга"
        /// 0 - Не участник,
        /// 1 - Участник программы (приглашен)
        /// 2 - Оплачен со скидкой по программе (приглашен + оплатил)
        /// </summary>
        public int ParticipantStatus { get; set; }

        /// <summary>
        /// Общее вознаграждение
        /// </summary>
        public decimal RewardSum { get; set; }
    }
}