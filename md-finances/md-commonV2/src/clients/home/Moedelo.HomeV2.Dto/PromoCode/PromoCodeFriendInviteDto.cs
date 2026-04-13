namespace Moedelo.HomeV2.Dto.PromoCode
{
    /// <summary>
    /// Данные, необходимые для сохранения промокода и информации о пользователе
    /// </summary>
    public class PromoCodeFriendInviteDto
    {
        public PromoCodeDto PromoCodeDto { get; set; }

        public UserPromoCodeInviteFriendDto UserPromoCodeInviteFriendDto { get; set; }
    }
}