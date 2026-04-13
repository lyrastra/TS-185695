import kontragentService from '@moedelo/frontend-common-v2/apps/kontragents/services/kontragentService';
import { action, override } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import Computed from './Computed';
import validationRules from './../validationRules';
import validator from './../../../../validation/validator';
import defaultModel from './Model';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';
import { mapKontragentToModel } from '../../../../../../../../helpers/newMoney/kontragentHelper';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import {
    mapLinkedDocumentsTaxPostingsToModel,
    osnoTaxPostingsToModel,
    usnTaxPostingsToModel
} from '../../../../../../../../mappers/taxPostingsMapper';
import TaxStatusEnum from '../../../../../../../../enums/TaxStatusEnum';

class Actions extends Computed {
    @override setTaxPostingList({ Postings = [], LinkedDocuments = [], TaxStatus } = {}) {
        const postings = Postings.slice();

        if ((TaxStatus === TaxStatusEnum.ByHand || this.model.TaxPostingsMode === ProvidePostingType.ByHand) && !Postings.length) {
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

    @action setSum = ({ value }) => {
        const sum = toFloat(value);

        if (sum === toFloat(this.model.Sum)) {
            return;
        }

        this.model.Sum = sum;

        this.validateField(`Sum`);
        this.validateField(`LoanInterestSum`);
    };

    @action setLongTermLoan = ({ checked }) => {
        this.model.IsLongTermLoan = checked;
    };

    @action setLoanInterestSum = ({ value }) => {
        const sum = toFloat(value) || null;

        if (sum === toFloat(this.model.LoanInterestSum)) {
            return;
        }

        this.model.LoanInterestSum = sum;

        this.validateField(`LoanInterestSum`);
    };

    @action setContract = ({ model = defaultModel.Contract } = {}) => {
        const { KontragentId, KontragentName } = model;

        this.model.Contract = {
            ContractBaseId: model.ContractBaseId,
            ProjectNumber: model.ProjectNumber,
            KontragentId
        };

        if (KontragentId && KontragentId !== this.model.Kontragent.KontragentId) {
            this.setContractor({ original: { ...defaultModel.Kontragent, KontragentId, KontragentName } });
        }

        if (this.validationState.Contract) {
            this.validateField(`Contract`);
        }
    };

    @override setContractorName({ value }) {
        if (!value) {
            this.setContractor({});

            return;
        }

        if (value !== this.model.Kontragent.KontragentName) {
            this.model.Kontragent.KontragentName = value;
        }
    }

    @action setContractor = async ({ original }) => {
        const previous = this.model.Kontragent;
        const newValues = mapKontragentToModel(original);

        if ((!newValues.KontragentId || (previous.KontragentId && newValues.KontragentId !== previous.KontragentId)) && this.model.Contract.ContractBaseId) {
            this.setContract();
        }

        this.model.Kontragent = newValues;

        if (this.validationState.Kontragent) {
            this.validateField(`Kontragent`);
        }

        await this.loadKontragentNameById();
    };

    @action loadKontragentNameById = async () => {
        const { KontragentId, KontragentName } = this.model.Kontragent;

        if (KontragentId && !KontragentName) {
            const { Name } = await kontragentService.getById({ id: KontragentId });
            this.model.Kontragent.KontragentName = Name;
        }
    };

    @action setDescription = value => {
        this.model.Description = value;
        this.validateField(`Description`);
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

    @override setContractorSettlementAccount({ value }) {
        this.model.Kontragent.KontragentSettlementAccount = value;
        this.validateField(`KontragentSettlementAccount`);
    }

    @action setContractorINN = ({ value }) => {
        this.model.Kontragent.KontragentINN = value;
        this.validateField(`KontragentInn`);
    };

    @action setContractorKPP = ({ value }) => {
        this.model.Kontragent.KontragentKPP = value;
        this.validateField(`KontragentKpp`);
    };

    @action validateTaxPostingsList = () => {
        this.model.TaxPostings.Postings = taxPostingsValidator.getValidatedList(this.model.TaxPostings.Postings, { isOsno: this.isOsno, isIp: this.isIp });
    };
}

export default Actions;
