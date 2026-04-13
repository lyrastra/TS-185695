import { computed } from 'mobx';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import UsnTypeEnum from '@moedelo/frontend-enums/mdEnums/UsnType';
import CalendarTypesEnum from '../../../../../../../../../enums/newMoney/budgetaryPayment/BudgetaryCalendarTypesEnum';
import AccountTypeEnum from '../../../../../../../../../enums/newMoney/budgetaryPayment/BudgetaryAccountTypeEnum';
import budgetaryDateHelper from '../../../../../../../../../helpers/newMoney/budgetaryPayment/budgetaryDateHelper';
import SyntheticAccountCodesEnum from '../../../../../../../../../enums/SyntheticAccountCodesEnum';
import { mapCurrencyInvoicesToBackendModel } from '../../../../../../../../../mappers/currencyInvoicesMapper';
import ProvidePostingType from '../../../../../../../../../enums/ProvidePostingTypeEnum';
import patentMapper from '../../../../../../../../../mappers/patentMapper';
import TaxStatusEnum from '../../../../../../../../../enums/TaxStatusEnum';
import OpenOperationActions from '../../../../../../../../../enums/newMoney/OpenOperationActionsEnum';

export default class Computed {
    @computed get canShowSecondAutocomplete() {
        return ![CalendarTypesEnum.Year, CalendarTypesEnum.NoPeriod].some(type => type === this.model.Period.Type);
    }

    @computed get canShowYearAutocomplete() {
        return CalendarTypesEnum.NoPeriod !== this.model.Period.Type;
    }

    @computed get getDataForSecondAutocomplete() {
        switch (this.model.Period.Type) {
            case CalendarTypesEnum.Month:
                return budgetaryDateHelper.getMonthsListForDropdown();
            case CalendarTypesEnum.Quarter:
                return budgetaryDateHelper.getQuarterListForDropdown();
            case CalendarTypesEnum.HalfYear:
                return budgetaryDateHelper.getHalfYearListForDropdown();
        }

        return [];
    }

    @computed get getValueForSecondAutocomplete() {
        switch (this.model.Period.Type) {
            case CalendarTypesEnum.Month:
                return this.model.Period.Month;
            case CalendarTypesEnum.Quarter:
                return this.model.Period.Quarter;
            case CalendarTypesEnum.HalfYear:
                return this.model.Period.HalfYear;
        }

        return [];
    }

    @computed get kbkList() {
        return this.KbkDefault.map(kbk => {
            return {
                value: kbk.Id,
                text: kbk.Name,
                original: kbk
            };
        });
    }

    @computed get currentKbkData() {
        return this.KbkDefault.find(kbk => kbk.Id === this.model.Kbk.Id);
    }

    @computed get currentKbkNumber() {
        const currentKbk = this.currentKbkData;

        if (currentKbk && currentKbk.Name) {
            return currentKbk.Name.trim().split(` `).pop();
        }

        return ``;
    }

    @computed get hasPatents() {
        return this.patents.length > 0;
    }

    @computed get patentSelectData() {
        return [{ value: null, text: `` }].concat(patentMapper.mapPatentsToDropDown(this.patents));
    }

    @computed get taxesOrFeesList() {
        const cutOffYearFor6806And6811 = 2025;
        const startDateFor6806And6811 = dateHelper(this.budgetaryPaymentModel?.Date).isBefore(`01.01.2025`);

        return this.accountCodes
            .filter(account => this.hasPatents || account.Code !== SyntheticAccountCodesEnum.patent)
            .filter(el => !(
                (el.value === SyntheticAccountCodesEnum._68_06 || el.value === SyntheticAccountCodesEnum._68_11) &&
                    (startDateFor6806And6811 || this.model?.Period?.Year < cutOffYearFor6806And6811)
            ))
            .map(({ Code: value, Name: text, DefaultCalendarType }) => ({ value, text, DefaultCalendarType }));
    }

    @computed get accountType() {
        const taxesCodeRegexp = /^68/;
        const feesCodeRegexp = /^69/;

        if (taxesCodeRegexp.test(this.model.AccountCode)) {
            return AccountTypeEnum.Taxes.value;
        }

        if (feesCodeRegexp.test(this.model.AccountCode)) {
            return AccountTypeEnum.Fees.value;
        }

        return null;
    }

    @computed get yearsList() {
        return budgetaryDateHelper.getYearsListForDropdown();
    }

    @computed get isTaxesFieldEmpty() {
        return !this.model.AccountCode;
    }

    @computed get getSubPaymentForSave() {
        const {
            Kbk, CurrencyInvoices, TaxPostingsMode, TaxPostings, ...rest
        } = this.model;
        const customPostings = TaxPostingsMode === ProvidePostingType.ByHand ? TaxPostings.Postings : [];

        return {
            ...rest,
            KbkId: Kbk.Id,
            CurrencyInvoices: mapCurrencyInvoicesToBackendModel(CurrencyInvoices.Data),
            TaxPostings: this.getTaxPostingForSave(customPostings, TaxPostingsMode, this.TaxationSystem)
        };
    }

    @computed get isTradingFee() {
        return this.model.AccountCode === SyntheticAccountCodesEnum.tradingFees;
    }

    @computed get patentSelectVisible() {
        return this.model.AccountCode === SyntheticAccountCodesEnum.patent;
    }

    @computed get patentValue() {
        return this.model.PatentId;
    }

    @computed get isNotTaxable() {
        return !!this.getTaxPostingsExplainingMsg() || this.model.TaxPostings.TaxStatus === TaxStatusEnum.NotTax;
    }

    @computed get areTaxPostingsValid() {
        return this.model.TaxPostings.Postings.some(({ DescriptionError, SumError, AllSumValidationMessage }) => {
            const errors = [DescriptionError, SumError, AllSumValidationMessage];

            return !errors.some(message => message?.length);
        });
    }

    @computed get isEmpty() {
        const { AccountCode, Sum, Kbk: { Id } } = this.model;

        return [AccountCode, Sum, Id].some(value => !value);
    }

    @computed get isValid() {
        return !Object.values(this.validationState).some(message => message.length > 0) && (this.isNotTaxable || this.areTaxPostingsValid);
    }

    @computed get isNew() {
        return this.budgetaryPaymentModel.Action === OpenOperationActions.new;
    }

    // Requisites //

    @computed get isUsn() {
        return this.TaxationSystem.IsUsn;
    }

    @computed get isUsn15() {
        return this.TaxationSystem.UsnType === UsnTypeEnum.ProfitAndOutgo;
    }

    @computed get isEnvd() {
        return this.TaxationSystem.IsEnvd;
    }

    @computed get isOsno() {
        return this.TaxationSystem.IsOsno;
    }

    @computed get isOoo() {
        return this.Requisites.IsOoo;
    }

    @computed get isIp() {
        return !this.Requisites.IsOoo;
    }

    @computed get isIpOsno() {
        return this.TaxationSystem.IsOsno && !this.Requisites.IsOoo;
    }

    @computed get taxationForPostings() {
        return this.TaxationSystem;
    }

    // Postings //

    @computed get canViewTaxPostings() {
        // return getAccessToPostings();
        return true;
    }

    @computed get isUsnTaxPostings() {
        return this.isUsn || this.taxationSystemTypeValueIsPatent;
    }

    @computed get isIpOsnoTaxPostings() {
        return this.isOsno && this.isIp;
    }

    @computed get canEditTaxPostings() {
        return this.canEdit;
    }

    @computed get isTaxPostingsLoading() {
        return this.taxPostingsLoading;
    }
}
