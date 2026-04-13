# Рекомендации для внешних клиентов API

## Использование заголовка `MD-AuditTrail-Context` в ответах

При работе с нашими API вы можете получать заголовок `MD-AuditTrail-Context` в ответах. Этот заголовок содержит идентификатор трейса для расследования инцидентов.

**Рекомендации по использованию:**

1. **При ошибках** - сохраняйте весь заголовок `MD-AuditTrail-Context` для передачи в службу поддержки
2. **В логах** - логируйте полное значение заголовка для корреляции с серверными логами

**Пример сохранения:**
```javascript
const response = await fetch('/api/orders');
const auditContext = response.headers.get('MD-AuditTrail-Context');
if (auditContext) {
    // Сохраняем весь заголовок для поддержки
    localStorage.setItem('lastApiAuditContext', auditContext);
    console.log(`Audit context: ${auditContext}`);
}
```

Это поможет нам быстрее расследовать инциденты и улучшить качество поддержки.
