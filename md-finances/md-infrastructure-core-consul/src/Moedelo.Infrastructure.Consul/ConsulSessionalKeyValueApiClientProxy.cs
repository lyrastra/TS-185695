using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Consul.Abstraction;
using Moedelo.Infrastructure.Consul.Exceptions;
using Moedelo.Infrastructure.Consul.Extensions;
using Moedelo.Infrastructure.Consul.Internals;
using Moedelo.Infrastructure.Consul.Models;

namespace Moedelo.Infrastructure.Consul;

internal sealed class ConsulSessionalKeyValueApiClientProxy : IConsulSessionalKeyValueApi
{
    private readonly HttpClient httpClientRef;
    private readonly IConsulEndpointConfig consulEndpoint;
    /// <summary>
    /// Флаг, указывающий нужно ли автоматически освобождать ресурсы HttpContent после использования.
    /// Используется в HttpContentWrapper для контроля жизненного цикла объектов HttpContent.
    /// </summary>
    private readonly bool mustDisposeRequestHttpContent;
    private readonly ResettableLazy<IConsulSession> sessionLazy;
    private readonly IConsulSessionNamingStrategy sessionNamingStrategy;
    private readonly IConsulSessionMonitoring consulSessionMonitoring;

    internal ConsulSessionalKeyValueApiClientProxy(HttpClient httpClientRef,
        IConsulEndpointConfig consulEndpoint,
        bool mustDisposeRequestHttpContent,
        IConsulSessionNamingStrategy sessionNamingStrategy,
        IConsulSessionMonitoring consulSessionMonitoring)
    {
        this.httpClientRef = httpClientRef;
        this.consulEndpoint = consulEndpoint;
        this.mustDisposeRequestHttpContent = mustDisposeRequestHttpContent;
        this.sessionNamingStrategy = sessionNamingStrategy;
        this.consulSessionMonitoring = consulSessionMonitoring;
        this.sessionLazy = new ResettableLazy<IConsulSession>(CreateSession);
    }

    public string ConsulSessionId => sessionLazy.Value.SessionId;

    /// <summary>
    /// Захватывает ключ в Consul KV с использованием сессии.
    /// 
    /// Поведение Consul KV Acquire API (PUT /v1/kv/{key}?acquire={session}):
    /// 
    /// Сценарии и ответы:
    /// - Валидная сессия + свободный ключ: HTTP 200 + true (захват успешен)
    /// - Валидная сессия + уже захваченный той же сессией: HTTP 200 + true (перезахват разрешен)
    /// - Валидная сессия + уже захваченный другой сессией: HTTP 200 + false (ключ занят)
    /// - Недействительная сессия (неправильный ID в URL): HTTP 500 + "raft apply failed: invalid session"
    /// - Удаленная/истекшая сессия: HTTP 500 + "raft apply failed: invalid session"
    /// 
    /// Ключевые выводы:
    /// - Недействительные сессии возвращают HTTP 500 с ошибкой "raft apply failed: invalid session"
    /// - Ключ уже захвачен возвращает HTTP 200 с false в теле
    /// - Consul позволяет "перезахватывать" ключ той же сессией
    /// - Session ID должен передаваться в URL параметре, а не в теле запроса
    /// - Consul может возвращать как простой boolean (true/false), так и JSON объект
    /// 
    /// Обработка ошибок:
    /// - При HTTP 500 проверяем содержимое ответа на наличие "invalid session"
    /// - Если проблема в сессии - освобождаем старую и создаем новую при следующем вызове
    /// - Если другая ошибка сервера - выбрасываем исключение с деталями
    /// 
    /// Исследование проведено на Consul v1.x, localhost:8500
    /// </summary>
    public async Task<bool> AcquireKeyValueAsync(string keyPath, string value, CancellationToken cancellationToken)
    {
        var session = sessionLazy.Value;
        
        consulSessionMonitoring?.OnKeyAcquisitionStarted(session.SessionId, keyPath);
        
        try
        {
            var url = consulEndpoint.GetConfig().GetKeyUrl(keyPath, $"acquire={session.SessionId}");
            
            // Создаем HttpContentWrapper с учетом mustDisposeRequestHttpContent
            using var content = value.CreateHttpStringContentWrapper(mustDisposeRequestHttpContent);

            using var response = await httpClientRef
                .PutAsync(url, content.HttpContent, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                // Проверяем, что проблема именно в сессии
                var errorContent = await response.Content.ReadAsStringAsync();
                
                if (errorContent.Contains("invalid session"))
                {
                    // Сессия недействительна - освобождаем и создаем новую
                    await DisposeSessionAsync();
                    
                    var error = new ConsulKeyAcquisitionException(session.SessionId, keyPath,
                        "Consul-сессия недействительна. При следующем обращении будет сделана попытка открыть новую");
                    consulSessionMonitoring?.OnKeyAcquisitionFailed(session.SessionId, keyPath, error);
                    throw error;
                }
                
                // Другая ошибка сервера - просто выбрасываем исключение
                var serverError = new ConsulKeyAcquisitionException(session.SessionId, keyPath, $"Consul server error: {errorContent}");
                consulSessionMonitoring?.OnKeyAcquisitionFailed(session.SessionId, keyPath, serverError);
                throw serverError;
            }

            response.EnsureSuccessStatusCodeEx();
            
            // Consul может возвращать как boolean (true/false), так и JSON объект
            var responseContent = await response.Content.ReadAsStringAsync();
            
            bool result;
            if (responseContent.Trim() == "true" || responseContent.Trim() == "false")
            {
                // Consul вернул простой boolean
                result = bool.Parse(responseContent.Trim());
            }
            else
            {
                // Consul вернул JSON объект
                var keyValueResponse = await response
                    .DeserializeJsonContentAsync<KeyValueResponse>(cancellationToken)
                    .ConfigureAwait(false);
                result = keyValueResponse.Success;
            }

            consulSessionMonitoring?.OnKeyAcquisitionCompleted(session.SessionId, keyPath, result);
            return result;
        }
        catch (Exception ex) when (!(ex is ConsulKeyAcquisitionException))
        {
            consulSessionMonitoring?.OnKeyAcquisitionFailed(session.SessionId, keyPath, ex);
            throw;
        }
    }

    /// <summary>
    /// Освобождает ранее захваченный ключ в Consul KV.
    /// 
    /// Поведение Consul KV Release API (PUT /v1/kv/{key}?release={session}):
    /// 
    /// Сценарии и ответы:
    /// - Валидная сессия + ключ захвачен этой сессией: HTTP 200 + true (освобождение успешно)
    /// - Валидная сессия + ключ не захвачен: HTTP 200 + false (нечего освобождать)
    /// - Валидная сессия + ключ захвачен другой сессией: HTTP 200 + false (нельзя освободить чужой ключ)
    /// - Недействительная сессия: HTTP 500 + "raft apply failed: invalid session"
    /// 
    /// Ключевые выводы:
    /// - Release возвращает true только если ключ был успешно освобожден
    /// - Release возвращает false если ключ не был захвачен или захвачен другой сессией
    /// - Session ID должен передаваться в URL параметре, а не в теле запроса
    /// 
    /// Оптимизация: если сессия не была создана (IsValueCreated = false),
    /// значит AcquireKeyValueAsync не вызывался, поэтому нет смысла
    /// пытаться освободить ключ, который не был захвачен.
    /// 
    /// Исследование проведено на Consul v1.x, localhost:8500
    /// </summary>
    public async Task ReleaseAcquiredKeyValueAsync(string keyPath, CancellationToken cancellationToken)
    {
        // Проверяем, была ли сессия создана
        if (!sessionLazy.IsValueCreated)
        {
            // Сессия не была создана, значит ключ не был захвачен
            return;
        }

        var session = sessionLazy.Value;
        
        consulSessionMonitoring?.OnKeyReleaseStarted(session.SessionId, keyPath);
        
        try
        {
            var url = consulEndpoint.GetConfig().GetKeyUrl(keyPath, $"release={session.SessionId}");
            
            // Для release отправляем пустое тело запроса с учетом mustDisposeRequestHttpContent
            using var content = "".CreateHttpStringContentWrapper(mustDisposeRequestHttpContent);

            using var response = await httpClientRef
                .PutAsync(url, content.HttpContent, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCodeEx();
            
            consulSessionMonitoring?.OnKeyReleaseCompleted(session.SessionId, keyPath);
        }
        catch (Exception ex)
        {
            consulSessionMonitoring?.OnKeyReleaseFailed(session.SessionId, keyPath, ex);
            throw;
        }
    }

    private IConsulSession CreateSession()
    {
        try
        {
            var sessionName = sessionNamingStrategy.GenerateSessionName();
            
            var session = ConsulSession.StartNewAsync(httpClientRef, consulEndpoint, mustDisposeRequestHttpContent, sessionName, consulSessionMonitoring, CancellationToken.None).Result;
            
            return session;
        }
        catch (Exception ex)
        {
            if (ex.InnerException != null)
            {
                // Логирование внутреннего исключения
            }
            throw;
        }
    }

    private async Task DisposeSessionAsync()
    {
        if (sessionLazy.IsValueCreated)
        {
            await sessionLazy.Value.DisposeAsync();
            sessionLazy.Reset(); // Сбрасываем, чтобы создать новую сессию при следующем обращении
        }
    }

    /// <summary>
    /// Освобождает ресурсы, включая сессию Consul.
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await DisposeSessionAsync().WaitIgnoringExceptionAsync(TimeSpan.FromSeconds(1));
    }
}