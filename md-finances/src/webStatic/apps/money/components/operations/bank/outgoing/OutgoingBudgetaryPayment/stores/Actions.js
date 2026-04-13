import { action, override, runInAction } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import KontragentsFormEnum from '@moedelo/frontend-enums/mdEnums/KontragentsForm';
import Computed from './Computed';
import validationRules from './../validationRules';
import validator from './../../../../validation/validator';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';
import CalendarTypesEnum from '../../../../../../../../enums/newMoney/budgetaryPayment/BudgetaryCalendarTypesEnum';
import { removeCityFromBankName } from '../../../../../../../../helpers/newMoney/budgetaryPayment/budgetaryKontragentHelper';
import AccountTypeEnum from '../../../../../../../../enums/newMoney/budgetaryPayment/BudgetaryAccountTypeEnum';
import { getTradingObjects } from '../../../../../../../../services/newMoney/tradingObjectService';
import SyntheticAccountCodesEnum from '../../../../../../../../enums/SyntheticAccountCodesEnum';
import TaxStatusEnum from '../../../../../../../../enums/TaxStatusEnum';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import { mapLinkedDocumentsTaxPostingsToModel, osnoTaxPostingsToModel, usnTaxPostingsToModel } from '../../../../../../../../mappers/taxPostingsMapper';
import { VALID_RU_SETTLEMENT_NUMBER_LENGTH } from '../../../../../../../../constants/requisitesConstants';
import PaymentReasonEnum from '../enums/PaymentReasonEnum';
import KbkTypesEnum from '../../../../../../../../enums/newMoney/budgetaryPayment/BudgetaryKbkTypesEnum';
import { isTPPaymentFoundationSelected, isTPPaymentFoundationTouched } from '../helpers/paymentFoundationHelper';
import { getUnifiedBudgetaryPaymentBudgetaryMetadataAsync } from '../../../../../../../../services/newMoney/budgeratyMetadataService';

class Actions extends Computed {
    @override setDate({ value }) {
        this.prevDate = this.model.Date;

        if (this.model.Date !== value) {
            this.model.Date = value;
            this.validateField(`Date`);

            const isValidDate = dateHelper(this.initialDate).isSameOrBefore(dateHelper(this.model.Date));
            this.isShowApprove = isValidDate && this.isOutsourceUser;

            if (this.model.Recipient.SettlementAccount.length === VALID_RU_SETTLEMENT_NUMBER_LENGTH) {
                this.validateField(`SettlementAccount`);
                this.validateField(`BankCorrespondentAccount`);
            }

            if (this.model.AccountCode === SyntheticAccountCodesEnum._68_69) {
                this.UnifiedBudgetaryPaymentStore?.reloadDescription();
            }
        }
    }

    /** isDeleting нужно для очищения последней НУ записи из списка
     * при клике на иконку удаления */
    @override setTaxPostingList(TaxPostings = {}, { isDeleting = false } = {}) {
        const { TaxStatus, Postings, LinkedDocuments } = this.model.TaxPostings;
        const taxStatusByHand = TaxStatus === TaxStatusEnum.ByHand;
        const list = TaxPostings?.Postings || [];
        let postings = list.slice();

        /** костыль для сохранения ручных записей НУ при изменении полей кбк, периода и т.д. */
        if ((this.model.TaxPostingsMode === ProvidePostingType.ByHand || taxStatusByHand) && !list.length) {
            this.model.TaxPostingsMode = TaxStatusEnum.ByHand;

            if (!Postings.length || isDeleting) {
                postings.push({});
            } else {
                postings = Postings;
            }
        }

        this.model.TaxPostings =
            {
                ...Postings,
                Postings: this.taxationForPostings.IsOsno && this.Requisites.IsOoo
                    ? osnoTaxPostingsToModel(postings, { OperationDirection: this.model.Direction })
                    : usnTaxPostingsToModel(postings, { OperationDirection: this.model.Direction }),
                ExplainingMessage: this.model.TaxPostings.ExplainingMessage,
                HasPostings: this.model.TaxPostings.HasPostings || postings.length > 0,
                TaxStatus: this.model.TaxPostings.TaxStatus,
                LinkedDocuments: mapLinkedDocumentsTaxPostingsToModel({ postings: LinkedDocuments, isOsno: this.isOsno, isOoo: this.Requisites.IsOoo })
            };
    }

    @override editTaxPostingList(list, options = {}) {
        this.model.TaxPostingsMode = ProvidePostingType.ByHand;

        this.setTaxPostingList({ Postings: list }, options);
    }

    @override checkKontragentRequisitesVisibility() {
        if (this.isKontragentRequisitesHasErrors) {
            this.isContractorRequisitesShown = true;
            this.isFullKbkAutoFieldsShown = true;
        }
    }

    @action showFullKbkAutoFields = () => {
        this.isFullKbkAutoFieldsShown = true;
    };

    @action setSum = ({ value }) => {
        const sum = toFloat(value);

        if (sum === toFloat(this.model.Sum)) {
            return;
        }

        this.model.Sum = sum;

        this.validateField(`Sum`);
    };

    @action setDescription = (value, { needValidate = true } = {}) => {
        this.model.Description = value;

        if (needValidate) {
            this.validateField(`Description`);
        }
    };

    @action validateField(validationField) {
        const rules = validationRules[validationField];

        if (!rules) {
            return;
        }

        const requisites = this.Requisites;
        const { model } = this;
        let data = {
            store: this
        };

        if (this.isUnifiedBudgetaryPayment && this.UnifiedBudgetaryPaymentStore) {
            const { subPaymentsTotalSum } = this.UnifiedBudgetaryPaymentStore;

            data = {
                isUnifiedBudgetaryPayment: this.isUnifiedBudgetaryPayment,
                subPaymentsTotalSum
            };
        }

        this.validationState[validationField] = validator({
            model, rules, requisites, data
        });
    }

    @action validateTaxPostingsList = () => {
        this.model.TaxPostings.Postings = taxPostingsValidator.getValidatedList(this.model.TaxPostings.Postings, { isOsno: this.isOsno, isIp: this.isIp });
    };

    @override setContractorName({ value }) {
        this.model.Recipient.Name = value;
        this.validateField(`Recipient`);
    }

    @override setContractorSettlementAccount({ value }) {
        this.model.Recipient.SettlementAccount = value;
        this.validateField(`SettlementAccount`);
        value.length === VALID_RU_SETTLEMENT_NUMBER_LENGTH && this.validateField(`BankCorrespondentAccount`);
    }

    @override setContractorBankCorrespondentAccount({ value }) {
        this.model.Recipient.BankCorrespondentAccount = value;
        this.validateField(`BankCorrespondentAccount`);
    }

    @action setContractorINN = ({ value }) => {
        this.model.Recipient.Inn = value;
        this.validateField(`Inn`);
    };

    @action setContractorKPP = ({ value }) => {
        this.model.Recipient.Kpp = value;
        this.validateField(`Kpp`);
    };

    @action setContractorBankNameAndBik = ({ original = {} }) => {
        this.model.Recipient.BankName = original.Name;
        this.model.Recipient.BankBik = original.Bik;
        this.validateField(`BankName`);
    };

    @action loadMetadata = async () => {
        const result = await this.getMetadata();

        if (this.isUnifiedBudgetaryPayment) {
            const { Accounts, ...rest } = await getUnifiedBudgetaryPaymentBudgetaryMetadataAsync();

            Object.assign(result, rest);
        }

        runInAction(() => {
            this.metaData = result;
            !result.Accounts.some(account => account.Code === this.model.AccountCode) && this.onChangeAccountCode({ value: result.Accounts[0].Code });
        });
    }

    /** NEW BUDGETARY ACTIONS */
    @action initBudgetaryModel = async () => {
        const nextNumberForCopyingObj = {};
        const tradingObjects = await getTradingObjects();
        const defaultFields = this.getDefaultFieldsForBudgetaryModel(this.model);

        await this.loadMetadata();
        this.handleDocumentNumberComplexity();
        this.setTradingObjectListForDropdown(tradingObjects);
        this.initComplexDocumentNumber();
        this.initPeriod();
        await this.checkAndInitUnifiedBudgetaryPaymentStore();

        runInAction(() => {
            Object.assign(this.model, defaultFields, nextNumberForCopyingObj);
            Object.assign(
                this.model.Recipient,
                {
                    Form: KontragentsFormEnum.UL,
                    BankName: removeCityFromBankName(this.model.Recipient.BankName)
                }
            );
        });
    };

    @action initPeriod = () => {
        const curMoment = dateHelper();
        const Quarter = curMoment.quarter();
        const HalfYear = Quarter < 3 ? 1 : 2;
        const Month = curMoment.month() + 1;
        const Year = curMoment.year();
        const Type = CalendarTypesEnum.Month;

        const period = this.model.Period;

        this.model.Period = {
            Type: period?.Type || this.taxesOrFeesList[0].DefaultCalendarType || Type,
            CanEditCalendarType: period?.CanEditCalendarType || true,
            Date: period?.Date || this.model.Date || curMoment.format(`DD.MM.YYYY`),
            MinDate: period?.MinDate || `01.01.2011`,
            readOnly: period?.readOnly || false,
            HalfYear: period?.HalfYear || HalfYear,
            Quarter: period?.Quarter || Quarter,
            Month: period?.Month || Month,
            Year: period?.Year || Year
        };
    };

    @action setAccountType = ({ value }) => {
        this.model.AccountType = value;
    };

    @action onChangeAccountCode = async ({ value }) => {
        if (this.model.AccountCode === SyntheticAccountCodesEnum._68_69 && this.validationState.Sum) {
            this.validationState.Sum = ``;
        }

        this.prevAccountCode = this.model.AccountCode;
        this.model.AccountCode = value;
        this.model.TaxPostings.Postings = [];

        [this.prevAccountCode, value].includes(SyntheticAccountCodesEnum._68_69) && await this.loadMetadata();

        if (value === SyntheticAccountCodesEnum._68_69) {
            this.setKbkPaymentType({ value: KbkTypesEnum.TaxAndFee });
            await this.checkAndInitUnifiedBudgetaryPaymentStore();
            this.UnifiedBudgetaryPaymentStore?.reloadDescription();
        }
    };

    @action setKbkPaymentType = ({ value }) => {
        this.model.KbkPaymentType = value;
    };

    /** methods for Period component */
    @action setSecondAutocompleteValue = ({ value }) => {
        switch (this.model.Period.Type) {
            case CalendarTypesEnum.Month:
                this.model.Period.Month = value;

                break;
            case CalendarTypesEnum.Quarter:
                this.model.Period.Quarter = value;

                break;
            case CalendarTypesEnum.HalfYear:
                this.model.Period.HalfYear = value;

                break;
        }
    };

    @action setPeriodDate = ({ value }) => {
        this.model.Period.Date = value;
        this.validateField(`Period`);
    };

    @action setPeriodCalendarType = ({ value }) => {
        this.model.Period.Type = value;
    };

    @action setPeriodYear = ({ value }) => {
        this.model.Period.Year = value;
    };

    /** для определенных типов налога нужно менять тип поля период */
    @action setDefaultCalendarType = value => {
        if (value) {
            this.model.Period.Type = value;

            return;
        }

        if (this.isTPPaymentFoundationTouched) {
            const isAccountTypeFees = this.model.AccountType === AccountTypeEnum.Fees.value;
            const { ndfl, propertyTaxes, otherTaxes } = SyntheticAccountCodesEnum;

            if (isAccountTypeFees || [ndfl, propertyTaxes, otherTaxes].includes(this.model.AccountCode)) {
                this.model.Period.Type = CalendarTypesEnum.Month;
            } else {
                this.model.Period.Type = CalendarTypesEnum.Quarter;
            }
        }
    };
    /** methods for Period component END */

    /** methods for KbkDropdown component */
    @action setKbkDefault = list => {
        this.KbkDefault = list;
    };

    @action setKbkAutoFields = (autoFields = {}, { setOnlyNull = false } = {}) => {
        const {
            Recipient = {}, DocumentDate, DocumentNumber, ...fields
        } = autoFields;

        if (setOnlyNull) {
            Object.keys(fields).forEach(key => {
                if (!this.model[key] || this.model[key] === null) {
                    this.model[key] = fields[key] || 0;
                }
            });
        } else {
            Object.keys(fields).forEach(key => {
                this.model[key] = fields[key] || 0;
            });
        }

        Object.assign(this.model.Recipient, { ...Recipient });

        this.validateField(`Recipient`);
        this.validateField(`BankName`);
        this.validateField(`Description`);
        this.checkKontragentRequisitesVisibility();
    };

    @action setKbk = ({ value }) => {
        this.model.Kbk.Id = value;
        this.model.Kbk.Number = this.currentKbkNumber;
        this.validateField(`KBK`);
    };

    @action setKbkNumber = ({ value }) => {
        this.model.Kbk.Number = value;
        this.validateField(`KBK`);
    };

    @action resetKbk = () => {
        this.model.Kbk = {
            Id: null,
            Number: null
        };
    };
    /** methods for KbkDropdown component END */

    /** methods for PayerStatusDropdown component */
    @action setPayerStatus = ({ value }) => {
        this.model.PayerStatus = value;
    };
    /** methods for PayerStatusDropdown component END */

    @action setOktmoOkato105 = ({ value }) => {
        if (dateHelper(this.model.Date, `DD.MM.YYYY`).isBefore(`2014-01-01`)) {
            this.model.Recipient.Okato = value;
            this.validateField(`Okato`);
        } else {
            this.model.Recipient.Oktmo = value;
            this.validateField(`Oktmo`);
        }
    };

    @action setPaymentBase = ({ value }) => {
        const { PaymentBase } = this.model;
        this.model.PaymentBase = value;
        this.onPaymentBaseChange({ oldValue: PaymentBase, newValue: value });
    };

    @action onPaymentBaseChange = ({ oldValue, newValue }) => {
        if (oldValue === newValue) {
            return;
        }

        const { complexNumber: { literalCode }, isComplexDocumentNumber } = this.model;

        this.updateIsTPPaymentFoundationTouched({
            oldPaymentBase: oldValue,
            newPaymentBase: newValue,
            oldLiteralCode: literalCode,
            newLiteralCode: literalCode,
            isComplexDocumentNumber
        });
    };

    @action setDocumentNumber = ({ value }) => {
        if (this.model.isComplexDocumentNumber) {
            this.setComplexNumberValue({ value });

            return;
        }

        this.model.DocumentNumber = value;
    };

    @action setDocumentDate = ({ value }) => {
        this.model.DocumentDate = value;
        this.validateField(`DocumentDate`);
    };

    @action setUin = ({ value }) => {
        this.model.Uin = value;
        this.validateField(`Uin`);
    };

    @action setTradingObjectListForDropdown = (list = []) => {
        const mappedList = list.map(({ Id, Name, Number }) => ({
            text: `${Name}${Number ? ` № ${Number}` : ``}`,
            value: Id
        }));

        this.TradingObjectList.push(...mappedList);
    };

    @action setTradingObjectId = ({ value }) => {
        this.model.TradingObjectId = value === 0 ? null : value;
    };

    @action resetLinkedCurrencyInvoices = () => {
        this.model.CurrencyInvoices = [];
        this.model.TaxPostings.Postings = [];
    }

    @action setPaymentReasons = ({ PaymentReasons = [], PaymentSubReasons = [], Accounts = [] } = {}) => {
        this.metaData.PaymentReasons = PaymentReasons;
        this.metaData.PaymentSubReasons = PaymentSubReasons;
        this.metaData.Accounts = Accounts;
    };

    @action handleDocumentNumberComplexity = () => {
        const dateObj = dateHelper(this.model.Date, undefined, true);

        const newNumberFormatDate = `01.10.2021`;

        const isComplexDocumentNumber = dateObj.isValid() &&
            this.model.PaymentBase === PaymentReasonEnum.FreeDebtRepayment &&
            dateObj.isSameOrAfter(newNumberFormatDate);

        this.model.isComplexDocumentNumber = isComplexDocumentNumber;

        if (!isComplexDocumentNumber) {
            this.setComplexNumberLiteralCode({ value: 0 });
        }
    };

    @action setComplexNumberLiteralCode = ({ value }) => {
        const { literalCode } = this.model.complexNumber;

        this.model.complexNumber.literalCode = value;
        this.onComplexNumberLiteralCodeChange({ oldValue: literalCode, newValue: value });

        this.validateField(`DocumentNumber`);
        this.validateField(`DocumentDate`);
    };

    @action onComplexNumberLiteralCodeChange = ({ oldValue, newValue }) => {
        if (oldValue === newValue) {
            return;
        }

        const { PaymentBase, isComplexDocumentNumber } = this.model;

        this.updateIsTPPaymentFoundationTouched({
            oldPaymentBase: PaymentBase,
            newPaymentBase: PaymentBase,
            oldLiteralCode: oldValue,
            newLiteralCode: newValue,
            isComplexDocumentNumber
        });
    };

    @action updateIsTPPaymentFoundationTouched = ({
        oldPaymentBase, newPaymentBase, oldLiteralCode, newLiteralCode, isComplexDocumentNumber
    }) => {
        this.isTPPaymentFoundationTouched = isTPPaymentFoundationTouched({
            oldPaymentBase,
            newPaymentBase,
            oldLiteralCode,
            newLiteralCode,
            isComplexDocumentNumber
        });

        this.onTPPaymentFoundationTouchedChange({
            paymentBase: newPaymentBase,
            literalCode: newLiteralCode,
            isComplexDocumentNumber
        });
    };

    @action onTPPaymentFoundationTouchedChange = ({ paymentBase, literalCode, isComplexDocumentNumber }) => {
        const isTPSelected = isTPPaymentFoundationSelected({
            paymentBase,
            literalCode,
            isComplexDocumentNumber
        });

        if (isTPSelected) {
            this.model.Period.Type = CalendarTypesEnum.Date;
        } else {
            this.setDefaultCalendarType();
        }
    };

    @action setComplexNumberValue = ({ value }) => {
        this.model.complexNumber.value = value;
        this.validateField(`DocumentNumber`);
    };
}

export default Actions;
