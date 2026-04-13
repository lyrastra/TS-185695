# План: синхронное сохранение при ручном редактировании

## Задача

Ручное редактирование одной операции (PUT) — sync-first, без Kafka. Source-of-truth и производные модели записываются синхронно до возврата 200 OK.

Массовые операции — Kafka для распределения по операциям, каждый consumer вызывает тот же Updater.

---

## Что сейчас в коде

### Updater: `PaymentFromCustomerUpdater.UpdateOperationAsync()` (строки 82-105)

```csharp
await apiClient.UpdateAsync(request);                        // ① SYNC → SQL UPDATE
await writer.WriteUpdatedEventAsync(request);                // ② KAFKA EVENT — центральный бизнес-триггер
await customTaxPostingsSaver.OverwriteAsync(                 // ③ KAFKA COMMANDS (только ByHand)
    PaymentFromCustomerMapper.MapToCustomTaxPostingsOverwriteRequest(request));
```

### Шаг ① — sync, source-of-truth

`apiClient.UpdateAsync()` → HTTP PUT → PaymentOrders API → `PaymentOrderDao.UpdateAsync()` → `Update.sql:27`: `SET TaxationSystemType = @TaxationSystemType`.

### Шаг ② — Kafka event: НЕ "только аудит", а центральный бизнес-триггер

`writer.WriteUpdatedEventAsync()` (`PaymentFromCustomerEventWriter.cs:47-53`) публикует `PaymentFromCustomerUpdated`. Перед публикацией устанавливает providing state:

```csharp
var eventData = PaymentFromCustomerMapper.MapToUpdatedMessage(request);
eventData.ProvidingStateId = await providingStateSetter.SetStateAsync(request.DocumentBaseId);
await topicWriter.WriteEventDataAsync(eventDefinition);
```

Три consumer'а подписаны:

**Providing consumer** (`PaymentFromCustomerHostedService.cs:70-73`) → `provider.ProvideAsync()`. Это главный — запускает полный pipeline:

1. Проверка существования операции (`paymentFromCustomerApiClient.IsExistsAsync()`)
2. Загрузка BaseDocuments (`baseDocumentReader.GetByIdsAsync()`)
3. Загрузка контрагента (`kontragentReader.GetByIdAsync()`)
4. Загрузка контракта (`contractReader.GetByBaseIdAsync()` / `GetMainAsync()`)
5. Параллельная загрузка накладных/актов/УПД (`waybillReader`, `statementReader`, `updReader`)
6. **Создание бухсправок и связей** (`CreateAccStatementsAndLinksAsync()` → `linksCreator.OverwriteAsync()`) — удаление старых связей, создание новых (bills, documents, invoices, reserve, contract, accounting statements)
7. **Обновление статусов счетов** (`billStatusUpdater.UpdateStatusesAsync()`)
8. **Генерация бух. проводок** (`accPostingsProvider.ProvideAsync()`)
9. **Генерация налоговых проводок** (`taxPostingsProvider.ProvideAsync()`) — ветвление по СНО, запись через `ITaxPostingsUsnClient`/`ITaxPostingsPsnClient`
10. Очистка providing state (`providingStateSetter.UnsetStateAsync()`)

**LinkedDocuments consumer** (`FromMoney_PaymentFromCustomerHostedService.cs:54-65`) — обновляет `BaseDocument`.

**ChangeLog consumer** — аудит-лог.

### Шаг ③ — CustomTaxPostingsSaver: только для ByHand

`CustomTaxPostingsSaver.cs:57`: `if (ProvidePostingType != ByHand) return;`

Для автоматических проводок (`Auto`) — ранний return, ничего не делает. Проводки генерируются через Providing consumer (шаг ②, пункт 9).

Массовая смена СНО (`PaymentFromCustomerTaxationSystemUpdater.cs:47`) ставит `Auto`:
```csharp
request.TaxPostings = new TaxPostingsData { ProvidePostingType = ProvidePostingType.Auto };
await updater.UpdateAsync(request);
```
→ `CustomTaxPostingsSaver` ничего не делает → проводки появляются только через Providing consumer.

### Текущий контракт PUT

В `PaymentFromCustomerUpdater.cs` нет try/catch вокруг шагов ② и ③. Ошибка в Kafka publish или CustomTaxPostingsSaver пробрасывается в контроллер → пользователь видит ошибку, не 200 OK.

### Providing — нет sync HTTP endpoint

В `/md-money/src/apps/providing/Moedelo.Money.Providing.Api/Controllers/` есть только endpoints для генерации DTO проводок, нет endpoint для полного `ProvideAsync()` pipeline. Pipeline запускается исключительно через Kafka events.

---

## Два сценария проводок: Auto vs ByHand

| | Auto (по умолчанию) | ByHand (ручные) |
|---|---|---|
| Кто генерирует | Providing consumer → `taxPostingsProvider.ProvideAsync()` | `CustomTaxPostingsSaver.OverwriteAsync()` |
| Триггер | Kafka event `PaymentFromCustomerUpdated` | Прямой вызов из Updater |
| Транспорт | Sync HTTP-клиенты внутри Providing (`ITaxPostingsUsnClient.SaveAsync()`) | Kafka command writers (`usnTaxPostingsCommandWriter.WriteOverwriteAsync()`) |
| Sync/Async с т.з. PUT | Async (Kafka → consumer → sync HTTP) | Async (Kafka commands) |
| При массовой смене | Этот путь (`Auto`) | Не используется |

Оба пути в итоге записывают в те же таблицы (`PostingForTax`/`PostingForPatent`) через те же HTTP-клиенты. Но между ними — разная доменная логика (генерация vs ручные данные).

---

## Что нужно сделать

### Главное: sync Providing facade

Сейчас нет sync HTTP endpoint для `ProvideAsync()`. Нужно создать его в Providing API. Это не "вызвать пару HTTP-клиентов напрямую" — это полный pipeline из 10 шагов (бухсправки, связи, статусы счетов, бух. проводки, налоговые проводки, providing state).

**Подход:**

1. В `Moedelo.Money.Providing.Api` добавить sync HTTP endpoint:
```csharp
[HttpPost("Incoming/PaymentFromCustomer/{documentBaseId}/Provide")]
public async Task<IActionResult> ProvideAsync(long documentBaseId, PaymentFromCustomerProvideRequest request)
{
    await provider.ProvideAsync(request);  // тот же provider что вызывает Kafka consumer
    return Ok();
}
```

2. В `Moedelo.Money.Business` вызывать этот endpoint через HTTP-клиент вместо Kafka event:
```csharp
// Вместо: await writer.WriteUpdatedEventAsync(request);
await providingClient.ProvideAsync(request);  // sync HTTP к Providing API
```

**Почему это работает:** `PaymentFromCustomerProvider.ProvideAsync()` уже содержит всю бизнес-логику. Kafka consumer просто маппит event → request и вызывает его. Sync endpoint делает то же самое, но синхронно.

**Что переиспользуется:** весь `PaymentFromCustomerProvider` с его зависимостями (`linksCreator`, `accPostingsProvider`, `taxPostingsProvider`, `billStatusUpdater`). Никакого дублирования логики.

3. Kafka consumer оставить — он нужен для массовой смены и других async-сценариев. Но для ручного PUT — sync HTTP вместо Kafka.

### CustomTaxPostingsSaver: два режима записи

Для ByHand проводок `CustomTaxPostingsSaver` использует Kafka command writers. Нужно добавить sync режим через те же HTTP-клиенты:

```csharp
interface ITaxPostingsWriter
{
    Task OverwriteUsnAsync(long documentBaseId, ...);
    Task OverwritePatentAsync(long documentBaseId, ...);
    Task OverwriteOsnoAsync(long documentBaseId, ...);
    Task OverwriteIpOsnoAsync(long documentBaseId, ...);
}

class SyncTaxPostingsWriter : ITaxPostingsWriter  // через ITaxPostingsUsnClient и т.д.
class KafkaTaxPostingsWriter : ITaxPostingsWriter  // текущие Kafka command writers
```

Бизнес-логика (ветвление по СНО, providing state, удаление патентных проводок) остаётся в `CustomTaxPostingsSaver`. Меняется только writer.

HTTP-клиенты (`ITaxPostingsUsnClient`, `ITaxPostingsPsnClient`, `ITaxPostingsOsnoClient`) уже доступны в Money.Business:
- `Money.Business.csproj` строка 45: `TaxPostings.ApiClient.Abstractions` (интерфейсы)
- `Money.Api.csproj` строка 85: `TaxPostings.ApiClient` (реализации)

**Обязательно:** parity-тесты — прогнать sync и Kafka пути на одних данных, сравнить результат (проводки, tax statuses, providing states).

### Updater: новый flow

```csharp
private async Task UpdateOperationAsync(PaymentFromCustomerSaveRequest request)
{
    if (request.TaxationSystemType == null)
        request.TaxationSystemType = await taxationSystemTypeReader.GetDefaultByYearAsync(request.Date.Year);

    // ① sync: source-of-truth (без изменений)
    await apiClient.UpdateAsync(request);

    // ② sync: Providing pipeline (ВМЕСТО Kafka event)
    //    Тот же PaymentFromCustomerProvider.ProvideAsync() через sync HTTP endpoint
    await providingClient.ProvideAsync(MapToProvideRequest(request));

    // ③ sync: ByHand проводки (через SyncTaxPostingsWriter вместо Kafka commands)
    await customTaxPostingsSaver.OverwriteSyncAsync(
        PaymentFromCustomerMapper.MapToCustomTaxPostingsOverwriteRequest(request));

    // ④ Kafka: только аудит (ChangeLog) — нет sync HTTP-клиента, не влияет на UI
    await changeLogWriter.WriteAsync(...);
}
```

### Контракт PUT: оставить strict-success

Текущий контракт: ошибка на любом шаге → ошибка для пользователя. Не менять. Если sync HTTP к Providing упал — пользователь видит ошибку, не 200 OK. Это честнее чем partial-success.

### SetReserve: sync через Providing facade

Добавить sync endpoint в Providing API для `UpdateReserveAsync()`:
```csharp
[HttpPost("Incoming/PaymentFromCustomer/{documentBaseId}/SetReserve")]
public async Task<IActionResult> SetReserveAsync(long documentBaseId, SetReserveRequest request)
{
    await provider.UpdateReserveAsync(request);  // тот же метод что вызывает Kafka consumer
    return Ok();
}
```

Логика остаётся в `PaymentLinksCreator.UpdateReserveAsync()` — никакого дублирования.

### Массовая смена: без изменений

`PaymentFromCustomerTaxationSystemUpdater.cs:48` вызывает `updater.UpdateAsync()`. Updater теперь sync внутри → каждая операция обрабатывается полностью внутри consumer'а.

**Нагрузка:** Updater тяжелее (sync HTTP к Providing вместо Kafka publish). Consumer setting: `PaymentOrderChangeTaxationSystemCommandConsumerCount` (не `MoneyEventConsumerCount` — это другой consumer). Мониторить latency и lag.

Фронт: заменить `setTimeout(10000)` (`MassChangeTaxSystemStore.js:112`) на polling sync-объекта.

### PaymentToSupplier: добавить TaxationSystemType + e2e тест

На все слои по аналогии с PaymentFromCustomer. SQL готов:
- `Update.sql:27`: `TaxationSystemType = @TaxationSystemType`
- `Select.sql:28`: `TaxationSystemType`

Обязательно: e2e контрактный тест по всей цепочке.

### Fallback null→default в Reader

`PaymentFromCustomerReader.cs:44-47` — убрать или добавить флаг `isTaxationSystemTypeDefault`.

---

## Затрагиваемые репозитории

- `md-money`
- `md-finances`

---

## Риски и митигация

| Риск | Митигация |
|---|---|
| Sync Providing facade увеличивает latency PUT | Providing pipeline делает ~10 HTTP-вызовов внутри. Мониторить p99. При необходимости — параллелить независимые шаги (уже частично сделано: `Task.WhenAll` для waybills/statements/upds) |
| Parity sync vs Kafka | Parity-тесты: прогнать оба пути на одних данных, сравнить проводки, статусы, связи |
| Нагрузка при массовой смене | Consumer setting `PaymentOrderChangeTaxationSystemCommandConsumerCount`. Мониторить, настраивать pool |
| Потеря поля PaymentToSupplier | e2e контрактный тест |

---

## Итог

| Инициатор | Действие | Было | Стало |
|---|---|---|---|
| 👤 PUT | Source-of-truth | Sync | Sync (без изменений) |
| 👤 PUT | Providing (связи, бухсправки, статусы, бух.проводки, налог.проводки) | Kafka event → Providing consumer | Sync HTTP → тот же `PaymentFromCustomerProvider.ProvideAsync()` через новый endpoint |
| 👤 PUT | ByHand проводки | Kafka commands | Sync через `ITaxPostingsWriter` (те же HTTP-клиенты) |
| 👤 PUT | LinkedDocuments | Kafka → consumer → DAO | Покрывается Providing facade (шаг 6 в pipeline — `linksCreator.OverwriteAsync()`) |
| 👤 PUT | Аудит | Kafka → ChangeLog | Kafka (оставить — нет sync клиента, не влияет на UI) |
| 👤 PUT | Контракт ответа | Strict-success | Strict-success (не менять) |
| 👤 SetReserve | Резерв | Только Kafka | Sync HTTP → Providing facade → `PaymentLinksCreator.UpdateReserveAsync()` |
| 👤 Массовая смена | N операций | Kafka fan-out → Updater | Kafka fan-out → тот же Updater (sync внутри) |
| — | PaymentToSupplier | Поле отсутствует | Добавить + e2e тест |
| — | Fallback | Молча подставляет | Убрать или пометить |
