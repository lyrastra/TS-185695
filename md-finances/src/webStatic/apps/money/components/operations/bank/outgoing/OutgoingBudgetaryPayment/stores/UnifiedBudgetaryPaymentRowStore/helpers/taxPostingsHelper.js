import ProvidePostingType from '../../../../../../../../../../enums/ProvidePostingTypeEnum';
import TaxStatusEnum from '../../../../../../../../../../enums/TaxStatusEnum';

const insurancePaymentKbk = `18210202000011000160`;
const insuranceOverdraftKbk = `18210203000011000160`;

export const getUITaxPostings = ({ model, TaxPostings, isDeleting }) => {
    const { TaxStatus, Postings: currentPostings } = model.TaxPostings;
    const isPostingsChangedByHand = model.TaxPostingsMode === ProvidePostingType.ByHand || TaxStatus === TaxStatusEnum.ByHand;
    const isInsurance = [insurancePaymentKbk, insuranceOverdraftKbk].includes(model.Kbk?.Number);
    const taxStatusNo = TaxStatus === TaxStatusEnum.No;
    const newPostings = TaxPostings?.Postings || [];
    const defaultResult = { postings: [{}], mode: null };

    if (isPostingsChangedByHand && !newPostings.length) {
        const mode = TaxStatusEnum.ByHand;

        if (!currentPostings?.length || isDeleting) {
            return { ...defaultResult, mode };
        }

        return { postings: currentPostings, mode };
    }

    if (taxStatusNo && isInsurance && !newPostings?.length) {
        return defaultResult;
    }

    return { ...defaultResult, postings: newPostings };
};

export default {};
