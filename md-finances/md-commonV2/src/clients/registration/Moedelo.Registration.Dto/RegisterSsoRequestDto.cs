using Moedelo.Common.Enums.Enums.Billing;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.Common.Enums.Enums.Requisites;
using System;

namespace Moedelo.Registration.Dto
{
    public class RegisterSsoRequestDto
    {
        public IntegrationPartners Partner { get; set; }

        /// <summary>
        /// Основной тариф для регистрации клиента 
        /// </summary>
        public Tariff Tariff { get; set; }

        /// <summary>
        /// Промо тариф для акционных клиентов, начитает действовать сразу после основной 
        /// Если тариф не указан, значит клиент не является акционным 
        /// </summary>
        public Tariff? PromoTariff { get; set; }

        public string Login { get; set; }

        public string Fio { get; set; }

        public string Phone { get; set; }

        public string OrganizationName { get; set; }

        public string Inn { get; set; }
        private bool? _isOOO;
        public bool IsOOO
        {
            get
            {
                if (_isOOO == null && Inn == null)
                    throw new ArgumentNullException("Не задан ни ИНН ни IsOOO");
                if (Inn?.Length == 10)
                    return true;
                return _isOOO == true;
            }
            set
            {
                _isOOO = value;
            }
        }

        public string Kpp { get; set; }

        public bool IsUsn { get; set; }

        public bool IsOsno { get; set; }

        public bool IsEnvd { get; set; }

        public bool IsPatent { get; set; }

        public bool IsEmployerMode { get; set; }

        public double UsnSize { get; set; }

        public UsnTypes UsnType { get; set; }

        public string IpAddress { get; set; }

        public string RegLink { get; set; }

        public string UtmSource { get; set; }

        public string UtmSourceExtension { get; set; }

        public string UtmCampaign { get; set; }

        public string UtmContent { get; set; }

        public string UtmMedium { get; set; }

        public string UtmTerm { get; set; }

        public string GoogleAnalyticId { get; set; }
    }
}