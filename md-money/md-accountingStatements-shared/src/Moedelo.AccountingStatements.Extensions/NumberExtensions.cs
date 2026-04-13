using System.Collections.Generic;
using System.Linq;
using Moedelo.AccountingStatements.Extensions.Models;

namespace Moedelo.AccountingStatements.Extensions
{
    /// <summary>
    /// Нумерует документы (написан для бухсправок, но может быть примерим к любым сущностям, реализуемым IApplyNumber)
    /// </summary>
    public static class NumberExtensions
    {
        /// <summary>
        /// Присвоить документам УНИКАЛЬНЫЕ ДЛЯ СПИСКА номера, генерируемые на основе их дат
        /// </summary>
        public static void ApplyNumbersFromDates(this IEnumerable<IApplyNumber> models)
        {
            if (models == null)
            {
                return;
            }
            
            foreach (var gr in models.Where(x => x != null).GroupBy(x => x.Date))
            {
                var counter = gr.Count() == 1 ? 0 : 1;
                var number = new NumberFromDate(gr.Key, counter);

                foreach (var model in gr)
                {
                    model.Number = number.ToString();
                    number++;
                }
            }
        }

        /// <summary>
        /// Присвоить документу номер, генерируемый на основе даты
        /// </summary>
        public static void ApplyNumberFromDate(this IApplyNumber model)
        {
            if (model == null)
            {
                return;
            }
            
            model.Number = new NumberFromDate(model.Date, 0).ToString();
        }
        
        /// <summary>
        /// Присвоить документу номер на основе даты и вернуть сам документ
        /// </summary>
        public static T WithNewNumber<T>(this T doc) where T : IApplyNumber
        {
            doc.ApplyNumberFromDate();
            return doc;
        }
    }
}