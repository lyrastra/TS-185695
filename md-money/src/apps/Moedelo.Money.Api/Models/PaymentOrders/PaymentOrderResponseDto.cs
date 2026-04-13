using Moedelo.Money.Enums;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Moedelo.Money.Api.Models.PaymentOrders
{
    public class PaymentOrderResponseDto : DynamicObject
    {
        private readonly Dictionary<string, PropertyInfo> properties;
        private readonly object instance;

        public PaymentOrderResponseDto(OperationType operationType, object instance)
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
            members.AddRange(["Direction"]);
            if (!members.Contains("OperationType"))
            {
                members.AddRange(["OperationType"]);
            }
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

    public class IncomingPaymentOrderResponseDto : PaymentOrderResponseDto
    {
        public IncomingPaymentOrderResponseDto(OperationType operationType, object instance)
            : base(operationType, instance)
        {
            Direction = MoneyDirection.Incoming;
        }
    }

    public class OutgoingPaymentOrderResponseDto : PaymentOrderResponseDto
    {
        public OutgoingPaymentOrderResponseDto(OperationType operationType, object instance)
            : base(operationType, instance)
        {
            Direction = MoneyDirection.Outgoing;
        }
    }
}
