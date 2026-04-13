using System;
using System.Diagnostics;
using System.Net;

namespace Moedelo.Common.Audit.Middleware.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class MdAuditTrailIsNotErrorAttribute : Attribute
{
    public MdAuditTrailIsNotErrorAttribute(params HttpStatusCode[] statusCodes)
    {
        Debug.Assert(statusCodes.Length > 0, "Должен быть указан хотя бы один код");
        StatusCodes = statusCodes;
    }

    public HttpStatusCode[] StatusCodes { get; } 
}