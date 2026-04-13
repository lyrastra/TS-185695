using System;

namespace Moedelo.Eds.Dto.EdsExpireInformation
{
    public class ExpirationOnDateDto
    {
        public int FirmId { get; set; }

        /// <summary>
        /// Дата истечения срока действия ЭЦП
        /// </summary>
        public DateTime ExpireDate { get; set; }

        /// <summary>
        /// Кол-во дней до/после истечения срока действия ЭЦП:
        /// если > 0 кол-во после истечения срока действия ЭЦП
        /// если < 0 кол-во до истечения срока действия ЭЦП
        /// если = 0 последний день действия ЭЦП
        /// Используется для перерасчёта событий по ЭЦП в налоговом календаре
        /// </summary>
        public int Days { get; set; }
    }
}