import { getUnifiedPaymentsOptions } from './mapper';
import SyntheticAccountCodesEnum from '../../../../../../../../../../enums/SyntheticAccountCodesEnum';

export function getOptionsWithEmpty(value, options) {
    if (!value) {
        return [
            {
                text: `&nbsp;`,
                label: `&nbsp;`,
                value: null,
                disabled: true,
                hidden: true
            },
            ...options
        ];
    }

    return options;
}

export function getAvailableTypes({ hasPatents }) {
    return getUnifiedPaymentsOptions().filter(o => {
        if (o.value === SyntheticAccountCodesEnum.patent && !hasPatents) {
            return false;
        }

        return true;
    });
}
