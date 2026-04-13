namespace Moedelo.OutSystemsIntegrationV2.Dto.ExchangeRates
{
    public class ExchangeRateValueDto
    {
        /// <summary>
        /// Валюта
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Курс валюты, скорректированный на номинал (за 1 ед.)
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Курс валюты за номинал
        /// </summary>
        public decimal NominalValue { get; set; }

        /// <summary>
        /// Номинал
        /// </summary>
        public int Nominal { get; set; }
    }
}