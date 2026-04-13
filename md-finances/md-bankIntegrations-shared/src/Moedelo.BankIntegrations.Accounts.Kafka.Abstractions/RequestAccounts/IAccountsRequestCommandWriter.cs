using Moedelo.BankIntegrations.Accounts.Kafka.Abstractions.RequestAccounts.CommandData;
using Moedelo.BankIntegrations.Kafka.Abstractions.RetryData;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.Accounts.Kafka.Abstractions.RequestAccounts
{
    public interface IAccountsRequestCommandWriter
    {
        /// <summary>
        /// Добавляет событие запроса расчётных счетов в очередь обработки.
        /// <param name="message">Данные события запроса расчётных счетов.</param>
        /// </summary>
        Task WriteAsync(RetryDataWrapper<AccountsRequestCommandData> message);
    }
}