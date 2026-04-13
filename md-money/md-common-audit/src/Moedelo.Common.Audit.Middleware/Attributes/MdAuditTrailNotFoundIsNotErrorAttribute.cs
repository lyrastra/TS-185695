using System;
using System.Net;

namespace Moedelo.Common.Audit.Middleware.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class MdAuditTrailNotFoundIsNotErrorAttribute : MdAuditTrailIsNotErrorAttribute
{
    public MdAuditTrailNotFoundIsNotErrorAttribute() : base(HttpStatusCode.NotFound)
    {
    }
}
