import AvailableTaxPostingDirection from '../../enums/newMoney/AvailableTaxPostingDirectionEnum';
import {
    getSubcontoModelToUpdate,
    getSubcontosScheme,
    loadAllAccountsSchemes
} from '../../apps/money/components/operations/commonComponents/Postings/helpers/postingSubcontosHelper';
import accountingPostingsValidator from '../../apps/money/components/operations/validation/accountingPostingsValidator';

export function sortPostings(a, b) {
    if (a.LinkedDocument && b.LinkedDocument) { return 0; }

    if (a.LinkedDocument) { return -1; }

    return 1;
}

export function availableDirection(direction) {
    switch (direction) {
        case AvailableTaxPostingDirection.Incoming:
            return `Доход`;
        case AvailableTaxPostingDirection.Outgoing:
            return `Расход`;
        default:
            return `доход/расход`;
    }
}

export const updateAccountingPostingsModelForOtherOperations = async postings => {
    const accountsData = await loadAllAccountsSchemes();

    return postings.map(p => {
        let posting = { ...p };

        const {
            Debit, Credit, SubcontoDebit, SubcontoCredit
        } = posting;

        const debitSubcontos = getSubcontosScheme(accountsData, Debit.Number);
        const creditSubcontos = getSubcontosScheme(accountsData, Credit.Number);

        const updatedDebitSubcontos = getSubcontoModelToUpdate(SubcontoDebit, debitSubcontos);
        const updatedCreditSubcontos = getSubcontoModelToUpdate(SubcontoCredit, creditSubcontos);

        if (updatedDebitSubcontos) {
            posting = accountingPostingsValidator.getValidatedByDebitSubconto({
                ...posting,
                SubcontoDebit: updatedDebitSubcontos
            });
        }

        if (updatedCreditSubcontos) {
            posting = accountingPostingsValidator.getValidatedByCreditSubconto({
                ...posting,
                SubcontoCredit: updatedCreditSubcontos
            });
        }

        return posting;
    });
};

export default { sortPostings, availableDirection, updateAccountingPostingsModelForOtherOperations };
