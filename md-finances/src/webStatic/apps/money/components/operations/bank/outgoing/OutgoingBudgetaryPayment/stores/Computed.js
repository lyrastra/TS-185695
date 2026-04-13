import { computed, override, toJS } from 'mobx';
import { toAmountString } from '@moedelo/frontend-core-v2/helpers/converter';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import CalendarTypesEnum from '../../../../../../../../enums/newMoney/budgetaryPayment/BudgetaryCalendarTypesEnum';
import budgetaryDateHelper from '../../../../../../../../helpers/newMoney/budgetaryPayment/budgetaryDateHelper';
import CommonOperationStore from '../../../common/CommonOperationStore';
import AccountTypeEnum from '../../../../../../../../enums/newMoney/budgetaryPayment/BudgetaryAccountTypeEnum';
import PostingsTabsEnum from '../../../../../../../../enums/newMoney/PostingsTabsEnum';
import SyntheticAccountCodesEnum from '../../../../../../../../enums/SyntheticAccountCodesEnum';
import TaxStatusEnum from '../../../../../../../../enums/TaxStatusEnum';
import patentMapper from '../../../../../../../../mappers/patentMapper';
import { parseComplexNumberToString } from '../helpers/documentNumberHelper';
import PaymentReasonEnum from '../enums/PaymentReasonEnum';
import KbkTypesEnum from '../../../../../../../../enums/newMoney/budgetaryPayment/BudgetaryKbkTypesEnum';

class Computed extends CommonOperationStore {
    @override get isKontragentRequisitesHasErrors() {
        const {
            Recipient,
            BankName,
            SettlementAccount,
            BankCorrespondentAccount,
            Inn,
            Kpp,
            Okato,
            Oktmo,
            DocumentDate,
            DocumentNumber
        } = this.validationState;

        return [
            Recipient,
            BankName,
            SettlementAccount,
            BankCorrespondentAccount,
            Inn,
            Kpp,
            Okato,
            Oktmo,
            DocumentDate,
            DocumentNumber
        ].some(field => field.length);
    }

    @override get hasTaxPostings() {
        return true;
    }

    @computed get budgetaryPaymentTypeLabel() {
        return this.model.AccountType === AccountTypeEnum.Taxes.value ? `Тип налога` : `Тип взноса`;
    }

    @computed get taxesOrFeesList() {
        const filterCodeRegexp = this.model.AccountType === AccountTypeEnum.Taxes.value ? /^68/ : /^69/;
        const isAfter2022 = dateHelper(this.model.Date).year() > 2022;
        const cutOffYearFor6806And6811 = 2025;
        const startDateFor6806And6811 = dateHelper(this.model?.Date).isBefore(`01.01.2025`);

        let filtered = this.metaData.Accounts
            .filter(account => filterCodeRegexp.test(account.Code) &&
                (this.hasPatents || account.Code !== SyntheticAccountCodesEnum.patent))
            .filter(el => !((el.Code === SyntheticAccountCodesEnum._68_06 || el.Code === SyntheticAccountCodesEnum._68_11) &&
                        (startDateFor6806And6811 || this.model?.Period?.Year < cutOffYearFor6806And6811)
            ));

        if ((this.isNew || this.isCopy()) && isAfter2022) {
            filtered = filtered.filter(({ Code }) => Code !== SyntheticAccountCodesEnum._68_01);
        }

        return filtered.map(({ Code: value, Name: text, DefaultCalendarType }) => {
            const result = { value, text, DefaultCalendarType };

            if (!this.isNew && isAfter2022 && value === SyntheticAccountCodesEnum._68_01) {
                result.disabled = true;
            }

            return result;
        });
    }

    @computed get isCreatedNDFLin2023() {
        return !this.isNew && this.model.AccountCode === SyntheticAccountCodesEnum._68_01 && dateHelper(this.model.Date).year() > 2022;
    }

    @computed get isNotTaxable() {
        return !!this.getTaxPostingsExplainingMsg() || this.model.TaxPostings.TaxStatus === TaxStatusEnum.NotTax;
    }

    @override get isSavingBlocked() {
        return !!this.model.TaxPostings.Error || this.taxPostingsLoading
            || !!this.model.AccountingPostings.Error || this.accountingPostingsLoading
            || this.savePaymentPending || this.sendToBankPending;
    }

    /** methods for Period component */
    @computed get showSingleCalendar() {
        return this.model.PaymentBase === PaymentReasonEnum.Tr; // основание платежа ТР - погашение задолженности по требованию налоговой
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

    @computed get canShowSecondAutocomplete() {
        return ![CalendarTypesEnum.Year, CalendarTypesEnum.NoPeriod].some(type => type === this.model.Period.Type);
    }

    @computed get canShowYearAutocomplete() {
        return CalendarTypesEnum.NoPeriod !== this.model.Period.Type;
    }

    @computed get yearsList() {
        const yearsList = budgetaryDateHelper.getYearsListForDropdown();

        // для типа накопительная часть трудовой пенсии список годов ограничивается 2013 годом
        if (this.model.AccountCode === SyntheticAccountCodesEnum.pfrAccumulateFee) {
            return yearsList.filter(year => year.value < 2014);
        }

        return yearsList;
    }
    /** methods for Period component END */

    /** methods for KbkDropdown component */
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

    @computed get kbkList() {
        return this.KbkDefault.map(kbk => {
            return {
                value: kbk.Id,
                text: kbk.Name,
                original: kbk
            };
        });
    }

    @computed get isOtherTaxesAndFees() {
        return this.model.AccountCode === SyntheticAccountCodesEnum.otherTaxes; // прочие налоги и сборы
    }
    /** methods for KbkDropdown component END */

    /** methods for PayerStatusDropdown component */
    @computed get statusOfPayers() {
        const result = toJS(this.metaData.StatusOfPayers);

        if (!result.some(status => status.Code === this.model.PayerStatus)) {
            result.push({
                Code: this.model.PayerStatus,
                Description: `${this.currentPayerStatusCode} - Статус плательщика не актуален`,
                IsActive: false
            });
        }

        return result.sort((a, b) => a.Code - b.Code);
    }

    @computed get currentPayerStatusCode() {
        const payerStatus = this.model.PayerStatus?.toString() || ``;
        const codeLength = 2;

        // Подставляем необходимое количество нулей в перед числом (например "9" -> "09")
        if (payerStatus.length < codeLength) {
            const zeros = Array.from(new Array(codeLength - payerStatus.length), () => 0).join(``);

            return `${zeros}${payerStatus}`;
        }

        return payerStatus;
    }

    @computed get currentPayerStatus() {
        return this.statusOfPayers.find(status => status.Code === this.model.PayerStatus);
    }

    @computed get currentPaymentBase() {
        return this.metaData.PaymentReasons.find(status => status.Code === this.model.PaymentBase) || {};
    }

    @computed get payerStatusList() {
        if (this.model.AccountType === AccountTypeEnum.Taxes.value) {
            let result = this.statusOfPayers;

            if (!this.isOtherTaxesAndFees) {
                result = result.filter(status => status.Code !== 8);
            }

            return result.map(status => {
                return {
                    value: status.Code,
                    text: status.Description,
                    disabled: status.IsActive === false,
                    original: status
                };
            });
        }

        const singleStatus = this.currentPayerStatus;

        return [{
            value: singleStatus.Code,
            text: singleStatus.Description,
            original: singleStatus
        }];
    }

    /** methods for PayerStatusDropdown component END */

    @computed get PaymentBaseList() {
        return this.metaData.PaymentReasons.map(reason => {
            return {
                value: reason.Code,
                text: `${reason.Designation} — ${reason.Description}`,
                original: reason
            };
        });
    }

    @computed get PaymentSubBaseList() {
        return this.metaData.PaymentSubReasons.map(reason => {
            return {
                value: reason.Code || 0,
                text: reason.Designation || `—`,
                description: reason.Description,
                original: reason
            };
        });
    }

    /** methods for kbkAutoFields component */
    @computed get kbkAutoFieldsArray() {
        return [
            `Статус плательщика - ${this.currentPayerStatus.Code}`,
            `Основание платежа - ${this.currentPaymentBase.Designation || ``}`,
            `Номер документа - ${this.model.DocumentNumber}`,
            `Дата документа - ${this.model.DocumentDate}`,
            `Код - ${this.model.Uin}`,
            `Получатель - ${this.model.Recipient.Name || ``}`
        ];
    }

    @computed get canEditDocumentNumber() {
        // поле Номер документа нельзя редактировать при основании платежа ТП, ЗД, БФ
        const forbiddenPaymentBases = [1, 11];

        if (!this.model.isComplexDocumentNumber) {
            forbiddenPaymentBases.push(2);
        }

        return !forbiddenPaymentBases.includes(this.model.PaymentBase);
    }

    @computed get canEditDocumentDate() {
        return this.model.isComplexDocumentNumber || this.model.PaymentBase !== PaymentReasonEnum.FreeDebtRepayment;
    }
    /** methods for kbkAutoFields component END */

    @computed get isTradingFee() {
        return this.model.AccountCode === SyntheticAccountCodesEnum.tradingFees;
    }

    @computed get isTaxPostingTabActive() {
        return this.currentPostingsTab === PostingsTabsEnum.tax;
    }

    @computed get canShowTradingFeeAdditionalWarning() {
        return this.isTradingFee && !(this.isEnvd && !this.isOsno && !this.isUsn) && this.isTaxPostingTabActive && !this.isIpOsno;
    }

    @override get patentSelectVisible() {
        return this.model.AccountCode === SyntheticAccountCodesEnum.patent;
    }

    @override get patentSelectData() {
        return [{ value: null, text: `` }].concat(patentMapper.mapPatentsToDropDown(this.patents));
    }

    @computed get hasPatents() {
        return this.patents.length > 0;
    }

    @computed get isIpOsno() {
        return !this.Requisites.IsOoo && this.TaxationSystem.IsOsno;
    }

    @override get description() {
        const {
            AccountCode, Sum, Description, PatentId
        } = this.model;

        if (!this.isNew) {
            return Description;
        }

        if (AccountCode === SyntheticAccountCodesEnum.patent) {
            const patent = this.patents.find(p => p.Id === PatentId);
            const patentInfo = patent
                ? ` N ${patent.Code} от ${patent.StartDate} сроком действия с ${patent.StartDate} по ${patent.EndDate}`
                : ``;
            const sumInfo = Sum
                ? ` Сумма ${toAmountString(Sum || 0)} р. Без НДС.`
                : ``;

            return `${Description.slice(0, -1)}${patentInfo}.${sumInfo}`;
        }

        return Description;
    }

    @override get canEditTaxPostings() {
        return !this.model.CurrencyInvoices?.length > 0;
    }

    @computed get documentNumberForSave() {
        if (this.model.isComplexDocumentNumber) {
            return parseComplexNumberToString(this.model.complexNumber, this.metaData);
        }

        return this.model.DocumentNumber;
    }

    @computed get documentNumberForView() {
        if (this.model.isComplexDocumentNumber) {
            return this.model.complexNumber.value;
        }

        return this.model.DocumentNumber;
    }

    @computed get documentNumberMaxLength() {
        if (this.model.isComplexDocumentNumber && !!this.model.complexNumber.literalCode) {
            return 13;
        }

        return 15;
    }

    @override get integrationPartner() {
        const currentAccount = this.currentSettlementAccount;

        if (!Object.values(currentAccount).length) {
            return null;
        }

        return currentAccount.IntegrationPartner;
    }

    @computed get isUnifiedBudgetaryPayment() {
        return this.model.AccountCode === SyntheticAccountCodesEnum._68_69;
    }

    @computed get isOtherPayment() {
        return this.model.AccountCode === SyntheticAccountCodesEnum._68_10;
    }

    @computed get wasUnifiedBudgetaryPayment() {
        return this.prevAccountCode === SyntheticAccountCodesEnum._68_69;
    }

    @computed get isFeesPayment() {
        return this.model.AccountCode === SyntheticAccountCodesEnum._69_11;
    }

    @computed get wasFeesPayment() {
        return this.prevAccountCode === SyntheticAccountCodesEnum._69_11;
    }

    @computed get wasOldBudgetaryPayment() {
        return (this.prevAccountCode === SyntheticAccountCodesEnum._68_01 || this.prevAccountCode === SyntheticAccountCodesEnum._69_01);
    }

    @computed get wasNotBudgetaryPayment() {
        return this.prevAccountCode === 0;
    }

    @override get isOutsourceUser() {
        const {
            IsTrial, IsPaid, IsExpired, IsProfOutsourceUser
        } = this.UserInfo;

        return (IsTrial || IsPaid) && !IsExpired && IsProfOutsourceUser;
    }

    @computed get kbkTypesRadioData() {
        const { canEdit, isUnifiedBudgetaryPayment, model } = this;
        const data = [];

        if (!isUnifiedBudgetaryPayment) {
            data.push(
                {
                    value: KbkTypesEnum.TaxAndFee,
                    text: model.AccountType === AccountTypeEnum.Taxes.value ? `Налог` : `Взнос`,
                    disabled: !canEdit
                },
                {
                    value: KbkTypesEnum.PenaltyTax,
                    text: `Пени`,
                    disabled: !canEdit
                },
                {
                    value: KbkTypesEnum.PenaltyCharge,
                    text: `Штраф`,
                    disabled: !canEdit
                }
            );
        }

        return data;
    }
}

export default Computed;
