/* eslint-disable */
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

export function getNdsOptions({date, isUsn, isOutgoing}) {
    let ndsPercent = [
        { value: Common.Data.BankAndCashNdsTypes.Nds18, label: `18%` },
        { value: Common.Data.BankAndCashNdsTypes.Nds10, label: `10%` }
    ];

    let nds = [
        { value: Common.Data.BankAndCashNdsTypes.Nds118, label: `18/118` },
        { value: Common.Data.BankAndCashNdsTypes.Nds110, label: `10/110` },
        { value: Common.Data.BankAndCashNdsTypes.Nds0, label: `0%` },
        { value: Common.Data.BankAndCashNdsTypes.None, label: `Без НДС` },
        { value: Common.Data.BankAndCashNdsTypes.Empty, label: `&nbsp;` }
    ];

    const nds20PercentDate = dateHelper(`2019-01-01`);
    const ndsUsnAfter2025 = dateHelper(`2025-01-01`);
    const ndsAfter2026 = dateHelper(`2026-01-01`);
    const documentDate = dateHelper(date, `DD.MM.YYYY`);

    if (documentDate.isSameOrAfter(nds20PercentDate)) {
        ndsPercent.unshift({ value: Common.Data.BankAndCashNdsTypes.Nds20, label: `20%` });
        nds.unshift({ value: Common.Data.BankAndCashNdsTypes.Nds120, label: `20/120` });
    }
    
    if (documentDate.isSameOrAfter(ndsUsnAfter2025) && !isUsn && !isOutgoing) {
        nds = nds.filter(option => option.value !== Common.Data.BankAndCashNdsTypes.Nds118);
        ndsPercent = ndsPercent.filter(option => option.value !== Common.Data.BankAndCashNdsTypes.Nds18);
    }

    if (documentDate.year() === 2025 && !isUsn && isOutgoing) {
        return [
            { value: Common.Data.BankAndCashNdsTypes.Nds20, label: `20%` },
            { value: Common.Data.BankAndCashNdsTypes.Nds10, label: `10%` },
            { value: Common.Data.BankAndCashNdsTypes.Nds120, label: `20/120` },
            { value: Common.Data.BankAndCashNdsTypes.Nds110, label: `10/110` },
            { value: Common.Data.BankAndCashNdsTypes.Nds0, label: `0%` },
            { value: Common.Data.BankAndCashNdsTypes.None, label: `Без НДС` },
            { value: Common.Data.BankAndCashNdsTypes.Nds5, label: `5%` },
            { value: Common.Data.BankAndCashNdsTypes.Nds7, label: `7%` },
            { value: Common.Data.BankAndCashNdsTypes.Nds5To105, label: `5/105` },
            { value: Common.Data.BankAndCashNdsTypes.Nds7To107, label: `7/107` },
            { value: Common.Data.BankAndCashNdsTypes.Empty, label: `&nbsp;` }
        ];
    }

    if (documentDate.isSameOrAfter(ndsAfter2026) && !isUsn && isOutgoing) {
        return [
            { value: Common.Data.BankAndCashNdsTypes.Nds22, label: `22%` },
            { value: Common.Data.BankAndCashNdsTypes.Nds20, label: `20%` },
            { value: Common.Data.BankAndCashNdsTypes.Nds10, label: `10%` },
            { value: Common.Data.BankAndCashNdsTypes.Nds22To122, label: `22/122` },
            { value: Common.Data.BankAndCashNdsTypes.Nds120, label: `20/120` },
            { value: Common.Data.BankAndCashNdsTypes.Nds110, label: `10/110` },
            { value: Common.Data.BankAndCashNdsTypes.Nds0, label: `0%` },
            { value: Common.Data.BankAndCashNdsTypes.None, label: `Без НДС` },
            { value: Common.Data.BankAndCashNdsTypes.Nds5, label: `5%` },
            { value: Common.Data.BankAndCashNdsTypes.Nds7, label: `7%` },
            { value: Common.Data.BankAndCashNdsTypes.Nds5To105, label: `5/105` },
            { value: Common.Data.BankAndCashNdsTypes.Nds7To107, label: `7/107` },
            { value: Common.Data.BankAndCashNdsTypes.Empty, label: `&nbsp;` }
        ];
    }

    if (documentDate.isSameOrAfter(ndsAfter2026) && !isUsn) {
        return [
            { value: Common.Data.BankAndCashNdsTypes.Nds22, label: `22%` },
            { value: Common.Data.BankAndCashNdsTypes.Nds20, label: `20%` },
            { value: Common.Data.BankAndCashNdsTypes.Nds10, label: `10%` },
            { value: Common.Data.BankAndCashNdsTypes.Nds22To122, label: `22/122` },
            { value: Common.Data.BankAndCashNdsTypes.Nds120, label: `20/120` },
            { value: Common.Data.BankAndCashNdsTypes.Nds110, label: `10/110` },
            { value: Common.Data.BankAndCashNdsTypes.Nds0, label: `0%` },
            { value: Common.Data.BankAndCashNdsTypes.None, label: `Без НДС` },
            { value: Common.Data.BankAndCashNdsTypes.Empty, label: `&nbsp;` }
        ];
    }

    if (documentDate.year() === 2025 && isUsn) {
        return [
            { value: Common.Data.BankAndCashNdsTypes.None, label: `Без НДС` },
            { value: Common.Data.BankAndCashNdsTypes.Nds5, label: `5%` },
            { value: Common.Data.BankAndCashNdsTypes.Nds7, label: `7%` },
            { value: Common.Data.BankAndCashNdsTypes.Nds10, label: `10%` },
            { value: Common.Data.BankAndCashNdsTypes.Nds20, label: `20%` },
            { value: Common.Data.BankAndCashNdsTypes.Nds5To105, label: `5/105` },
            { value: Common.Data.BankAndCashNdsTypes.Nds7To107, label: `7/107` },
            { value: Common.Data.BankAndCashNdsTypes.Nds110, label: `10/110` },
            { value: Common.Data.BankAndCashNdsTypes.Nds120, label: `20/120` },
            { value: Common.Data.BankAndCashNdsTypes.Nds0, label: `0%` },
            { value: Common.Data.BankAndCashNdsTypes.Empty, label: `&nbsp;` }
        ];
    }

    if (documentDate.isSameOrAfter(ndsAfter2026) && isUsn) {
        return [
            { value: Common.Data.BankAndCashNdsTypes.None, label: `Без НДС` },
            { value: Common.Data.BankAndCashNdsTypes.Nds5, label: `5%` },
            { value: Common.Data.BankAndCashNdsTypes.Nds7, label: `7%` },
            { value: Common.Data.BankAndCashNdsTypes.Nds10, label: `10%` },
            { value: Common.Data.BankAndCashNdsTypes.Nds22, label: `22%` },
            { value: Common.Data.BankAndCashNdsTypes.Nds20, label: `20%` },
            { value: Common.Data.BankAndCashNdsTypes.Nds5To105, label: `5/105` },
            { value: Common.Data.BankAndCashNdsTypes.Nds7To107, label: `7/107` },
            { value: Common.Data.BankAndCashNdsTypes.Nds110, label: `10/110` },
            { value: Common.Data.BankAndCashNdsTypes.Nds22To122, label: `22/122` },
            { value: Common.Data.BankAndCashNdsTypes.Nds120, label: `20/120` },
            { value: Common.Data.BankAndCashNdsTypes.Nds0, label: `0%` },
            { value: Common.Data.BankAndCashNdsTypes.Empty, label: `&nbsp;` }
        ];
    }

    return [...ndsPercent, ...nds];
}

export function getMediationNdsOptions() {
    const ndsAfter2026 = dateHelper(`2026-01-01`);
    const documentDate = dateHelper(date, `DD.MM.YYYY`);

     if (documentDate.year() === 2025) {
        return [
            { value: Common.Data.BankAndCashNdsTypes.None, label: `Без НДС` },
            { value: Common.Data.BankAndCashNdsTypes.Nds5, label: `5%` },
            { value: Common.Data.BankAndCashNdsTypes.Nds7, label: `7%` },
            { value: Common.Data.BankAndCashNdsTypes.Nds10, label: `10%` },
            { value: Common.Data.BankAndCashNdsTypes.Nds20, label: `20%` },
            { value: Common.Data.BankAndCashNdsTypes.Nds5To105, label: `5/105` },
            { value: Common.Data.BankAndCashNdsTypes.Nds7To107, label: `7/107` },
            { value: Common.Data.BankAndCashNdsTypes.Nds110, label: `10/110` },
            { value: Common.Data.BankAndCashNdsTypes.Nds120, label: `20/120` },
            { value: Common.Data.BankAndCashNdsTypes.Nds0, label: `0%` },
            { value: Common.Data.BankAndCashNdsTypes.Empty, label: `&nbsp;` }
        ];
    }

    if (documentDate.isSameOrAfter(ndsAfter2026)) {
        return [
            { value: Common.Data.BankAndCashNdsTypes.None, label: `Без НДС` },
            { value: Common.Data.BankAndCashNdsTypes.Nds5, label: `5%` },
            { value: Common.Data.BankAndCashNdsTypes.Nds7, label: `7%` },
            { value: Common.Data.BankAndCashNdsTypes.Nds10, label: `10%` },
            { value: Common.Data.BankAndCashNdsTypes.Nds20, label: `20%` },
            { value: Common.Data.BankAndCashNdsTypes.Nds22, label: `22%` },
            { value: Common.Data.BankAndCashNdsTypes.Nds5To105, label: `5/105` },
            { value: Common.Data.BankAndCashNdsTypes.Nds7To107, label: `7/107` },
            { value: Common.Data.BankAndCashNdsTypes.Nds110, label: `10/110` },
            { value: Common.Data.BankAndCashNdsTypes.Nds120, label: `20/120` },
            { value: Common.Data.BankAndCashNdsTypes.Nds22To122, label: `22/122` },
            { value: Common.Data.BankAndCashNdsTypes.Nds0, label: `0%` },
            { value: Common.Data.BankAndCashNdsTypes.Empty, label: `&nbsp;` }
        ];
    }
}

export default {
    getNdsOptions, getMediationNdsOptions
};
