import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import ndsList from '../../resources/newMoney/ndsListResource';
import NdsTypesEnum from '../../enums/newMoney/NdsTypesEnum';

export function getNdsTypes({ date, taxationSystem, direction }) {
    const momentDate = date ? dateHelper(date, `DD.MM.YYYY`) : dateHelper();

    if (taxationSystem) {
        const {
            IsUsn, IsOsno, isAfter2026, isDate2025
        } = taxationSystem;

        if (momentDate.isValid()) {
            if (IsUsn && isAfter2026) {
                return ndsList.usnAfter2026;
            }

            if (IsUsn && isDate2025) {
                return ndsList.usnAfter2025;
            }

            if (IsOsno && isAfter2026 && direction === Direction.Outgoing) {
                return ndsList.outgoingOsnoAfter2026;
            }

            if (IsOsno && isAfter2026 && direction === Direction.Incoming) {
                return ndsList.incomingOsnoAfter2026;
            }

            if (IsOsno && isDate2025 && direction === Direction.Outgoing) {
                return ndsList.outgoingOsnoAfter2025;
            }

            if (IsOsno && isDate2025 && direction === Direction.Incoming) {
                return ndsList.incomingOsnoAfter2025;
            }
        }
    }

    return momentDate.isValid() && momentDate.isBefore(`2019-01-01`) ? ndsList.before2019 : ndsList.after2019;
}

export function hasNds({ IncludeNds, NdsType }) {
    return IncludeNds && [NdsTypesEnum.None, NdsTypesEnum.Nds0].indexOf(NdsType) === -1;
}

export function hasMediationCommissionNds({ IncludeMediationCommissionNds, MediationCommissionNdsType }) {
    return IncludeMediationCommissionNds && [NdsTypesEnum.None, NdsTypesEnum.Nds0].indexOf(MediationCommissionNdsType) === -1;
}

export function needToSetDefaultNds(newNdsTypeValue, currentNdsTypeValue) {
    return [NdsTypesEnum.Nds18, NdsTypesEnum.Nds20, NdsTypesEnum.Nds22].includes(newNdsTypeValue) && newNdsTypeValue !== currentNdsTypeValue;
}

export default {
    getNdsTypes,
    hasNds,
    hasMediationCommissionNds
};
