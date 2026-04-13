using Moedelo.SpamV2.Dto.PushSender.Models;

namespace Moedelo.SpamV2.Dto.PushSender
{
    public interface IPushUserData<out T> where T: IPushNotificationData
    {
        
    }
}
