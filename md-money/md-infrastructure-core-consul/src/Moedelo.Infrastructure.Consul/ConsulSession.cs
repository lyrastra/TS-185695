using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Consul.Abstraction;
using Moedelo.Infrastructure.Consul.Extensions;
using Moedelo.Infrastructure.Consul.Models;
using Moedelo.Infrastructure.Consul.Exceptions;

namespace Moedelo.Infrastructure.Consul;

internal sealed class ConsulSession : IConsulSession
{
    /// <summary>
    /// Время жизни сессии в consul
    /// </summary>
    private static TimeSpan ConsulSessionTtl => TimeSpan.FromMinutes(4);
    private static TimeSpan RenewSessionInterval => TimeSpan.FromMinutes(2);
    private static TimeSpan RenewSessionFailureRecoveryInterval => TimeSpan.FromSeconds(10);
    private const int MaxRenewSessionFailureInARowCount = 5;
        
    private readonly HttpClient httpClient;
    private readonly IConsulEndpointConfig consulEndpoint;
    private readonly CancellationTokenSource autoRenewTaskCancellation;
    private readonly Task autoRenewTask;
    private readonly IConsulSessionMonitoring sessionMonitoring;

    private ConsulSession(HttpClient httpClient, IConsulEndpointConfig consulEndpoint, string sessionId, IConsulSessionMonitoring sessionMonitoring, CancellationToken cancellationToken)
    {
        this.httpClient = httpClient;
        this.consulEndpoint = consulEndpoint;
        this.SessionId = sessionId;
        this.sessionMonitoring = sessionMonitoring;
        autoRenewTaskCancellation = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        this.autoRenewTask = RenewSessionPeriodicAsync(autoRenewTaskCancellation.Token);
    }

    public string SessionId { get; }

    public async ValueTask DisposeAsync()
    {
        if (!autoRenewTaskCancellation.IsCancellationRequested)
        {
            autoRenewTaskCancellation.Cancel();
        }

        var timeout = TimeSpan.FromSeconds(1);

        // Ждем завершения фоновой задачи обновления сессии
        await autoRenewTask.WaitIgnoringExceptionAsync(timeout);
        // Удаляем сессию явно
        await DestroySessionAsync().WaitIgnoringExceptionAsync(timeout);

        sessionMonitoring?.OnSessionDisposed(SessionId);
        autoRenewTaskCancellation.Dispose();
    }

    public static async Task<IConsulSession> StartNewAsync(
        HttpClient httpClient,
        IConsulEndpointConfig consulEndpoint,
        bool mustDisposeRequestHttpContent,
        string sessionName,
        IConsulSessionMonitoring sessionMonitoring,
        CancellationToken cancellationToken)
    {
        sessionMonitoring?.OnSessionCreationStarted(sessionName);
        
        try
        {
            var sessionEntry = new SessionEntry
            {
                Name = sessionName,
                TTL = $"{(int)ConsulSessionTtl.TotalSeconds}s",
                Checks = ["serfHealth"]
            };

            var url = consulEndpoint.GetConfig().GetSessionApiMethodUrl("create");
            
            using var content = sessionEntry.CreateHttpJsonStringContentWrapper(mustDisposeRequestHttpContent);

            using var response = await httpClient
                .PutAsync(url, content.HttpContent, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var error = new ConsulSessionCreationException(sessionName, 
                    $"Не удалось создать сессию consul. Код ответа сервера: {response.StatusCode}. Content: {errorContent}");
                sessionMonitoring?.OnSessionCreationFailed(sessionName, error);
                throw error;
            }

            var sessionResponse = await response
                .DeserializeJsonContentAsync<SessionResponse>(cancellationToken)
                .ConfigureAwait(false);

            if (string.IsNullOrEmpty(sessionResponse.ID))
            {
                var error = new ConsulSessionCreationException(sessionName, "Consul вернул пустой ID сессии");
                sessionMonitoring?.OnSessionCreationFailed(sessionName, error);
                throw error;
            }

            var session = new ConsulSession(httpClient, consulEndpoint, sessionResponse.ID, sessionMonitoring, cancellationToken);
            
            // Проверяем, что сессия действительно создалась и валидна
            await session.ValidateSessionAsync(cancellationToken);
            
            sessionMonitoring?.OnSessionCreated(sessionResponse.ID, sessionName);
            return session;
        }
        catch (Exception ex) when (ex.Message != "Consul вернул пустой ID сессии" && !ex.Message.Contains("Не удалось создать сессию consul"))
        {
            sessionMonitoring?.OnSessionCreationFailed(sessionName, ex);
            throw;
        }
    }

    /// <summary>
    /// Проверяет, что сессия существует и валидна в Consul
    /// </summary>
    private async Task ValidateSessionAsync(CancellationToken cancellationToken)
    {
        sessionMonitoring?.OnSessionValidationStarted(SessionId);
        
        try
        {
            var url = consulEndpoint.GetConfig().GetSessionApiMethodUrl($"info/{SessionId}");

            using var response = await httpClient
                .GetAsync(url, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                var error = new ConsulSessionValidationException(SessionId, $"Сессия {SessionId} не найдена в Consul после создания");
                sessionMonitoring?.OnSessionValidationFailed(SessionId, error);
                throw error;
            }

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var error = new ConsulSessionValidationException(SessionId, 
                    $"Ошибка при проверке сессии {SessionId}: {response.StatusCode}. Content: {errorContent}");
                sessionMonitoring?.OnSessionValidationFailed(SessionId, error);
                throw error;
            }

            // Сессия найдена и валидна - HTTP статус успешный
            sessionMonitoring?.OnSessionValidated(SessionId);
        }
        catch (Exception ex) when (!ex.Message.Contains("не найдена") && !ex.Message.Contains("Ошибка при проверке"))
        {
            sessionMonitoring?.OnSessionValidationFailed(SessionId, ex);
            throw;
        }
    }

    private async Task RenewSessionPeriodicAsync(CancellationToken cancellationToken)
    {
        await Task.Yield();
        var errorsInARow = 0;
        
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await RenewSessionAsync(cancellationToken);
                await Task.Delay(RenewSessionInterval, cancellationToken);

                errorsInARow = 0;
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                break;
            }
            catch (ConsulSessionException)
            {
                // Сессия больше не существует - прекращаем попытки обновления
                // Console.WriteLine($"[INFO] Stopping session renewal due to session being invalid or not found: {ex.Message}");
                break;
            }
            catch (Exception) when (errorsInARow++ < MaxRenewSessionFailureInARowCount)
            {
                // несколько ошибок проглатываем и продолжаем попытки обновления
                // Console.WriteLine($"[WARNING] Session renewal error ({errorsInARow}/{maxErrorsInARow}): {ex.Message}");
                await Task.Delay(RenewSessionFailureRecoveryInterval, cancellationToken);
            }
            catch (Exception)
            {
                // Слишком много ошибок подряд - прекращаем попытки обновления
                // Console.WriteLine($"[ERROR] Too many session renewal errors ({errorsInARow}/{maxErrorsInARow}). Stopping renewal: {ex.Message}");
                break;
            }
        }
        
        // Console.WriteLine($"[INFO] Session renewal loop ended for session: {SessionId}");
    }

    private async Task RenewSessionAsync(CancellationToken cancellationToken)
    {
        sessionMonitoring?.OnSessionRenewalStarted(SessionId);
        
        try
        {
            var url = consulEndpoint.GetConfig().GetSessionApiMethodUrl($"renew/{SessionId}");

            using var response = await httpClient
                .PutAsync(url, content: null, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                // Сессия больше не существует
                var error = new ConsulSessionException(SessionId, ConsulSessionProblemType.NotFound, 
                    $"Consul session {SessionId} not found. Session may have been destroyed or expired.");
                sessionMonitoring?.OnSessionRenewalFailed(SessionId, error);
                throw error;
            }
            
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                // Проверяем, что проблема именно в сессии
                var errorContent = await response.Content.ReadAsStringAsync();
                
                if (errorContent.Contains("invalid session") || errorContent.Contains("session not found"))
                {
                    var error = new ConsulSessionException(SessionId, ConsulSessionProblemType.Invalid, 
                        $"Consul session {SessionId} is invalid or not found. Stopping renewal attempts.");
                    sessionMonitoring?.OnSessionRenewalFailed(SessionId, error);
                    throw error;
                }
            }

            response.EnsureSuccessStatusCodeEx();
            sessionMonitoring?.OnSessionRenewed(SessionId);
        }
        catch (ConsulSessionException)
        {
            // Перебрасываем ConsulSessionException как есть
            throw;
        }
        catch (Exception ex)
        {
            sessionMonitoring?.OnSessionRenewalFailed(SessionId, ex);
            throw;
        }
    }

    private async Task DestroySessionAsync()
    {
        var url = consulEndpoint.GetConfig().GetSessionApiMethodUrl($"destroy/{SessionId}");

        using var response = await httpClient
            .PutAsync(url, content: null, CancellationToken.None)
            .ConfigureAwait(false);

        // Игнорируем ошибки при удалении сессии
        if (!response.IsSuccessStatusCode)
        {
            // Сессия могла уже быть удалена
        }
    }
} 