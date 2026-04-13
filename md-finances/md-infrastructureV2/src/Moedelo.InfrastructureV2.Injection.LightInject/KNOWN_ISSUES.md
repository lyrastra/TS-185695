# Известные проблемы LightInject

## Дубликаты при резолвинге через IEnumerable<T>

### Проблема

При регистрации класса с атрибутом `[InjectAsSingleton(typeof(IBase), typeof(IDerived))]`, где `IDerived : IBase`, при резолвинге через `IEnumerable<IBase>` LightInject возвращает несколько экземпляров одного и того же класса (по одному для каждой регистрации: самого типа, базового интерфейса и производного интерфейса).

**Пример:**
```csharp
[InjectAsSingleton(typeof(IWebAppConfigCheck), typeof(IRedisCheck))]
internal sealed class RedisCheck : IRedisCheck { }

// При резолвинге через IEnumerable<IWebAppConfigCheck> получается 3 экземпляра RedisCheck
```

### Решение

**Проблема не может быть решена на уровне LightInject контейнера.** Это ограничение библиотеки LightInject.

**Рекомендация:** При использовании `IEnumerable<T>` для резолвинга сервисов, которые могут быть зарегистрированы под несколькими интерфейсами, необходимо выполнять **дедупликацию по месту использования**.

### Для Singleton

```csharp
public class WebAppConfigChecker
{
    public WebAppConfigChecker(IEnumerable<IWebAppConfigCheck> checks)
    {
        // Для Singleton все экземпляры - это одна и та же ссылка, поэтому Distinct() достаточно
        var uniqueChecks = checks.Distinct().ToArray();
        // Используем uniqueChecks
    }
}
```

### Для Transient/PerWebRequest

```csharp
public class SomeService
{
    public SomeService(IEnumerable<ISomeInterface> services)
    {
        // Группируем по типу реализации и берем первый из каждой группы
        var uniqueServices = services
            .GroupBy(service => service.GetType())
            .Select(group => group.First())
            .ToArray();
        // Используем uniqueServices
    }
}
```

### Когда применять

Дедупликация необходима, когда:
- Класс регистрируется под несколькими интерфейсами через атрибут `[InjectAsSingleton(typeof(IBase), typeof(IDerived))]`
- Один из интерфейсов наследуется от другого (`IDerived : IBase`)
- Резолвинг происходит через `IEnumerable<T>` базового интерфейса
