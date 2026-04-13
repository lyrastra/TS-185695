using Moedelo.Money.Enums;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Moedelo.Money.Api.Models.CashOrders
{
    public class CashOrderResponseDto : DynamicObject
    {
        private readonly Dictionary<string, PropertyInfo> properties;
        private readonly object instance;

        public CashOrderResponseDto(OperationType operationType, object instance)
        {
            OperationType = operationType;
            this.instance = instance;
            properties = instance.GetType()
               .GetProperties()
               .ToDictionary(x => x.Name);
        }

        public MoneyDirection Direction { get; set; }

        public OperationType OperationType { get; }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            var members = properties.Keys.ToList();
            members.AddRange(new[] { "Direction", "OperationType" });
            return members;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (properties.TryGetValue(binder.Name, out var property))
            {
                result = property.GetValue(instance);
                return true;
            }
            return base.TryGetMember(binder, out result);
        }
    }

    public class IncomingCashOrderResponseDto : CashOrderResponseDto
    {
        public IncomingCashOrderResponseDto(OperationType operationType, object instance)
            : base(operationType, instance)
        {
            Direction = MoneyDirection.Incoming;
        }
    }

    public class OutgoingCashOrderResponseDto : CashOrderResponseDto
    {
        public OutgoingCashOrderResponseDto(OperationType operationType, object instance)
            : base(operationType, instance)
        {
            Direction = MoneyDirection.Outgoing;
        }
    }
}
