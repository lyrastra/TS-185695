import { action, override } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import Computed from './Computed';
import validationRules from './../validationRules';
import validator from './../../../../validation/validator';
import TaxStatusEnum from '../../../../../../../../enums/TaxStatusEnum';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import {
    mapLinkedDocumentsTaxPostingsToModel,
    osnoTaxPostingsToModel,
    usnTaxPostingsToModel
} from '../../../../../../../../mappers/taxPostingsMapper';

class Actions extends Computed {
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

    @action setSum = ({ value }) => {
        const sum = toFloat(value);

        if (sum === toFloat(this.model.Sum)) {
            return;
        }

        this.model.Sum = sum;

        this.validateField(`Sum`);
    };

    @action setWorker = ({ value }) => {
        const { Id, Name } = value || {};

        this.model.SalaryWorkerId = Id;
        this.model.WorkerName = Name;

        if (this.validationState.Worker) {
            this.validateField(`Worker`);
        }
    };

    @action clearAdvanceStatements = () => {
        this.model.AdvanceStatements = [];
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
}

export default Actions;
