using System;
using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.Common.Enums.Attributes;

public class PurseOperationTypeAttribute(PurseOperationType operationType) : Attribute
{
    public PurseOperationType OperationType { get; private set; } = operationType;
}