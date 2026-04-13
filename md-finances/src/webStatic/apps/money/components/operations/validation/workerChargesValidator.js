export function validateChargeSum(charge) {
    if (!charge.Sum) {
        return `Укажите сумму`;
    }

    if (charge.Salary && charge.Salary < charge.Sum) {
        return `Остаток по начислению ${charge.formattedSalary} руб.`;
    }

    return ``;
}

export function validateWorker(worker) {
    if (!worker.WorkerId) {
        return `Введите сотрудника`;
    }

    return ``;
}

export default { validateWorker, validateChargeSum };
