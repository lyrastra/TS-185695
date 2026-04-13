import PaymentPriority from '../../../enums/PaymentPriority';

export const paymentPriorityData = [
    {
        value: PaymentPriority.First,
        text: `1 - алименты либо средства на возмещение вреда жизни или здоровью`
    },
    {
        value: PaymentPriority.Third,
        text: `3 - погашение задолженности без исполнительного документа`
    },
    {
        value: PaymentPriority.Forth,
        text: `4 - иная задолженность работника по исполнительным документам`
    },
    {
        value: PaymentPriority.Fifth,
        text: `5 - платежи в порядке календарной очередности`
    }
];

export default { paymentPriorityData };
