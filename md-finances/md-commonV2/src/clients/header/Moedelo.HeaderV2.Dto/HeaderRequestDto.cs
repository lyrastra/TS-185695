namespace Moedelo.HeaderV2.Dto
{
    public class HeaderRequestDto
    {
        public int UserId { get; set; }

        public int FirmId { get; set; }

        public MenuItemType ActiveMenuItemType { get; set; }
    }
}
