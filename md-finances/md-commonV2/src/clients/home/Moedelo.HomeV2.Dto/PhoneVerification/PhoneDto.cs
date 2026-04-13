namespace Moedelo.HomeV2.Dto.PhoneVerification
{
    public class PhoneDto
    {
        public int PhoneId { get; set; }

        public int FirmId { get; set; }

        /// <summary>
        /// Номер телефона, привязанный к фирме, по которому продажники могут обзванивать
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}
