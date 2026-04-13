using System.Collections.Generic;
using System.Linq;

namespace Moedelo.BillingV2.Dto.BackofficeBilling.Bills.BillRequest.Builder
{
    struct ModifierValue
    {
        public string Code { get; set; }

        public string GradationName { get; set; }

        public decimal? GradationScaleValue { get; set; }
    }

    public class BillRequestBuilder
    {
        private string Code { get; set; }

        private int Duration { get; set; }

        private List<ModifierValue> Modifiers = new List<ModifierValue>();

        public void SetProductConfigurationCode(string code)
        {
            Code = code;
        }

        public void SetDuration(int duration)
        {
            Duration = duration;
        }

        public void ClearModifiers()
        {
            Modifiers.Clear();
        }

        public void AddModifier(string modifierTypeCode, string gradationName, decimal? gradationScaleValue = null)
        {
            var item = new ModifierValue
            {
                Code = modifierTypeCode,
                GradationName = gradationName,
                GradationScaleValue = gradationScaleValue
            };

            Modifiers.Add(item);
        }

        public ProductConfigurationRequestDto ToDto()
        {
            var modifiers = Modifiers.ToDictionary(
                item => item.Code,
                item => new ModifierRequestDto
                {
                    GradationName = item.GradationName,
                    GradationScaleValue = item.GradationScaleValue
                });

            return new ProductConfigurationRequestDto
            {
                Code = Code,
                Duration = Duration,
                ModifiersValues = modifiers
            };
        }
    }
}
