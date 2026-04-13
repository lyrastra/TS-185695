# План: синхронное сохранение при ручном редактировании

## Задача

Ручное редактирование одной операции (PUT) — sync-first, без Kafka. Разделение:
- **Source-of-truth** (`Accounting_PaymentOrder.TaxationSystemType`) — записывается гарантированно (sync HTTP → SQL).
- **Производные модели** (проводки в TaxPostings, BaseDocument в LinkedDocuments) — best-effort sync. Если HTTP к TaxPostings/LinkedDocuments упал после записи source-of-truth — нужна стратегия (retry / partial success / компенсация).

Массовые операции — Kafka для распределения по операциям, каждый consumer вызывает тот же Updater.

## Два инициатора — два flow

### Flow 1: Пользователь редактирует одну операцию

```
👤 Пользователь нажимает "Сохранить"
  ↓
PUT /PaymentFromCustomer/{id} → Updater (всё sync)
  ↓
① apiClient.UpdateAsync()              → sync HTTP → SQL UPDATE Accounting_PaymentOrder
② taxPostingsSaver.OverwriteAsync()    → sync (та же бизнес-логика, sync режим клиентов)
③ baseDocumentsClient.UpdateAsync()    → sync HTTP → LinkedDocuments
④ changeLogWriter.WriteAsync()         → Kafka (только аудит, не влияет на UI)
  ↓
200 OK → данные записаны во все хранилища
```

### Flow 2: Пользователь меняет СНО массово

```
👤 Пользователь выбирает N операций → "Сменить СНО"
  ↓
POST /ChangeTaxationSystem → 202 Accepted
  ↓
Kafka fan-out → для каждой операции consumer вызывает тот же Updater (①②③④)
```

---

## Что сейчас в коде

### Updater: `PaymentFromCustomerUpdater.UpdateOperationAsync()` (строки 82-105)

```csharp
await apiClient.UpdateAsync(request);                        // ① SYNC
await writer.WriteUpdatedEventAsync(request);                // ② KAFKA → Providing/LinkedDocs/ChangeLog
await customTaxPostingsSaver.OverwriteAsync(                 // ③ KAFKA commands (только ByHand)
    PaymentFromCustomerMapper.MapToCustomTaxPostingsOverwriteRequest(request));
```

### CustomTaxPostingsSaver: `CustomTaxPostingsSaver.cs:54-111`

Не просто delete+save. Содержит бизнес-логику:
- строка 57: ранний return если `ProvidePostingType != ByHand`
- строка 71: ветка для USN → `OverwriteUsnAsync()` + providing state
- строка 83: ветка для ООО ОСНО → `OverwriteOsnoAsync()`
- строка 92: ветка для ИП ОСНО + Patent → `OverwritePatentAsync()`
- строка 101: ветка для ИП ОСНО → `OverwriteIpOsnoAsync()` + `taxPostingsRemover.DeletePatentPostingsAsync()`
- строка 73: `providingStateSetter.SetCustomTaxPostingsStateAsync()` — управление состоянием providing

Внутри каждой ветки — Kafka command writers (`usnTaxPostingsCommandWriter.WriteOverwriteAsync()` и т.д.).

### Providing consumer: `PaymentFromCustomerTaxPostingsProvider.cs:51-115`

Аналогичная бизнес-логика для автоматических проводок:
- строка 65: `taxPostingsRemover.DeleteAsync()` — удаление всех проводок
- строка 67-78: USN ветка → `UsnPostingsSaver.OverwriteAsync()` → `ITaxPostingsUsnClient.Save/Delete`
- строка 80-88: USN+Patent ветка → `PatentPostingsSaver.OverwriteAsync()` → `ITaxPostingsPsnClient.Save/Delete`
- строка 91-102: OSNO ветки
- строка 104-111: OSNO+Patent ветка

### Массовая смена: `PaymentFromCustomerTaxationSystemUpdater.cs:30-48`

```csharp
await updater.UpdateAsync(request);  // строка 48 — тот же Updater
```

### SetReserve: `PaymentFromCustomerUpdater.cs:65-68`

```csharp
return writer.WriteSetReserveEventAsync(request);  // только Kafka
```

Логика обновления резерва живёт в `PaymentLinksCreator.UpdateReserveAsync()` в Providing-контуре (не в Money.Business).

---

## Доступные HTTP-клиенты

`Money.Business.csproj`:
- строка 45: `TaxPostings.ApiClient.Abstractions` → `ITaxPostingsUsnClient`, `ITaxPostingsPsnClient`, `ITaxPostingsOsnoClient`
- строка 34: `LinkedDocuments.ApiClient.Abstractions` → `IBaseDocumentsClient`

Реализации регистрируются через `Money.Api.csproj` (строки 85, 68).

---

## Как сделать

### 1. Рефакторинг CustomTaxPostingsSaver — два режима вместо дублирования

Нельзя просто заменить Kafka commands на HTTP-клиенты — в `CustomTaxPostingsSaver` зашита доменная логика:
- ветвление по СНО (USN/OSNO/Patent, строки 71-110)
- providing state management (строка 73)
- удаление патентных проводок перед записью ОСНО (строка 130)
- установка TaxStatus через providing state (строка 73: `providingStateSetter.SetCustomTaxPostingsStateAsync()`)

**Подход:** стратегия записи через интерфейс. Бизнес-логика остаётся в `CustomTaxPostingsSaver`, меняется только транспорт:

```csharp
interface ITaxPostingsWriter
{
    Task OverwriteUsnAsync(long documentBaseId, ...);
    Task OverwritePatentAsync(long documentBaseId, ...);
    Task OverwriteOsnoAsync(long documentBaseId, ...);
}

// Sync: через HTTP-клиенты (для ручного PUT)
class SyncTaxPostingsWriter : ITaxPostingsWriter { ... }

// Async: через Kafka commands (текущее поведение)
class KafkaTaxPostingsWriter : ITaxPostingsWriter { ... }
```

**Обязательно:** parity-тесты перед rollout. На одних и тех же входных данных прогнать оба режима (sync и Kafka) и сравнить результат в TaxPostings: одинаковые проводки, одинаковые tax statuses, одинаковые providing states. Без parity-тестов можно сломать семантику ручных проводок (ByHand) или нарушить providing state.

### 2. SetReserve — sync вызов к Providing API

Нельзя копировать `PaymentLinksCreator.UpdateReserveAsync()` из Providing в Money.Business — это дублирование.

**Подход:** если Providing уже имеет HTTP API (проверить наличие endpoint для SetReserve), вызывать его синхронно. Если нет — добавить endpoint в Providing API и вызывать через HTTP-клиент из Money.Business.

### 3. Updater — заменить Kafka на sync вызовы

```csharp
private async Task UpdateOperationAsync(PaymentFromCustomerSaveRequest request)
{
    if (request.TaxationSystemType == null)
        request.TaxationSystemType = await taxationSystemTypeReader.GetDefaultByYearAsync(request.Date.Year);

    // ① sync: source-of-truth (без изменений)
    await apiClient.UpdateAsync(request);

    // ② sync: производные модели (best-effort)
    try
    {
        await customTaxPostingsSaver.OverwriteSyncAsync(request);  // sync через ITaxPostingsWriter
        await baseDocumentsClient.UpdateAsync(...);
    }
    catch (Exception ex)
    {
        // source-of-truth уже записан — логируем, не откатываем
        // проводки обновятся при следующем provide или ручном пересчёте
        logger.LogError(ex, "Failed to sync derived models");
    }

    // ③ Kafka: только аудит
    await changeLogWriter.WriteAsync(...);
}
```

**Стратегия при сбое:** source-of-truth (`Accounting_PaymentOrder`) записывается первым и не откатывается. Производные модели (проводки, LinkedDocuments) — best-effort. Если HTTP упал — логируем. Providing consumer при следующем событии (или при ручном пересчёте) исправит рассинхрон. Это лучше текущего состояния (сейчас производные модели ВСЕГДА async).

### 4. Массовая смена — учесть нагрузку

`PaymentFromCustomerTaxationSystemUpdater.cs:48` вызывает тот же `updater.UpdateAsync()`. После рефакторинга Updater станет тяжелее (3-4 sync HTTP на операцию вместо 1).

**Что учесть:**
- Таймауты HTTP-клиентов (текущие: 30 сек в `PaymentOrderApiClient.cs:34`)
- Размер consumer pool (`MoneyEventConsumerCount` setting)
- Backpressure: если consumer не успевает — Kafka lag растёт
- Мониторинг: время обработки одного сообщения до/после изменения

Фронт: заменить `setTimeout(10000)` на polling sync-объекта (`TaxationSystemChangingSyncObjectManager` уже есть).

### 5. PaymentToSupplier — добавить TaxationSystemType + e2e тест

Добавить поле на все слои по аналогии с PaymentFromCustomer. SQL готов (`Update.sql:27`, `Select.sql:28`).

**Обязательно:** end-to-end контрактный тест, который проверяет что `TaxationSystemType` проходит сквозь всю цепочку: фронт → SaveDto → Mapper → SaveRequest → Updater → Mapper → DTO → PaymentOrders Mapper → SQL → Select → Response → фронт. Без теста поле снова потеряется при рефакторинге.

### 6. Fallback null→default в Reader

`PaymentFromCustomerReader.cs:44-47` — убрать или добавить флаг `isTaxationSystemTypeDefault` в Response.

---

## Риски и митигация

| Риск | Описание | Митигация |
|---|---|---|
| Частичная запись | Source-of-truth записан, HTTP к TaxPostings упал — проводки не обновлены | Best-effort: логируем, не откатываем. Providing/ручной пересчёт исправит. Лучше текущего (сейчас ВСЕГДА async) |
| Сломать семантику проводок | Sync-путь может дать другой результат чем Kafka-путь (tax status, providing state) | Parity-тесты: прогнать оба режима на одних данных, сравнить результат |
| Смешение bounded contexts | Если перенести reserve-логику из Providing в Money.Business — дублирование правил | SetReserve через фасад Providing API, не копирование PaymentLinksCreator |
| Нагрузка при массовой смене | Updater тяжелее (3-4 sync HTTP) → consumer медленнее | Мониторинг latency/lag, настройка таймаутов и consumer pool |
| Потеря поля PaymentToSupplier | Много слоёв маппинга — легко пропустить | End-to-end контрактный тест по всей цепочке |
| UI показывает старое после 200 OK | Кеш, polling, задержка рендера — не зависят от sync записи | Не обещать "UI сразу актуален", обещать "данные в хранилищах актуальны" |

---

## Итог

| Инициатор | Действие | Было | Стало |
|---|---|---|---|
| 👤 PUT | TaxationSystemType в БД | Sync | Sync (без изменений) |
| 👤 PUT | Проводки | Kafka → Providing → HTTP-клиенты | Sync: та же бизнес-логика, sync режим записи (ITaxPostingsWriter) |
| 👤 PUT | LinkedDocuments | Kafka → consumer → DAO | Sync: `IBaseDocumentsClient.UpdateAsync()` |
| 👤 PUT | Аудит | Kafka | Kafka (оставить) |
| 👤 SetReserve | Резерв | Только Kafka | Sync: HTTP к Providing API |
| 👤 Массовая смена | N операций | Kafka fan-out → Updater | Kafka fan-out → тот же Updater (sync внутри). Polling вместо таймера |
| — | PaymentToSupplier | Поле отсутствует | Добавить + e2e тест |
| — | Fallback | Молча подставляет | Убрать или пометить |
