using System;
using LightInject;

namespace Moedelo.InfrastructureV2.Injection.Lightinject
{
    /// <summary>
    /// настройка внедрения типа
    /// </summary>
    internal sealed class TypeInjectionSetup
    {
        /// <summary>
        /// тип, внедряемый для реализации целевых типов
        /// </summary>
        public Type Implementation { get; set; }
        /// <summary>
        /// целевые типы внедрения
        /// </summary>
        public Type[] InjectionTargetServices { get; set; }
        /// <summary>
        /// тип внедрения
        /// </summary>
        public ILifetime Lifetime { get; set; }
    }
}
