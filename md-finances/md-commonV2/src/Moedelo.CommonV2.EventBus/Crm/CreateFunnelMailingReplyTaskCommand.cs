using System;
using Moedelo.Common.Enums.Enums.Crm.FunnelMailingReply;

namespace Moedelo.CommonV2.EventBus.Crm
{
    /// <summary>
    /// Команда создания задачи по отклику с рассылки
    /// </summary>
    public class CreateFunnelMailingReplyTaskCommand
    {
        public int FirmId { get; set; }

        /// <summary>
        /// Название создаваемой задачи
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Описание задачи
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Продукт регистрации
        /// </summary>
        public string RegistrationProduct { get; set; }

        /// <summary>
        /// Код воронки
        /// </summary>
        public string FunnelCode { get; set; }

        /// <summary>
        /// Тип задачи (значение в crm)
        /// </summary>
        public string TaskType { get; set; }

        /// <summary>
        /// Назначить менеджеру в биллинге
        /// </summary>
        [Obsolete("Use AssigningManagerType")]
        public bool IsAssignManagerInBilling { get; set; }

        /// <summary>
        /// Тип менеджера, на которого будут созданы задача и сделка
        /// </summary>
        public AssigningManagerType AssigningManagerType { get; set; }

        /// <summary>
        /// Опционально. Идентификатор оператора МД, которого следует назначить ответственным по задаче
        /// </summary>
        public int? OperatorId { get; set; }

        /// <summary>
        /// Источник команды
        /// </summary>
        public CommandSource CommandSource { get; set; }

        public MailingType MailingType { get; set; }
    }
}
