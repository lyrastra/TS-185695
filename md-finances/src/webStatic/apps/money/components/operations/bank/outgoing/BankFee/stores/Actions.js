import { action, override } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import Computed from './Computed';
import {
    mapLinkedDocumentsTaxPostingsToModel,
    osnoTaxPostingsToModel,
    usnTaxPostingsToModel
} from '../../../../../../../../mappers/taxPostingsMapper';
import validationRules from './../validationRules';
import validator from './../../../../validation/validator';
import TaxStatusEnum from '../../../../../../../../enums/TaxStatusEnum';
import ProvidePostingTypeEnum from '../../../../../../../../enums/ProvidePostingTypeEnum';

class Actions extends Computed {
    @override setTaxPostingList({ Postings = [], LinkedDocuments = [], TaxStatus } = {}) {
        const postings = Postings.slice();

        if (TaxStatus === TaxStatusEnum.ByHand && !Postings.length) {
            this.model.TaxPostingsMode = ProvidePostingTypeEnum.ByHand;
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
                HasPostings: this.model.TaxPostings.HasPostings || postings.length > 0,
                TaxStatus: this.model.TaxPostings.TaxStatus
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
