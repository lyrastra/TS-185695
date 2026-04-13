# Конфигурация AuditTrail Middleware

## Обзор

Документация по настройке и конфигурации AuditTrail middleware для ASP.NET Core приложений.

## Исключения

Система поддерживает исключение URL-ов из трейсинга через регулярные выражения:

- `ExcludeUrlPatterns` - полное исключение из трейсинга
- `ExcludeRequestBodyUrlPatterns` - исключение тела запроса
- `ExcludeResponseBodyUrlPatterns` - исключение тела ответа

## Настройки

- `TreatValidationExceptionAsError` - обработка ValidationException как ошибки

## Регистрация middleware

```csharp
// В Program.cs или Startup.cs
app.UseAuditApiHandlerTrace();
```

## Настройка исключений

```csharp
var settings = new AuditApiHandlerTraceMiddlewareSettings
{
    ExcludeUrlPatterns = new[] { "/health", "/metrics" },
    ExcludeRequestBodyUrlPatterns = new[] { "/api/upload" },
    ExcludeResponseBodyUrlPatterns = new[] { "/api/download" },
    TreatValidationExceptionAsError = true
};

app.UseAuditApiHandlerTrace(settings);
```

## Порядок регистрации

AuditTrail middleware должен быть зарегистрирован до других middleware, которые могут влиять на обработку запросов.

## Troubleshooting

### Middleware не работает

1. Удостоверьтесь, что auditTrail включен в окружении, где происходит выполнение приложения
2. Проверьте регистрацию middleware в правильном порядке
3. Убедитесь, что URL не исключен из трейсинга
4. Проверьте настройки исключений

### Производительность

- Исключайте из трейсинга health checks и метрики
- Настройте исключения для больших файлов
- Используйте регулярные выражения для группового исключения
