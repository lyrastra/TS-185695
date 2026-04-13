using System;
// ReSharper disable InconsistentNaming

namespace Moedelo.Infrastructure.Consul.Abstraction.Models;

/// <summary>
/// Запрос на регистрацию сервиса с авторегистрацией TTL-проверки
/// <a href="https://developer.hashicorp.com/consul/api-docs/agent/service">Документация на developer.hashicorp.com</a> 
/// </summary>
public class AgentServiceRegistration
{
    /// <summary>
    /// Логическое имя сервиса
    /// Общее для всех экземпляров сервиса 
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Уникальный идентификатор экземпляра сервиса
    /// </summary>
    public string ID { get; set; }
    /// <summary>
    /// Список тэгов сервиса
    /// </summary>
    public string[] Tags { get; set; }
    /// <summary>
    /// Адрес, на котором развёрнут экземпляр сервиса
    /// </summary>
    public string Address { get; set; }
    /// <summary>
    /// Порт, который использует экземпляр сервиса
    /// </summary>
    public int Port { get; set; }
    /// <summary>
    /// Настройки механизма проверки жизнеспособности сервиса
    /// </summary>
    public TtlCheckRegistration Check { get; set; }

    public class TtlCheckRegistration
    {
        private const string WarningStatus = "warning";
        private static string InitialStatus => WarningStatus;
        public string Name { get; set; }
        public string Notes { get; set; }
        public TimeSpan? TTL { get; set; }
        public string Status => InitialStatus;
        public TimeSpan? DeregisterCriticalServiceAfter { get; set; }
    }
}
