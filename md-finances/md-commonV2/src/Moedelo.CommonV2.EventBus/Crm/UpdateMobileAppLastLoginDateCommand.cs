using System;

namespace Moedelo.CommonV2.EventBus.Crm
{
    /// <summary>
    /// Команда обновления даты последней авторизации в мобильном приложении
    /// </summary>
    public class UpdateMobileAppLastLoginDateCommand
    {
        public string SuiteCrmAccountId { get; set; }

        public DateTime LoginDate { get; set; }
    }
}