/** при сохранении к названию банка приклеивается город 2 раза, зачем хз
 * надо будет выпилить это. приклееный город мешает автокомплиту */
export function removeCityFromBankName(bankName) {
    if (bankName == null || bankName === undefined) {
        return bankName;
    }

    const res = bankName.split(`,`);

    if (res.length) {
        return res[0];
    }

    return bankName;
}

export function removeKbkFromBankName(bankName) {
    if (bankName === null || bankName === undefined) {
        return bankName;
    }

    const res = bankName.split(` // `);

    if (res.length) {
        return res[0];
    }

    return bankName;
}

export default { removeCityFromBankName };
