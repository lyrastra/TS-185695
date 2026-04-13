export default {
    date: msg(`дату`),
    sum: msg(`сумму`),
    payer: msg(`плательщика`),
    outgoingPayer: msg(`получателя`),
    middlemanContract: msg(`посреднический договор`),
    acquiringCommissionDate: msg(`дату комиссии`),
    notPaid: `Не учитывается. Платежное поручение не оплачено.`,
    kbk: `Не учитывается. Заполните КБК.`,
    workerName: `Не учитывается. Заполните поле ФИО.`,
    workerSum: `Не учитывается. Заполните сумму к выплате.`,
    exchangeRate: msg(`курс`),
    toSettlementAccountId: msg(`счёт зачисления`),
    fromSettlementAccountId: msg(`счёт списания`),
    subPayments: msg(`виды налогов/взносов`)
};

function msg(field) {
    return `Не учитывается. Заполните ${field}`;
}
