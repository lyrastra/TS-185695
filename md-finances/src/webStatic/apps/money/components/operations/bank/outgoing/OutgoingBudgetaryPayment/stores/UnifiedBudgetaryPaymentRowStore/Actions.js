import { action } from 'mobx';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import Computed from './Computed';
import CalendarTypesEnum from '../../../../../../../../../enums/newMoney/budgetaryPayment/BudgetaryCalendarTypesEnum';
import ProvidePostingType from '../../../../../../../../../enums/ProvidePostingTypeEnum';
import PostingDirection from '../../../../../../../../../enums/newMoney/TaxPostingDirectionEnum';
import SyntheticAccountCodesEnum from '../../../../../../../../../enums/SyntheticAccountCodesEnum';
import {
    mapLinkedDocumentsTaxPostingsToModel,
    osnoTaxPostingsToModel,
    usnTaxPostingsToModel
} from '../../../../../../../../../mappers/taxPostingsMapper';
import validator from '../../../../../validation/validator';
import validationRules from './validation/validationRules';
import taxPostingsValidator from '../../../../../validation/taxPostingsValidator';
import { getPeriodByKbkOrAccountCode } from '../../helpers/periodAutofillHelper';
import { getUITaxPostings } from './helpers/taxPostingsHelper';
import { getAccountCodesAsync } from '../UnifiedBudgetaryPaymentStore/services/unifiedBudgetaryPaymentService';

export default class Actions extends Computed {
    @action autoFillPeriod = () => {
        this.setPeriod({ ...this.model.Period, Type: getPeriodByKbkOrAccountCode(this.model.AccountCode, this.currentKbkData?.Name || ``) });
    }

    @action onChangeAccountCode = async ({ value }) => {
        this.model.AccountCode = value || this.accountCodes[0].Code;
        this.model.TaxPostings.Postings = [];
        this.isSomeFieldDirty = true;

        this.autoFillPeriod();

        if (value !== SyntheticAccountCodesEnum.patent && this.model.PatentId) {
            this.model.PatentId = null;
        }

        if (value !== SyntheticAccountCodesEnum._68_09) {
            this.model.TradingObjectId = null;
        }

        if (value === SyntheticAccountCodesEnum.patent && this.patents.length === 1) {
            this.setPatentId({ value: this.patents[0].Id });
        }

        if (value === SyntheticAccountCodesEnum._68_09 && this.TradingObjectList.length === 2) {
            this.setTradingObjectId({ value: this.TradingObjectList[1].value });
        }

        !!this.validationState.AccountCode && this.validateField(`AccountCode`);
        this.validateField(`TradingObject`);
        this.validateField(`Patent`);

        this.loadKbkList();
    };

    @action initAccountCode = () => {
        this.model.AccountCode = this.accountCodes[0].Code;
    }

    @action setSum = ({ value }) => {
        const sum = toFloat(value);

        if (sum === toFloat(this.model.Sum)) {
            return;
        }

        this.isSomeFieldDirty = true;
        this.model.Sum = sum;
        this.unifiedBudgetaryPaymentStore.onChangeSubPaymentSum();
        this.handleTaxPostingsValidation();
        !!this.validationState.Sum && this.validateField(`Sum`);
    };

    @action handleTaxPostingsValidation = () => {
        const { Postings } = this.model.TaxPostings;

        if (!Postings.length) {
            return;
        }

        this.model.TaxPostings.Postings = Postings.map(posting => {
            const AllSumValidationMessage = taxPostingsValidator.getUnifiedBudgetarySubPaymentValidation([posting], { Sum: this.model.Sum });

            return { ...posting, AllSumValidationMessage };
        });
    }

    @action setPeriod = period => {
        const curMoment = dateHelper();
        const Quarter = curMoment.quarter();
        const HalfYear = Quarter < 3 ? 1 : 2;
        const Month = curMoment.month() + 1;
        const Year = curMoment.year();

        this.model.Period = {
            Type: period?.Type || CalendarTypesEnum.Month,
            HalfYear: period?.HalfYear || HalfYear,
            Quarter: period?.Quarter || Quarter,
            Month: period?.Month || Month,
            Year: period?.Year || Year
        };
    }

    @action setKbk = ({ value }) => {
        this.model.Kbk.Id = value;
        this.model.Kbk.Number = this.currentKbkNumber;
        this.isSomeFieldDirty = true;

        this.autoFillPeriod();

        !!this.validationState.KBK && this.validateField(`KBK`);
    };

    @action setPeriodCalendarType = ({ value }) => {
        this.model.Period.Type = value;
    }

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

    @action setPeriodYear = ({ value }) => {
        this.model.Period.Year = value;
    };

    @action validateAll = () => {
        if (!this.isSomeFieldDirty) {
            return;
        }

        this.validateField(`AccountCode`);
        this.validateField(`KBK`);
        this.validateField(`Sum`);
        this.validateField(`Patent`);
        this.validateField(`TradingObject`);
    }

    @action setTradingObjectId = ({ value }) => {
        this.model.TradingObjectId = value === 0 ? null : value;
        this.validateField(`TradingObject`);
    };

    @action setPatentId = ({ value }) => {
        this.model.PatentId = value;
        this.validateField(`Patent`);
    };

    @action setTaxPostingLoading = value => {
        this.taxPostingsLoading = !!value;
    };

    /** isDeleting нужно для очищения последней НУ записи из списка
     * при клике на иконку удаления */
    @action setTaxPostingList(TaxPostings = {}, { isDeleting = false } = {}) {
        const { Postings, LinkedDocuments } = this.model.TaxPostings;
        const { postings, mode } = getUITaxPostings({
            model: this.model,
            TaxPostings,
            isDeleting
        });

        if (mode) {
            this.model.TaxPostingsMode = mode;
        }

        this.model.TaxPostings =
            {
                ...Postings,
                Postings: this.taxationForPostings.IsOsno && this.Requisites.IsOoo
                    ? osnoTaxPostingsToModel(postings, { OperationDirection: PostingDirection.Outgoing })
                    : usnTaxPostingsToModel(postings, { OperationDirection: PostingDirection.Outgoing }),
                ExplainingMessage: this.model.TaxPostings.ExplainingMessage,
                HasPostings: this.model.TaxPostings.HasPostings || postings.length > 0,
                TaxStatus: this.model.TaxPostings.TaxStatus,
                LinkedDocuments: mapLinkedDocumentsTaxPostingsToModel({ postings: LinkedDocuments, isOsno: this.isOsno, isOoo: this.Requisites.IsOoo })
            };
    }

    @action editTaxPostingList = (list, options = {}) => {
        this.model.TaxPostingsMode = ProvidePostingType.ByHand;

        this.isSomeFieldDirty = true;

        this.setTaxPostingList({ Postings: list }, options);
    };

    @action validateField(validationField) {
        const rules = validationRules[validationField];

        if (!rules) {
            return;
        }

        const { model, budgetaryPaymentModel } = this;

        this.validationState[validationField] = validator({
            model, rules, data: { budgetaryPaymentModel }
        });
    }

    @action validateTaxPostingsList = () => {
        //
        this.model.TaxPostings.Postings = taxPostingsValidator.getValidatedList(this.model.TaxPostings.Postings, { isOsno: this.isOsno, isIp: this.isIp });
    };

    @action setAccountCodes = async () => {
        this.accountCodes = await getAccountCodesAsync();
    }
}
