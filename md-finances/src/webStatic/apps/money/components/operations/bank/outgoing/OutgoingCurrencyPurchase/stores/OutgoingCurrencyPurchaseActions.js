import { action, override } from 'mobx';
import { toAmountString, toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import validationRules from '../validationRules';
import validator from '../../../../validation/validator';
import OutgoingCurrencyPurchaseComputed from './OutgoingCurrencyPurchaseComputed';
import { getCurrencyRate } from '../../../../../../../../services/newMoney/currencyService';
import { round } from '../../../../../../../../helpers/numberConverter';
import TaxStatusEnum from '../../../../../../../../enums/TaxStatusEnum';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import {
    mapLinkedDocumentsTaxPostingsToModel,
    osnoTaxPostingsToModel,
    usnTaxPostingsToModel
} from '../../../../../../../../mappers/taxPostingsMapper';

class OutgoingCurrencyPurchaseActions extends OutgoingCurrencyPurchaseComputed {
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

    @override editTaxPostingList(list) {
        this.model.TaxPostingsMode = ProvidePostingType.ByHand;

        this.setTaxPostingList({ ...this.model.TaxPostings, Postings: list, TaxStatus: TaxStatusEnum.ByHand });
    }

    @override async setDate({ value }) {
        if (this.model.Date !== value) {
            this.model.Date = value;
            this.validateField(`Date`);
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
        this.calculateTotalSumAndExchangeRateDiff();
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
        this.calculateTotalSumAndExchangeRateDiff();
        this.updateDescription();
        this.validateField(`Sum`);
    };

    @action setExchangeRate = ({ value }) => {
        const rate = toFloat(value);

        if (rate === this.model.ExchangeRate) {
            return;
        }

        this.model.ExchangeRate = rate;
        this.calculateTotalSumAndExchangeRateDiff();
        this.updateDescription();
        this.validateField(`ExchangeRate`);
    };

    @action setRateByCentralBank = () => {
        this.setExchangeRate({ value: this.model.CentralBankRate });
    };

    @action calculateTotalSumAndExchangeRateDiff = () => {
        const totalSum = round(this.model.Sum / this.model.ExchangeRate);
        const exchangeRateDiff = round((totalSum * this.model.CentralBankRate) - this.model.Sum, 2);
        this.model.TotalSum = totalSum;
        this.model.ExchangeRateDiff = exchangeRateDiff;
        this.updateDescription();
    };

    @action updateDescription = () => {
        if (!this.isNew && !this.isCopy()) {
            return;
        }

        const value = this.model.ExchangeRateDiff > 0 && this.model.Sum && this.model.ExchangeRate
            ? `Покупка валюты по курсу ${toAmountString(this.model.ExchangeRate, 2, true)}, сумма положительной курсовой разницы ${toAmountString(this.model.ExchangeRateDiff, 2, true)} руб. НДС не облагается.`
            : `Покупка валюты. НДС не облагается.`;

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

export default OutgoingCurrencyPurchaseActions;
