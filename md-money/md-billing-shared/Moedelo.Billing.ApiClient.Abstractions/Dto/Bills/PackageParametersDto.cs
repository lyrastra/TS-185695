using System;
using System.Collections.Generic;
using Moedelo.Billing.Shared.Enums;

namespace Moedelo.Billing.Abstractions.Dto.Bills;

public class PackageParametersDto
{
    public int Id { get; set; }

    public string Duration { get; set; }

    public IReadOnlyDictionary<string, MarketplaceModifierValue> Values { get; set; }

    public string PromoCode { get; set; }

    public BillOperationType OperationType { get; set; }

    public DateTime? StartDate { get; set; }

    public class MarketplaceModifierValue
    {
        public string Code { get; set; }

        public MarketplaceModifierType? ModifierType { get; set; }

        /// <summary>
        /// Прописываем Id градации в лоб - костыль из-за сложности определения типа опции
        /// Используется в MarketplaceHelperCart.cs - BuildModifierOptions()
        /// </summary>
        public int? GradationId { get; set; }

        public ValueModel Value { get; set; }

        public class ValueModel
        {
            public string Text { get; set; }

            public bool? IsOn { get; set; }

            public int? Count { get; set; }

            public int? OverriddenValue { get; set; }
        }
    }
}