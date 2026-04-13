using System;

namespace Moedelo.Money.Business.Abstractions.SkypeNotifications
{
    public interface ISkypeNotificationSender
    {
        void SendException(string logger, Exception ex, object ext = null);
    }
}
