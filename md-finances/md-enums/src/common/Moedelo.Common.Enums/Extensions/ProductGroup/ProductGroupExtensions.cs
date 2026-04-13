using System.Collections.Generic;
using System.ComponentModel;
using Moedelo.Common.Enums.Enums;
using Moedelo.Common.Enums.Enums.RegistrationService;

namespace Moedelo.Common.Enums.Extensions.ProductGroup
{
    public static class ProductGroupExtensions
    {
        private static Dictionary<string, ProductGroups> _enums = new Dictionary<string, ProductGroups>
        {
            {"FREE", ProductGroups.FREE },
            {"BIZ", ProductGroups.BIZ },
            {"SPS", ProductGroups.SPS },
            {"OUT", ProductGroups.OUT }
        };

        private static Dictionary<ProductGroups, string> _titles = new Dictionary<ProductGroups, string>
        {
            {ProductGroups.FREE, "Бесплатные"},
            {ProductGroups.BIZ, "Интернет-бухгалтерия"},
            {ProductGroups.SPS, "Бюро"},
            {ProductGroups.OUT, "Аутсорсинг"}
        };

        private static Dictionary<ProductGroups, RegistrationType> _registrationTypes = new Dictionary<ProductGroups, RegistrationType>
        {
            {ProductGroups.FREE, RegistrationType.Businessman},
            {ProductGroups.BIZ, RegistrationType.Businessman},
            {ProductGroups.OUT, RegistrationType.Businessman},
            {ProductGroups.SPS, RegistrationType.Office}
            
        };

        /// <summary> Получить название группы продуктов </summary>
        public static string GetTitleByEnum(this ProductGroups productGroup)
        {
            string title;

            if (!_titles.TryGetValue(productGroup, out title))
            {
                throw new InvalidEnumArgumentException("Нет такого ProductGroup");
            }
            return title;

        }


        /// <summary>
        /// Получить ProductGroups по коду
        /// </summary>
        /// <param name="code">Код энама в базе</param>
        /// <returns></returns>
        public static ProductGroups GetEnumByCode(string code)
        {
            ProductGroups group;

            if (!_enums.TryGetValue(code, out group))
            {
                throw new InvalidEnumArgumentException("Нет такого ProductGroup");
            }
            return group;
        }

        /// <summary> Получить соответствие группы продукта и типа регистрации </summary>
        public static Dictionary<ProductGroups, RegistrationType> GetRegistrationTypes()
        {
            return _registrationTypes;
        }

        public static Dictionary<ProductGroups, string> GetProductGroups()
        {
            return _titles;
        }
    }
}