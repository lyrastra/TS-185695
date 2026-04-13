using System;

namespace Moedelo.TestData.Client.Dto
{
    public class TestDataSettingDto
    {
        /// <summary>
        /// Идентификатор основной фирмы пользователя
        /// </summary>
        public int FirmId { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Идентификатор тестовой фирмы
        /// </summary>
        public int TestDataFirmId { get; set; }

        /// <summary>
        /// Дата создания тестовой фирмы
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
