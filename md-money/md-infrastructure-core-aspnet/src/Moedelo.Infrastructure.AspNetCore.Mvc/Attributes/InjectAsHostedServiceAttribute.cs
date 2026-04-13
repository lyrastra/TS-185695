using System;

namespace Moedelo.Infrastructure.AspNetCore.Mvc.Attributes;

/// <summary>
/// Помечает класс, чтобы он был автоматически зарегистрирован как <see cref="Microsoft.Extensions.Hosting.IHostedService"/>
/// при вызове <see cref="Extensions.ServiceCollectionExtensions.AddHostedServicesFromAssembly(Microsoft.Extensions.DependencyInjection.IServiceCollection,System.Reflection.Assembly)"/>.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
// ReSharper disable once ClassNeverInstantiated.Global
public class InjectAsHostedServiceAttribute : Attribute;
