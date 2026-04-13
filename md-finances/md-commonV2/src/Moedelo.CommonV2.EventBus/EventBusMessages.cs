using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.CommonV2.EventBus.Stocks;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

namespace Moedelo.CommonV2.EventBus
{
    public partial class EventBusMessages
    {
        static EventBusMessages()
        {
            //ниже в рантайме через рефлексию используется этот конструктор.
            var testObj = new EventBusEventDefinition<object>("test");
            string typeName = testObj.GetType().Name;

            EventsByName = typeof(EventBusMessages)
                .GetFields()
                .Where(f => f.FieldType.Name == typeName)
                .Select(f =>
                {
                    var obj = f.FieldType.GetConstructor(new[] {typeof(string)})?.Invoke(new object[] {f.Name});
                    f.SetValue(null, obj);
                    return f;
                })
                .ToDictionary(f => f.Name, f => f.GetValue(null));
        }

        private static Dictionary<string, object> EventsByName { get; }

        public static T GetEventByName<T>(string name)
        {
            return (T) EventsByName[name];
        }

    }
}