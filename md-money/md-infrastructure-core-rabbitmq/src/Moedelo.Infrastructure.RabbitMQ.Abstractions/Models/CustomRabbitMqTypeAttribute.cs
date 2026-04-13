using System;
using System.Collections.Generic;
using System.Text;

namespace Moedelo.Infrastructure.RabbitMQ.Abstractions.Models
{
    public class CustomRabbitMqTypeAttribute : Attribute
    {
        public string TypeName { get; set; }

        public CustomRabbitMqTypeAttribute(string typeName)
        {
            TypeName = typeName;
        }
    }
}
