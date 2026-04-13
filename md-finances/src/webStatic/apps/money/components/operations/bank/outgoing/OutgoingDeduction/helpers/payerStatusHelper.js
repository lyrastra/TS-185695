export function isBailiffPayment(settlementAccount) {
    // если счет получателя начинается с «03212», то по умолчанию выбираем статус 31
    return settlementAccount?.substring(0, 5) === `03212`;
}

export default { isBailiffPayment };
