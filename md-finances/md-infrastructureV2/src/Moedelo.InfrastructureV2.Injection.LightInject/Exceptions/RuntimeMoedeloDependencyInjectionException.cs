using System;

namespace Moedelo.InfrastructureV2.Injection.Lightinject.Exceptions
{
    public sealed class RuntimeMoedeloDependencyInjectionException : Exception
    {
        public RuntimeMoedeloDependencyInjectionException(
            Type implementationType, Type injectionTargetService)
            : base($"Для типа {implementationType.FullName} в качестве цели указан тип {injectionTargetService.FullName}," +
                   $" хотя эти типы не связаны отношением реализации. Сборка: {implementationType.Assembly.FullName}")
        {
        }
    }
}
