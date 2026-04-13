function getNumericCode(documentNumber = ``) {
    if (typeof documentNumber !== `string`) {
        return ``;
    }

    return (documentNumber.match(/\d+$/) || [])[0] || ``;
}

function getLiteralCode(documentNumber = ``, metaData) {
    if (typeof documentNumber !== `string`) {
        return ``;
    }

    const designation = (documentNumber.match(/^[А-Яа-я]{2}/) || [])[0] || 0;

    return metaData.PaymentSubReasons.find(i => i.Designation === designation)?.Code || 0;
}

export function parseComplexNumberToObj(documentNumber = ``, metaData) {
    return {
        literalCode: getLiteralCode(documentNumber, metaData),
        value: getNumericCode(documentNumber)
    };
}

export function parseComplexNumberToString(complexNumber, metaData) {
    const { literalCode, value } = complexNumber;

    const code = literalCode !== 0 ? metaData.PaymentSubReasons.find(i => i.Code === literalCode)?.Designation : ``;

    return `${code}${value}`;
}

export default { parseComplexNumberToObj };
