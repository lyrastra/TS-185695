import kontragentService from '@moedelo/frontend-common-v2/apps/kontragents/services/kontragentService';
import { action, override } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import Computed from './Computed';
import validationRules from './../validationRules';
import validator from './../../../../validation/validator';
import { mapKontragentToModel } from '../../../../../../../../helpers/newMoney/kontragentHelper';

class Actions extends Computed {
    @action setSum = ({ value }) => {
        const sum = toFloat(value);

        if (sum === toFloat(this.model.Sum)) {
            return;
        }

        this.model.Sum = sum;

        this.validateField(`Sum`);
    }

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
    }

    @action loadKontragentNameById = async () => {
        const { KontragentId, KontragentName } = this.model.Kontragent;

        if (KontragentId && !KontragentName) {
            const { Name } = await kontragentService.getById({ id: KontragentId });
            this.model.Kontragent.KontragentName = Name;
        }
    }

    @action setDescription = value => {
        this.model.Description = value;
        this.validateField(`Description`);
    }

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
    }

    @action setContractorKPP = ({ value }) => {
        this.model.Kontragent.KontragentKPP = value;
        this.validateField(`KontragentKpp`);
    }

    @action setAllContractorFields = data => {
        const {
            Id, Name, Inn, Kpp, Number, BankName, Bik, KontragentBankCorrespondentAccount
        } = data;

        this.model.Kontragent = {
            KontragentId: Id,
            KontragentName: Name,
            KontragentINN: Inn,
            KontragentKPP: Kpp,
            KontragentSettlementAccount: Number,
            KontragentBankName: BankName,
            KontragentBankBIK: Bik,
            KontragentBankCorrespondentAccount
        };
    }
}

export default Actions;
