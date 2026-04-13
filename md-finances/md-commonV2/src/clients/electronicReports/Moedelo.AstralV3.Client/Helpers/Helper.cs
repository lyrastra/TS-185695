using System.IO;
using System.Xml.Linq;
using Moedelo.AstralV3.Client.Content;
using Moedelo.AstralV3.Client.Data;
using Moedelo.AstralV3.Client.Data.Enum;

namespace Moedelo.AstralV3.Client.Helpers
{
    public static class Helper
    {
        /// <summary>Парсинг ответа от сервиса регистрации Астрала</summary>
        /// <param name="result">Текст ответа</param>
        /// <returns>Результат парсинга, текст ошибки в случае её прихода или файл ответа в случае парсинга ответа на регистрацию</returns>
        public static ResponseTransfer<string> ParseRegServiceStandartResponse(string result)
        {
            TextReader tr = new StringReader(result);
            XDocument doc = XDocument.Load(tr);

            if (doc.Root == null || doc.Root.Name != "result")
            {
                return new ResponseTransfer<string>(AnswerType.Error, Resource.ErrorRegistrationAnswer);
            }

            XElement xElement = doc.Root.Element("code");

            if (xElement != null && int.Parse(xElement.Value) == 0)
            {
                XElement element = doc.Root.Element("packet");
                return element == null
                           ? new ResponseTransfer<string>(AnswerType.Success)
                           : new ResponseTransfer<string>(AnswerType.Success, element.Value, null);
            }

            XElement xElementE = doc.Root.Element("errorMessage");

            return xElementE != null
                       ? new ResponseTransfer<string>(AnswerType.Error, string.Format(Resource.ErrorRegistrationReason, xElementE.Value))
                       : new ResponseTransfer<string>(AnswerType.Error, string.Format(Resource.ErrorRegistrationUnknown));
        }

        /// <summary>Парсинг ответа от External сервиса Астрала</summary>
        /// <param name="result">Текст ответа</param>
        /// <returns>Результат парсинга, текст ошибки в случае её прихода или файл ответа в случае парсинга ответа на запрос</returns>
        public static ResponseTransfer<string> ParseExternalServiceStandartResponse(string result)
        {
            if (!result.StartsWith("["))
            {
                return new ResponseTransfer<string>(AnswerType.Error, result.Substring(0, System.Math.Min(500, result.Length))); //ограничим длину сообщения до 500 символов.
            }

            return new ResponseTransfer<string>(AnswerType.Success, result, null);
        }
    }
}
