using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using System;

namespace Moedelo.BankIntegrations.Accounts.Kafka.Abstractions.RequestAccounts.CommandData
{
    public class AccountsRequestCommandData : IEntityCommandData
    {
        public IntegrationPartners IntegrationPartner { get; set; }
        public int FirmId { get; set; }
        public int UserId { get; set; }
        /// <summary>
        /// Если после создания счетов в кабинете нужно запросить выписки
        /// </summary>
        public bool IsRequestMovements { get; set; }
        /// <summary>
        /// Можно указать дату начала запроса выписок
        /// </summary>
        public DateTime? RequestMovementStartDate { get; set; }
    }
}