import { action, override } from 'mobx';
import { toAmountString, toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import validationRules from '../validationRules';
import validator from '../../../../validation/validator';
import OutgoingCurrencySaleComputed from './OutgoingCurrencySaleComputed';
import { getCurrencyRate } from '../../../../../../../../services/newMoney/currencyService';
import { round } from '../../../../../../../../helpers/numberConverter';
import TaxStatusEnum from '../../../../../../../../enums/TaxStatusEnum';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import {
    mapLinkedDocumentsTaxPostingsToModel,
    osnoTaxPostingsToModel,
    usnTaxPostingsToModel
} from '../../../../../../../../mappers/taxPostingsMapper';

class OutgoingCurrencySaleActions extends OutgoingCurrencySaleComputed {
    /* todo: во всех местах проводки должны смотреть на TaxStatus. В дальнейшем нужно зарефачить и провести регресс */
    @override setTaxPostingList({ Postings = [], LinkedDocuments = [], TaxStatus } = {}) {
        const postings = Postings.slice();

        if (TaxStatus === TaxStatusEnum.ByHand && !Postings.length) {
            this.model.TaxPostingsMode = ProvidePostingType.ByHand;
            postings.push({});
        }

        this.model.TaxPostings =
            {
                ...this.model.TaxPostings.Postings,
                Postings: this.taxationForPostings.IsOsno && this.Requisites.IsOoo
                    ? osnoTaxPostingsToModel(postings, { OperationDirection: this.model.Direction })
                    : usnTaxPostingsToModel(postings, { OperationDirection: this.model.Direction }),
                LinkedDocuments: mapLinkedDocumentsTaxPostingsToModel({ postings: LinkedDocuments, isOsno: this.isOsno, isOoo: this.Requisites.IsOoo }),
                ExplainingMessage: this.model.TaxPostings.ExplainingMessage,
                // this.model.TaxPostings.HasPostings от старых щей, после перевода всех операций, от него избавиться
                HasPostings: this.model.TaxPostings.HasPostings || postings.length > 0,
                TaxStatus
            };
    }

    @override async setDate({ value }) {
        if (this.model.Date !== value) {
            this.model.Date = value;
            this.validateField(`Date`);
            const isValidDate = dateHelper(this.initialDate).isSameOrBefore(dateHelper(this.model.Date));
            this.isShowApprove = isValidDate && this.isOutsourceUser;
        }

        if (!this.validationState.Date) {
            this.setTaxationSystem(await taxationSystemService.getTaxSystem(this.model.Date));
            this.updateCurrencyRate();
            this.updateDescription();
        }
    }

    @action updateCurrencyRate = async () => {
        this.model.CentralBankRate = await getCurrencyRate({
            date: this.model.Date,
            currencyCode: this.currencyCode
        });
        this.calculateTotalSum();
    };

    @action setToSettlementAccount = ({ value }) => {
        if (value !== this.model.ToSettlementAccountId) {
            this.model.ToSettlementAccountId = value;
            this.validateField(`ToSettlementAccountId`);

            this.updateCurrencyRate();
            this.updateDescription();
        }
    };

    @action setSum = ({ value }) => {
        const sum = toFloat(value);

        if (sum === this.model.Sum) {
            return;
        }

        this.model.Sum = sum;
        this.calculateTotalSum();
        this.updateDescription();
        this.validateField(`Sum`);
    };

    @action setExchangeRate = ({ value }) => {
        const rate = toFloat(value);

        if (rate === this.model.ExchangeRate) {
            return;
        }

        this.model.ExchangeRate = rate;
        this.calculateTotalSum();
        this.updateDescription();
        this.validateField(`ExchangeRate`);
    };

    @action setRateByCentralBank = () => {
        this.setExchangeRate({ value: this.model.CentralBankRate });
    };

    @action calculateTotalSum = () => {
        const exchangeRateDiff = this.model.Sum * (this.model.ExchangeRate - this.model.CentralBankRate);
        const totalSum = this.model.Sum * this.model.ExchangeRate;
        this.model.TotalSum = round(totalSum, 2);
        this.model.ExchangeRateDiff = round(exchangeRateDiff, 2);
        this.updateDescription();
    };

    @action updateDescription = () => {
        if (!this.isNew && !this.isCopy()) {
            return;
        }

        const value = this.model.ExchangeRateDiff > 0 && this.model.Sum && this.model.ExchangeRate
            ? `Продажа валюты по курсу ${toAmountString(this.model.ExchangeRate, 2, true)}, сумма положительной курсовой разницы ${toAmountString(this.model.ExchangeRateDiff, 2, true)} руб. НДС не облагается.`
            : `Продажа валюты. НДС не облагается.`;

        this.setDescription(value);
    };

    @action setDescription = value => {
        if (this.model.Description !== value) {
            this.model.Description = value;
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

        this.validationState[validationField] = validator({
            model, rules, requisites
        });
    }
}

export default OutgoingCurrencySaleActions;
