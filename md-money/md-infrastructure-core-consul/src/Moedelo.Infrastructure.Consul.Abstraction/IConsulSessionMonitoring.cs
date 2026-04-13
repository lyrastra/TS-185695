using System;

namespace Moedelo.Infrastructure.Consul.Abstraction;

/// <summary>
/// Интерфейс для мониторинга работы с сессиями Consul
/// </summary>
public interface IConsulSessionMonitoring
{
    /// <summary>
    /// Логирование попытки создания сессии
    /// </summary>
    /// <param name="sessionName">Имя создаваемой сессии</param>
    void OnSessionCreationStarted(string sessionName);
    
    /// <summary>
    /// Логирование успешного создания сессии
    /// </summary>
    /// <param name="sessionId">ID созданной сессии</param>
    /// <param name="sessionName">Имя сессии</param>
    void OnSessionCreated(string sessionId, string sessionName);
    
    /// <summary>
    /// Логирование ошибки создания сессии
    /// </summary>
    /// <param name="sessionName">Имя сессии</param>
    /// <param name="error">Ошибка</param>
    void OnSessionCreationFailed(string sessionName, Exception error);
    
    /// <summary>
    /// Логирование попытки валидации сессии
    /// </summary>
    /// <param name="sessionId">ID сессии</param>
    void OnSessionValidationStarted(string sessionId);
    
    /// <summary>
    /// Логирование успешной валидации сессии
    /// </summary>
    /// <param name="sessionId">ID сессии</param>
    void OnSessionValidated(string sessionId);
    
    /// <summary>
    /// Логирование ошибки валидации сессии
    /// </summary>
    /// <param name="sessionId">ID сессии</param>
    /// <param name="error">Ошибка</param>
    void OnSessionValidationFailed(string sessionId, Exception error);
    
    /// <summary>
    /// Логирование попытки обновления сессии
    /// </summary>
    /// <param name="sessionId">ID сессии</param>
    void OnSessionRenewalStarted(string sessionId);
    
    /// <summary>
    /// Логирование успешного обновления сессии
    /// </summary>
    /// <param name="sessionId">ID сессии</param>
    void OnSessionRenewed(string sessionId);
    
    /// <summary>
    /// Логирование ошибки обновления сессии
    /// </summary>
    /// <param name="sessionId">ID сессии</param>
    /// <param name="error">Ошибка</param>
    void OnSessionRenewalFailed(string sessionId, Exception error);
    
    /// <summary>
    /// Логирование попытки захвата ключа
    /// </summary>
    /// <param name="sessionId">ID сессии</param>
    /// <param name="keyPath">Путь к ключу</param>
    void OnKeyAcquisitionStarted(string sessionId, string keyPath);
    
    /// <summary>
    /// Логирование результата захвата ключа
    /// </summary>
    /// <param name="sessionId">ID сессии</param>
    /// <param name="keyPath">Путь к ключу</param>
    /// <param name="success">Успешность захвата</param>
    void OnKeyAcquisitionCompleted(string sessionId, string keyPath, bool success);
    
    /// <summary>
    /// Логирование ошибки захвата ключа
    /// </summary>
    /// <param name="sessionId">ID сессии</param>
    /// <param name="keyPath">Путь к ключу</param>
    /// <param name="error">Ошибка</param>
    void OnKeyAcquisitionFailed(string sessionId, string keyPath, Exception error);
    
    /// <summary>
    /// Логирование попытки освобождения ключа
    /// </summary>
    /// <param name="sessionId">ID сессии</param>
    /// <param name="keyPath">Путь к ключу</param>
    void OnKeyReleaseStarted(string sessionId, string keyPath);
    
    /// <summary>
    /// Логирование успешного освобождения ключа
    /// </summary>
    /// <param name="sessionId">ID сессии</param>
    /// <param name="keyPath">Путь к ключу</param>
    void OnKeyReleaseCompleted(string sessionId, string keyPath);
    
    /// <summary>
    /// Логирование ошибки освобождения ключа
    /// </summary>
    /// <param name="sessionId">ID сессии</param>
    /// <param name="keyPath">Путь к ключу</param>
    /// <param name="error">Ошибка</param>
    void OnKeyReleaseFailed(string sessionId, string keyPath, Exception error);
    
    /// <summary>
    /// Логирование освобождения сессии
    /// </summary>
    /// <param name="sessionId">ID сессии</param>
    void OnSessionDisposed(string sessionId);
}
