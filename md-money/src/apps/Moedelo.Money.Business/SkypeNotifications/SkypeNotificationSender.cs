using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.AspNetCore.Abstractions.BackgroundTasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Money.Business.Abstractions.SkypeNotifications;
using Moedelo.Spam.ApiClient.Abastractions.Dto.Messengers;
using Moedelo.Spam.ApiClient.Abastractions.Interfaces.Messengers;
using System;
using System.Reflection;

namespace Moedelo.Money.Business.SkypeNotifications
{
    [InjectAsSingleton(typeof(ISkypeNotificationSender))]
    public class SkypeNotificationSender : ISkypeNotificationSender
    {
        private const string CustomEventName = "BIZ_MONEY_API";
        private readonly IBackgroundTaskQueue backgroundTaskQueue;
        private readonly ISkypeSenderClient skypeSenderClient;
        private readonly SettingValue environment;

        public SkypeNotificationSender(
            IBackgroundTaskQueue backgroundTaskQueue,
            ISkypeSenderClient skypeSenderClient,
            ISettingRepository settingRepository)
        {
            this.backgroundTaskQueue = backgroundTaskQueue;
            this.skypeSenderClient = skypeSenderClient;
            environment = settingRepository.Get("Environment");
        }
        public void SendException(string logger, Exception ex, object ext)
        {
            if (environment.Value != "prod")
            {
                return;
            }
            var request = new SkypeSendOptionsDto
            {
                EventName = CustomEventName,
                Message = FormatMessage(logger, ex, ext)
            };
            backgroundTaskQueue.QueueBackgroundWorkItem(x => skypeSenderClient.SendAsync(request));
        }

        private static string FormatMessage(string logger, Exception ex, object ext)
        {
            var message = new
            {
                AppName = Assembly.GetEntryAssembly().GetName().Name,
                logger,
                Exception = ex,
                ext
            };
            return message.ToJsonString();
        }
    }
}
