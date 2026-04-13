using Moedelo.BankIntegrations.Accounts.Kafka.Abstractions.RequestAccounts;
using Moedelo.BankIntegrations.Accounts.Kafka.Abstractions.RequestAccounts.CommandData;
using Moedelo.BankIntegrations.Kafka.Abstractions.RetryData;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.Accounts.Kafka.RequestAccounts
{
    [InjectAsSingleton(typeof(IAccountsRequestCommandWriter))]
    internal sealed class AccountsRequestCommandWriter : IAccountsRequestCommandWriter
    {
        private readonly IMoedeloEntityCommandKafkaTopicWriter commandWriter;
        private readonly MoedeloEntityCommandKafkaMessageDefinitionBuilder definitionBuilder;

        public AccountsRequestCommandWriter(IMoedeloEntityCommandKafkaTopicWriter commandWriter)
        {
            this.commandWriter = commandWriter;
            definitionBuilder = MoedeloEntityCommandKafkaMessageDefinitionBuilder.For(
                Topics.AccountsRequestEntity.Command.Topic,
                Topics.AccountsRequestEntity.EntityName);
        }


        public Task WriteAsync(RetryDataWrapper<AccountsRequestCommandData> commandData)
        {
            var commandDefinition = definitionBuilder.CreateCommandDefinition(commandData.Data.IntegrationPartner.ToString(), commandData);

            return commandWriter.WriteCommandDataAsync(commandDefinition);
        }
    }
}