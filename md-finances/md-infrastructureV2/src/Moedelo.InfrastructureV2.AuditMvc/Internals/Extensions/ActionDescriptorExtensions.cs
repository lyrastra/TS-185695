using System.Web.Mvc;

namespace Moedelo.InfrastructureV2.AuditMvc.Internals.Extensions;

internal static class ActionDescriptorExtensions
{
    internal static string GetChildActionAuditTrailSpanName(this ActionDescriptor actionDescriptor)
    {
        var controllerName = actionDescriptor.ControllerDescriptor.ControllerName;
        var actionName = actionDescriptor.ActionName;

        return $"{controllerName}.{actionName}";
    }
}
