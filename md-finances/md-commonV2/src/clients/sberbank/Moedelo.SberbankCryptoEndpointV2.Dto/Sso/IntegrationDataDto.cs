using System;

namespace Moedelo.SberbankCryptoEndpointV2.Dto.Sso
{
    public class IntegrationDataDto
    {
        public int FirmId { get; set; }
        
        /// <summary> Сессия токена доступа </summary>
        public DateTime SessionLastDate { get; set; }
       
        /// <summary> Токен доступа </summary>
        public string AccessToken { get; set; }
        
        /// <summary> Токен обновления </summary>
        public string RefreshToken { get; set; } 
        
        /// <summary> ИД партнера 'Мое дело' в системе Сбера </summary>
        public long ClientId { get; set; }
        
        /// <summary> Тип пользователя (смс или токен) </summary>
        public int SberbankUserType { get; set; }
        
        /// <summary> Прайс-лист для подписок </summary>
        public int? AcceptancePriceListId { get; set; }
   
        /// <summary> Дата изменений последнего шага по подпискам </summary>
        public DateTime? AcceptanceLastDate { get; set; }

        /// <summary> Guid последнего выставленного ПТ в Сбербанк </summary>
        public Guid? LastErrorPaymentGuid { get; set; }

        /// <summary> Количество неуспешных попыток выставления ПТ в Сбербанк </summary>
        public int? ErrorPaymentCount { get; set; }

        /// <summary> Признак, что счёт для подписок в Сбербанке заблокирован </summary>
        public bool? BlockedAcceptanceSettlement { get; set; }
        
        /// <summary> Признак, что счёт для подписок в Сбербанке закрыт </summary>
        public bool? ClosedAcceptanceSettlement { get; set; }

        /// <summary> Дата последней проверки счёта в Сбербанке на блокировку </summary>
        public DateTime? AcceptanceSettlementCheckLastDate { get; set; }
    }
}