using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.Accounts;
using Moedelo.BankIntegrations.ApiClient.Dto.PointBank.Consents;
using Moedelo.BankIntegrations.ApiClient.Dto.PointBank.Customers;
using Moedelo.BankIntegrations.Dto.Movements;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.Pointbank
{
    public interface IPointbankAdapterClient
    {
        /// <summary> Метод записи и обмена кода на токены </summary>
        /// <param name="code"> Код авторизации от банка </param>
        /// <param name="firmId"> Идентификатор фирмы </param>
        /// <param name="redirectUri"> Uri для перехода, который указывали при регистрации в API в интернет-банке. На него будет произведён редирект для получения Access и Refresh токенов</param>
        /// <returns> Состояние о записи токенов </returns>
        Task<bool> WriteTokenByCodeAsync(int firmId, string code, string redirectUri);

        /// <summary> Метод создания разрешения на доступ </summary>
        /// <param name="permissions"> Типы данных доступа </param>
        /// <returns> Уникальный идентификатор, предназначенный для идентификации разрешения </returns>
        Task<string> CreateConsentIdAsync(string[] permissions);
        
        /// <summary> Метод получения информации о разрешении </summary>
        /// <param name="firmId"> Идентификатор фирмы </param>
        /// <param name="consentId"> Уникальный идентификатор, предназначенный для идентификации разрешения </param>
        /// <returns> Информация о разрешении </returns>
        Task<ConsentDto> GetConsentInfoAsync(int firmId, string consentId);

        /// <summary> Метод получения списка дочерних разрешений </summary>
        /// <param name="firmId"> Идентификатор фирмы </param>
        /// <param name="consentId"> Уникальный идентификатор родительского разрешения </param>
        /// <returns> Список дочерних разрешений </returns>
        Task<GetConsentsDto> GetAllChildConsentsAsync(int firmId, string consentId);
        
        /// <summary> Метод получения список доступных клиентов/компаний </summary>
        /// <param name="firmId"> Идентификатор фирмы </param>
        /// <returns> Список доступных компаний </returns>
        Task<List<CustomerDto>> GetCustomersListAsync(int firmId);
        
        /// <summary> Метод получения счетов по компании </summary>
        /// <param name="firmId"> Идентификатор фирмы </param>
        /// <param name="customerCode"> Уникальный код компании в банке </param>
        /// <returns> Список счетов </returns>
        Task<GetAccountsResponseDto> GetAccountsByCustomerCodeAsync(int firmId, string customerCode);

        Task<RequestMovementResponseDto> RequestMovementListAsync(RequestMovementRequestDto requestDto);
    }
}
