export function round(value, decimalPart = 2) {
    return Math.round(value * (10 ** decimalPart)) / (10 ** decimalPart);
}

export default { round };
