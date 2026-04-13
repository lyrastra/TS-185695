const InvoiceStatusCodeEnum = {
    None: 0,
    // Если не хватает прав у клиента для выполнения запроса в банке
    AccessError: 1,
    // Если реквизиты в банке не прошли валидацию
    RequisiteError: 2,
    // Если по счету нельзя выполнять сквозной платеж
    InvalidSettlementAccount: 3,
    // в МД невозможно создать сквозной платеж через API банка
    ValidationError: 4
};

export default InvoiceStatusCodeEnum;
