import { action, override } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import kontragentService from '@moedelo/frontend-common-v2/apps/kontragents/services/kontragentService';
import KontragentsFormEnum from '@moedelo/frontend-enums/mdEnums/KontragentsForm';
import defaultModel from '../../PaymentFromBuyer/stores/PaymentFromBuyerModel';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';
import accountingPostingsValidator from '../../../../validation/accountingPostingsValidator';
import { mapDocumentsToModel } from '../../../../../../../../helpers/newMoney/linkedDocumentsHelper';
import validationRules from '../validationRules';
import { mapKontragentToModel } from '../../../../../../../../helpers/newMoney/kontragentHelper';
import validator from '../../../../validation/validator';
import Computed from './Computed';
import SyntheticAccountCodesEnum from '../../../../../../../../enums/SyntheticAccountCodesEnum';

class Actions extends Computed {
    @action setNumber = ({ value }) => {
        if (this.model.Number !== value) {
            this.model.Number = value;
            this.validateField(`Number`);
        }
    };

    @action setNdsType = ({ value }) => {
        this.model.NdsType = value === ` ` ? null : value;
    };

    @action setIncludeNds = ({ checked }) => {
        if (!this.model.IncludeNds && checked) {
            this.setNdsType({ value: this.ndsTypes[0].value });
        }

        this.model.IncludeNds = checked;
    };

    @override setNdsSum({ value }) {
        this.model.NdsSum = toFloat(value) || ``;

        this.validateField(`NdsSum`);
    }

    @action setSum = ({ value }) => {
        const sum = toFloat(value);

        if (sum === toFloat(this.model.Sum)) {
            return;
        }

        this.model.Sum = sum;

        this.updateBills();

        this.validateField(`Sum`);
        this.validateField(`MediationCommission`);
    };

    @action setIsMediation = ({ checked }) => {
        this.model.IsMediation = checked;

        if (!checked) {
            this.model.MediationCommission = ``;
            this.validateField(`MediationCommission`);
        } else {
            this.model.KontragentAccountCode = SyntheticAccountCodesEnum._62_02;
        }
    };

    @action setMediationCommission = ({ value }) => {
        const mediationCommission = toFloat(value);

        if (this.model.MediationCommission !== mediationCommission) {
            this.model.MediationCommission = toFloat(value);
            this.validateField(`MediationCommission`);
        }
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
    };

    @action setDescription = value => {
        this.model.Description = value;
        this.validateField(`Description`);
    };

    @action setBills = async (items, options = {}) => {
        const { keepZero = true } = options;

        let list = mapDocumentsToModel(items, toFloat(this.model.Sum));

        if (!keepZero) {
            list = filterLinked(list);
        }

        this.model.Bills = list;

        const billKontragentId = (this.bills.find(b => b.KontragentId > 0) || {}).KontragentId;

        if (!this.kontragentId && billKontragentId) {
            await this.setContractor({ original: { KontragentId: billKontragentId } });
        }

        this.validateField(`BillsSum`);
    };

    @action updateBills = () => {
        const sum = toFloat(this.model.Sum);

        if (sum > 0) {
            this.setBills(this.bills.map(({ Sum, ...bill }) => bill), { keepZero: false });
        }
    };

    /* kontragent */

    @action setContractor = async ({ original }) => {
        const previous = this.model.Kontragent;
        const newValues = mapKontragentToModel(original);

        if (!newValues.KontragentId || newValues.SalaryWorkerId
            || (previous.KontragentId && newValues.KontragentId !== previous.KontragentId)
        ) {
            this.setContract();
            this.setBills([]);
        }

        this.model.Kontragent = newValues;

        this.handleSelfKontragentForm();

        if (this.validationState.Kontragent) {
            this.validateField(`Kontragent`);
        }

        await this.loadKontragentNameById();
    };

    /** нужно для валидации ИНН, когда выбираем р/сч своей же организации */
    @action handleSelfKontragentForm = () => {
        if (this.isSelfKontragent) {
            this.model.Kontragent.KontragentForm = this.Requisites.IsAccounting ? KontragentsFormEnum.UL : KontragentsFormEnum.FL;
        }
    };

    @action setKontragentAccountCode = ({ value }) => {
        this.model.KontragentAccountCode = value;
    };

    @action loadKontragentNameById = async () => {
        const { KontragentId, KontragentName } = this.model.Kontragent;

        if (KontragentId && !KontragentName) {
            const { Name } = await kontragentService.getById({ id: KontragentId });
            this.model.Kontragent.KontragentName = Name;
        }
    };

    @action setContractorName = ({ value }) => {
        if (!value) {
            this.setContractor({});

            return;
        }

        if (value !== this.model.Kontragent.KontragentName) {
            this.model.Kontragent.KontragentName = value;
        }
    };

    @action setContractorSettlementAccount = ({ value }) => {
        this.model.Kontragent.KontragentSettlementAccount = value;
        this.validateField(`KontragentSettlementAccount`);
    };

    @action setContractorINN = ({ value }) => {
        this.model.Kontragent.KontragentINN = value;
        this.validateField(`KontragentInn`);
    };

    @action setContractorKPP = ({ value }) => {
        this.model.Kontragent.KontragentKPP = value;
        this.validateField(`KontragentKpp`);
    };

    /* kontragent END */

    @action validateField = validationField => {
        const rules = validationRules[validationField];

        if (!rules) {
            return;
        }

        const requisites = this.Requisites;
        const { model } = this;

        this.validationState[validationField] = validator({
            model, rules, requisites
        });
    };

    @action validateTaxPostingsList = () => {
        this.model.TaxPostings.Postings = taxPostingsValidator.getValidatedList(this.model.TaxPostings.Postings, { isOsno: this.isOsno, isIp: this.isIp });
    };

    @action validateAccountingPostingsList = () => {
        if (!this.canViewAccountingPostings || !this.model.ProvideInAccounting) {
            return;
        }

        this.model.AccountingPostings.Postings = accountingPostingsValidator.getValidatedList(this.model.AccountingPostings.Postings);
    };

    @action updateAccountingPostingList = posting => {
        const { Postings } = this.model.AccountingPostings;
        const index = Postings.findIndex(item => posting.key === item.key);

        if (index !== -1) {
            Postings[index] = posting;
        } else {
            Postings.push(posting);
        }
    };
}

function filterLinked(list) {
    return list.filter(item => !!toFloat(item.Sum));
}

export default Actions;
