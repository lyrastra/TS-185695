using System.Runtime.Serialization;
using Moedelo.AstralV3.Client.Data.Enum;

namespace Moedelo.AstralV3.Client.Data
{
    /// <summary> Класс ответа от сервиса при обмене файлами </summary>
    [DataContract]
    public class ResponseTransfer<T>
    {
        /// <summary>Результат</summary>
        [DataMember]
        public AnswerType Result { get; set; }

        /// <summary>Сообщение</summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>Значение</summary>
        [DataMember]
        public T Value { get; set; }

        public ResponseTransfer(AnswerType result) : this(result, string.Empty)
        {
        }

        public ResponseTransfer(AnswerType result, string message)
        {
            Result = result;
            Message = message;
        }

        public ResponseTransfer(AnswerType result, T value, string message)
        {
            Result = result;
            Message = message;
            Value = value;
        }
    }
}
