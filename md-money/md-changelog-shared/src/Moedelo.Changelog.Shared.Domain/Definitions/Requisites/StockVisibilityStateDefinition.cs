using Moedelo.Changelog.Shared.Domain.Attributes;
using Moedelo.Changelog.Shared.Domain.Enums;
using Moedelo.Common.AccessRules.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Requisites
{
    public class StockVisibilityStateDefinition : AutoEntityStateDefinition<
        StockVisibilityStateDefinition,
        StockVisibilityStateDefinition.State>
    {
        public StockVisibilityStateDefinition()
        {
            AddTag("stock_visibility");
        }

        public override ChangeLogEntityType EntityType { get; } = ChangeLogEntityType.StockVisibility;

        public class State
        {
            public int Id { get; set; }

            [Display(Name = "Год")]
            [FieldTypeYear]
            public int Year { get; set; }

            [Display(Name = "Склад виден")]
            public bool IsVisible { get; set; }
        }

        public override long GetEntityId(State state)
        {
            return state.Id;
        }

        public override IReadOnlyCollection<AccessRule> RequiredReadPermissions { get; }
            = Array.Empty<AccessRule>();

        protected override string GetEntityName(State state)
        {
            return $"{EntityTypeName} за {state.Year} год";
        }
    }
}
