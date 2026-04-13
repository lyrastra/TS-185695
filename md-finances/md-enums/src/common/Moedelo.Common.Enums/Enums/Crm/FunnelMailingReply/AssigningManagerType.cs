namespace Moedelo.Common.Enums.Enums.Crm.FunnelMailingReply
{
    /// <summary>
    /// Тип менеджера, на которого будут созданы задача и сделка
    /// </summary>
    public enum AssigningManagerType
    {
        None = 0,

        /// <summary>
        /// Менеджер в биллинге
        /// </summary>
        BillingOperator = 1,

        /// <summary>
        /// Менеджер по обучению БИЗ
        /// </summary>
        BizTeacher = 2,

        /// <summary>
        /// Назначение менеджера группы обучения по переданному идентификатору
        /// </summary>
        TrainingGroupOperatorId = 3,

        /// <summary>
        /// Назначение менеджера группы сопровождения по переданному идентификатору
        /// </summary>
        SupportGroupOperatorId = 4
    }
}