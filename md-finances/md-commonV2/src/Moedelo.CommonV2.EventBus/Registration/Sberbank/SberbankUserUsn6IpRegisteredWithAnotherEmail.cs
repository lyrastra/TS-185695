/*
 namespace не соответствует расположению, чтобы уменьшить длину названия сообщения
 в RabbitMQ (она рассчитывается как Type.FullName + ":" + Type.Assembly.GetName().Name
 и ограничена 255 знаками
 */

namespace Moedelo.CommonV2.EventBus
{
    public class SberbankUsn6IpRegisteredWithAnotherEmail
    {
        public int    NewFirmId     { get; set; }
        public string PreviousLogin { get; set; }
    }
}
