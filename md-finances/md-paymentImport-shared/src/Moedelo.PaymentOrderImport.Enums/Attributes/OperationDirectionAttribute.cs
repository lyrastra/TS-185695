using System;
using System.Collections.Generic;
using System.Text;

namespace Moedelo.PaymentOrderImport.Enums.Attributes
{
    public class OperationDirectionAttribute : Attribute
    {
        public OperationDirectionAttribute(OperationDirection direction)
        {
            Direction = direction;
        }

        public OperationDirection Direction { get; set; }
    }
}
