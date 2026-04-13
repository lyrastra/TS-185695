export default {
    date: msg(`дату`),
    sum: msg(`сумму`),
    ndsSum: msg(`сумму НДС`),
    payer: msg(`плательщика`),
    outgoingPayer: msg(`получателя`),
    middlemanContract: msg(`посреднический договор`),
    acquiringCommissionDate: msg(`дату комиссии`),
    notPaid: `Не учитывается. Платежное поручение не оплачено.`,
    exchangeRate: msg(`курс`),
    toSettlementAccountId: msg(`счёт зачисления`),
    fromSettlementAccountId: msg(`счёт списания`),
    fixedAsset: msg(`основное средство`),
    period: `Не учитывается. Укажите период.`,
    kbk: msg(`КБК`),
    loanInterestSum: `Не учитывается при расчёте налога`,
    documents: `Не учитывается. Добавьте документ`,
    accountCode: msg(`вид налога`)
};

function msg(field) {
    return `Не учитывается. Заполните ${field}`;
}
