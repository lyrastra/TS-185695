using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Parsers.Klto1CParser.Helpers
{
    public class DocumentTypeHelper
    {
        private static int DefaultDocumentType = 1;

        private static readonly Dictionary<int, string> DocumentTypes = new Dictionary<int, string> {
                                   {1, "Платежное поручение"},
                                   {9, "Мемориальный ордер"},
                                   {17, "Банковский ордер"}
                               };

        /// <summary>
        /// operationType передавать в формате:
        /// http://www.consultant.ru/document/cons_doc_LAW_132831/e9c0396d514478f0d8232f5def698c2b54097878/
        /// 01 Списано, зачислено по платежному поручению, по поручению банка
        /// 02 Оплачено, зачислено по платежному требованию
        /// 03 Оплачен наличными денежный чек, выдано по расходному кассовому ордеру
        /// 04 Поступило наличными по объявлению на взнос наличными, приходному кассовому ордеру, препроводительной ведомости к сумке 0402300
        /// 05 Исключено с 1 января 2014 года. - Указание Банка России от 04.09.2013 N 3053-У
        /// 06 Оплачено, зачислено по инкассовому поручению
        /// 07 Оплачено, поступило по расчетному чеку
        /// 08 Открытие аккредитива, зачисление сумм неиспользованного, аннулированного аккредитива
        /// 09 Списано, зачислено по мемориальному ордеру, а также по первичным учетным документам с реквизитами счетов по дебету и кредиту
        /// 10 - 11 Исключены с 1 января 2014 года. - Указание Банка России от 04.09.2013 N 3053-У
        /// 12 Зачислено на основании авизо
        /// 13 Исключено с 1 января 2014 года. - Указание Банка России от 04.09.2013 N 3053-У
        /// 16 Списано, зачислено по платежному ордеру
        /// 17 Списано, зачислено по банковскому ордеру
        /// 18 Списано, зачислено по ордеру по передаче ценностей
        /// </summary>
        /// <param name="operationType"></param>
        /// <returns></returns>
        public static string GetDocumentTypeName(string operationType)
        {
            if (string.IsNullOrEmpty(operationType) ||
                !int.TryParse(operationType, out int numberDocType) ||
                !DocumentTypes.ContainsKey(numberDocType))
            {
                return DocumentTypes[DefaultDocumentType];
            }

            return DocumentTypes[numberDocType];
        }
    }
}