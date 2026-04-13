namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto
{
    public class MoneyAccessDto
    {
        /// <summary>
        /// Право на создание расчетных счетов в иностранной валюте
        /// </summary>
        public bool CanEditCurrencyAccount { get; set; }

        /// <summary>
        /// Признак, необходимо ли сохранять счета в иностранной валюте как рублевые
        /// </summary>
        public bool CurrencyAccountAsRubAccount { get; set; }
    }
}