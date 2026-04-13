using System;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Moedelo.AstralV3.Client.AstralInteractionsLogger.MessagesInspector
{
    /// <summary>
    /// Класс, перехватывающий WCF-сообщения на клиентской стороне. Расчитан под то, что на каждый
    /// запрос создаётся свой экземпляр
    /// </summary>
    public class MessagesInspector : IMessagesInspector
    {
        /// <summary>
        /// Тело запроса
        /// </summary>
        public string Request { get; private set; }

        /// <summary>
        /// Тело ответа
        /// </summary>
        public string Response { get; private set; }

        /// <summary>
        /// Длительность запроса
        /// </summary>
        public TimeSpan Duration { get; private set; }

        /// <summary>
        /// Пользователь, из под которого выполняется запрос
        /// </summary>
        public int? UserId { get; private set; }

        /// <summary>
        /// Фирма, из под которой выполняется запрос
        /// </summary>
        public int? FirmId { get; private set; }

        /// <summary>
        /// Для измерения длительности запросов
        /// </summary>
        private Stopwatch _stopwatch = new Stopwatch();

        /// <summary>
        /// Конструктор, принимает фирму и пользователя из под которых выполняется запрос
        /// </summary>
        public MessagesInspector(int? userId = null, int? firmId = null)
        {
            // Исключение может случиться до завершения запроса
            Duration = TimeSpan.FromTicks(0);
            UserId = userId;
            FirmId = firmId;
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            // Не нужно
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            // Навешиваем сами себя в качестве инспектора сообщений
            clientRuntime.ClientMessageInspectors.Add(this);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            // Не нужно, Dispatch - это для серверной стороны
        }

        public void Validate(ServiceEndpoint endpoint)
        {
            // Не нужно
        }

        // Перехватчик запроса
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            // Снимаем копию с запроса (оригинал возвращать нельзя, т.к. он модифицируется внешним кодом, к тому же если прочитать тело запроса ридером - то внешний код кинет
            // эксепшн, что тело уже прочитаною).
            var messageBuffer = request.CreateBufferedCopy(int.MaxValue);
            request = messageBuffer.CreateMessage(); // Отдаём наружу ещё непрочитанную копию

            // Копия сообщения, мы можем с ней работать
            var messageCopy = messageBuffer.CreateMessage();
            Request = GetMessageBodyAsString(messageCopy); // Тело запроса

            // Запускаем таймер
            _stopwatch.Start();

            return null;
        }

        /// <summary>
        /// Перехватчик ответа
        /// </summary>
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            _stopwatch.Stop();

            var messageBuffer = reply.CreateBufferedCopy(int.MaxValue);
            reply = messageBuffer.CreateMessage();

            Response = GetMessageBodyAsString(messageBuffer.CreateMessage()); // Тело ответа
            Duration = _stopwatch.Elapsed;
        }

        /// <summary>
        /// Возвращает содержимое сообщения в виде строки.
        /// </summary>
        private string GetMessageBodyAsString(Message message)
        {
            using (var reader = message.GetReaderAtBodyContents())
            {
                return reader.ReadOuterXml();
            }
        }
    }
}
