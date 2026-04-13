import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';

export function getTaxationSystem({ taxationSystemType, taxationSystem }) {
    if (!taxationSystemType) {
        return taxationSystem;
    }

    const IsUsn = taxationSystemType === TaxationSystemType.Usn
        || taxationSystemType === TaxationSystemType.UsnAndEnvd
        || taxationSystemType === TaxationSystemType.UsnAndPatent
        || taxationSystemType === TaxationSystemType.UsnAndPatentAndEnvd;

    const IsEnvd = taxationSystemType === TaxationSystemType.Envd
        || taxationSystemType === TaxationSystemType.UsnAndEnvd
        || taxationSystemType === TaxationSystemType.EnvdAndPatent
        || taxationSystemType === TaxationSystemType.UsnAndPatentAndEnvd
        || taxationSystemType === TaxationSystemType.OsnoAndEnvd;

    const IsOsno = taxationSystemType === TaxationSystemType.Osno
        || taxationSystemType === TaxationSystemType.OsnoAndEnvd;

    return { ...taxationSystem, ...{ IsUsn, IsEnvd, IsOsno } };
}

export function getValidTaxationSystemType({ taxationSystemType, taxationSystem, isOoo }) {
    if (!taxationSystemType) {
        return null;
    }

    const isIpOsno = taxationSystem.IsOsno && isOoo === false;

    switch (taxationSystemType) {
        case TaxationSystemType.Usn:
            return taxationSystem.IsUsn ? taxationSystemType : null;
        case TaxationSystemType.UsnAndEnvd:
            return taxationSystem.IsUsn && taxationSystem.IsEnvd ? taxationSystemType : null;
        case TaxationSystemType.Osno:
            return taxationSystem.IsOsno ? taxationSystemType : null;
        case TaxationSystemType.OsnoAndEnvd:
            return taxationSystem.IsOsno && taxationSystem.IsEnvd ? taxationSystemType : null;
        case TaxationSystemType.Envd:
            return taxationSystem.IsEnvd ? taxationSystemType : null;
        case TaxationSystemType.Patent:
            return taxationSystem.IsUsn || taxationSystem.IsEnvd || isIpOsno ? taxationSystemType : null;
    }

    return null;
}

export function getDefaultTaxationSystemType(taxationSystemObj) {
    const { IsUsn, IsOsno } = taxationSystemObj;

    if (IsUsn) {
        return TaxationSystemType.Usn;
    }

    if (IsOsno) {
        return TaxationSystemType.Osno;
    }

    return TaxationSystemType.Envd;
}

export function getTaxationSystemType(taxationSystemObj) {
    const { IsUsn, IsOsno, IsEnvd } = taxationSystemObj;

    if (IsUsn && IsEnvd) {
        return TaxationSystemType.UsnAndEnvd;
    } else if (IsOsno && IsEnvd) {
        return TaxationSystemType.OsnoAndEnvd;
    } else if (IsUsn) {
        return TaxationSystemType.Usn;
    } else if (IsOsno) {
        return TaxationSystemType.Osno;
    } else if (IsEnvd) {
        return TaxationSystemType.Envd;
    }

    return TaxationSystemType.Default;
}

export function isValidTaxationSystem(taxationSystem, operationTaxationSystem) {
    switch (taxationSystem) {
        case TaxationSystemType.UsnAndEnvd:
            return operationTaxationSystem === TaxationSystemType.Usn || operationTaxationSystem === TaxationSystemType.Envd;
        case TaxationSystemType.OsnoAndEnvd:
            return operationTaxationSystem === TaxationSystemType.Osno || operationTaxationSystem === TaxationSystemType.Envd;
        case TaxationSystemType.Envd:
            return operationTaxationSystem === TaxationSystemType.Envd;
        case TaxationSystemType.Usn:
            return operationTaxationSystem === TaxationSystemType.Usn;
        case TaxationSystemType.Osno:
            return operationTaxationSystem === TaxationSystemType.Osno;
    }

    return false;
}

export const mapTaxationSystemTypeToDropdown = item => ({
    value: item.type,
    text: item.text
});

export default {
    getTaxationSystem,
    getValidTaxationSystemType,
    getDefaultTaxationSystemType,
    getTaxationSystemType,
    isValidTaxationSystem,
    mapTaxationSystemTypeToDropdown
};
