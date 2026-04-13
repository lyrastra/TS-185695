import PayerStatus from '../../../enums/PayerStatus';

const PayerStatusData = [{
    text: `Отсутствует`,
    value: PayerStatus.None
},
{
    text: `19 -  плательщики, перечисляющие задолженность по платежам в бюджет на основании исполнительного документа`,
    value: PayerStatus.DeductionFromSalary
},
{
    text: `31 - плательщики, перечисляющие деньги на счет, в первых пяти знаках которого указано «03212», или в исполнительном документе есть УИН`,
    value: PayerStatus.BailiffPayment
}];

export default PayerStatusData;
