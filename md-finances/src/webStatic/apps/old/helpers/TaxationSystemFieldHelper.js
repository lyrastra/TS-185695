/* global Money */
import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';
import MdConverter from '@moedelo/md-frontendcore/mdCommon/helpers/MdConverter';

class TaxationSystemFieldHelper {
    static getTaxationOptions({ date }) {
        const ts = new Money.Collections.Common.TaxationSystemCollection(window._preloading.TaxationSystems);
        const taxationSystem = ts.Current(MdConverter.toDate(date));

        const isOsno = taxationSystem.get(`IsOsno`);
        const isUsn = taxationSystem.get(`IsUsn`);
        const isEnvd = taxationSystem.get(`IsEnvd`);

        if (isOsno && isEnvd) {
            return [
                { value: TaxationSystemType.Osno, label: `ОСНО` },
                { value: TaxationSystemType.Envd, label: `ЕНВД` },
                { value: TaxationSystemType.OsnoAndEnvd, label: `ОСНО + ЕНВД` }
            ];
        }

        if (isUsn && isEnvd) {
            return [
                { value: TaxationSystemType.Usn, label: `УСН` },
                { value: TaxationSystemType.Envd, label: `ЕНВД` },
                { value: TaxationSystemType.UsnAndEnvd, label: `УСН + ЕНВД` }
            ];
        }

        return [];
    }
}

export default TaxationSystemFieldHelper;
