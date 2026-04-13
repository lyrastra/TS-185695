import { action, runInAction, override } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import kontragentService from '@moedelo/frontend-common-v2/apps/kontragents/services/kontragentService';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import defaultModel from './RentPaymentModel';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';
import validationRules from '../validationRules';
import { mapKontragentToModel } from '../../../../../../../../helpers/newMoney/kontragentHelper';
import validator from '../../../../validation/validator';
import RentPaymentComputed from './RentPaymentComputed';
import { getCurrencyRate } from '../../../../../../../../services/newMoney/currencyService';
import { getContractById } from '../../../../../../../../services/contractService';
import { mapToServerModel } from '../../../../../../../../mappers/mappers';
import contractHelper from '../../../../../../../../helpers/newMoney/contractHelper';
import { mapPeriodsToModel } from '../../../../../../../../mappers/periodMapper';
import { getInventoryCardByBaseId } from '../../../../../../../../services/fixedAssetService';

class RentPaymentActions extends RentPaymentComputed {
    @override setDate({ value }) {
        if (this.model.Date === value) {
            return;
        }

        const oldDateYear = dateHelper(this.model.Date).year();
        const newDateYear = dateHelper(value).year();
        const isManualChangeYear = oldDateYear !== newDateYear;

        this.model.Date = value;
        this.validateField(`Date`);

        const isValidDate = dateHelper(this.initialDate).isSameOrBefore(dateHelper(this.model.Date));
        this.isShowApprove = isValidDate && this.isOutsourceUser;

        if (dateHelper(this.model.Date).isValid()) {
            this.updateNumber();
        }

        if (this.model.Date) {
            this.initNds();
        }

        if (isManualChangeYear && this.isNew) {
            this.checkUsnTax();
        }
    }

    @override setNdsType({ value }) {
        this.model.NdsType = value === ` ` ? null : value;
        this.validateField(`NdsSum`);
    }

    @override setIncludeNds({ checked }) {
        if (!this.model.IncludeNds && checked) {
            this.setNdsType({ value: this.ndsTypes[0].value });
        }

        if (!checked) {
            this.setNdsType({ value: null });
            this.setNdsSum({ value: null });
        }

        this.model.IncludeNds = checked;
        this.validateField(`NdsSum`);
    }

    @action checkUsnTax = () => {
        const { IsUsn, isAfter2025 } = this.isAfter2025WithTaxation;

        if (isAfter2025 && IsUsn) {
            this.setIncludeNds({ checked: true });
        } else if (IsUsn) {
            this.setIncludeNds({ checked: false });
        }
    };

    @override setNdsSum({ value }) {
        this.model.NdsSum = toFloat(value) || ``;

        this.validateField(`NdsSum`);
    }

    @action setSum = ({ sum, period }) => {
        this.model.Periods = this.model.Periods.map(item => {
            if (item.Id === period.Id) {
                Object.assign(item, { Sum: sum ? toFloat(sum) : 0 });
            }

            return item;
        });

        this.model.TotalSum = this.totalSum;
        this.validateField(`TotalSum`);
        this.validateField(`SumPeriod`);
        this.validateField(`DefaultSumPeriod`);
    };

    @action updateCurrencyRate = async () => {
        this.model.CentralBankRate = await getCurrencyRate({
            date: this.model.Date,
            currencyCode: this.currencyCode
        });
    };

    @action setContract = async ({ model = defaultModel.Contract } = {}) => {
        const { KontragentId, KontragentName } = model;

        this.model.Contract = {
            ContractBaseId: model.ContractBaseId,
            ProjectNumber: model.ProjectNumber,
            ProjectId: model.ProjectId,
            KontragentId
        };

        if (KontragentId && KontragentId !== this.model.Kontragent.KontragentId) {
            await this.setContractor({ original: { ...defaultModel.Kontragent, KontragentId, KontragentName } });
        }

        if (this.model.Contract && !this.model.Contract.ProjectId) {
            await this.setFixedAsset();
        } else if (this.model.Contract.ProjectId) {
            const fixedAssets = await this.getFixedAssetAutocomplete(``);
            fixedAssets?.data?.length > 0 && await this.setFixedAsset({ model: fixedAssets.data[0].model });
            await this.setFirstPeriodId(this.model.Contract.ProjectId);
        }

        if (this.validationState.Contract) {
            this.validateField(`Contract`);
        }

        this.setPeriods([{
            Id: 0,
            Sum: ``,
            DefaultSum: ``,
            Description: ``
        }]);
    };

    @action setFixedAsset = async ({ model = defaultModel.FixedAsset } = {}) => {
        const { KontragentId, ContractId, DocumentBaseId } = model;
        const inventoryCard = DocumentBaseId && await getInventoryCardByBaseId({ documentBaseId: DocumentBaseId });

        this.model.FixedAsset = {
            Id: model.Id,
            Name: model.Name,
            Number: model.Number,
            ContractId,
            KontragentId,
            DocumentBaseId,
            IsRentRemains: inventoryCard?.IsFromBalances
        };

        this.kontragentLoading = true;
        this.contractLoading = true;

        if (KontragentId && (!this.model.Kontragent.KontragentId || KontragentId !== this.model.Kontragent.KontragentId)) {
            const kontragent = await kontragentService.getById({ id: KontragentId });
            runInAction(() => {
                this.kontragentLoading = false;
                this.model.Kontragent = mapToServerModel(kontragent);
                this.model.KontragentId = this.model.Kontragent.KontragentId;
            });
            await this.setContractor({ original: this.model.Kontragent });
        }

        if (ContractId && (!this.model.Contract.ProjectId || ContractId !== this.model.Contract.ProjectId)) {
            const contract = await getContractById({ contractId: ContractId });
            runInAction(() => {
                this.contractLoading = false;
            });
            // eslint-disable-next-line
            await this.setContract({ model: contractHelper.mapToServerModel(contract) });
        }

        this.setPeriods([{
            Id: 0,
            Sum: ``,
            DefaultSum: ``,
            Description: ``
        }]);

        if (this.contractLoading) {
            this.contractLoading = false;
        }

        if (this.kontragentLoading) {
            this.kontragentLoading = false;
        }

        this.validateField(`FixedAsset`);
    };

    @action setPeriod = (period, value) => {
        this.model.Periods = this.model.Periods.map(obj => {
            if (obj.Id === period.Id) {
                return value;
            }

            return obj;
        });
    };

    @action setFirstPeriodId = async contractId => {
        if (!this.firstPeriodId && contractId) {
            const { remainsDate } = this;
            const contract = await getContractById({ contractId });
            const periods = contract?.RentalPaymentSchedule?.RentalPaymentItems.filter(item => {
                const paymentDate = dateHelper(item.PaymentDate, `DD.MM.YYYY`);

                return paymentDate.year() >= remainsDate.year()
                    && paymentDate.month() >= remainsDate.month() - 1;
            });

            this.model.FirstPeriodId = periods?.length > 0 && periods[0].Id;
        }
    };

    @action setPeriods = items => {
        this.model.Periods = mapPeriodsToModel(items);

        this.validateField(`Period`);
        this.validateField(`SumPeriod`);

        this.model.TotalSum = this.totalSum;
        this.validateField(`TotalSum`);
    };

    @action setDescription = value => {
        if (this.model.Description !== value) {
            this.model.Description = value;
            this.validateField(`Description`);
        }
    };

    // kontragent
    @action setContractor = async ({ original }) => {
        const previous = this.model.Kontragent;
        const newValues = mapKontragentToModel(original);

        if (!newValues.KontragentId
            || (previous.KontragentId && newValues.KontragentId !== previous.KontragentId)) {
            await this.setContract();
        }

        this.model.Kontragent = newValues;

        if (this.validationState.Kontragent) {
            this.validateField(`Kontragent`);
        }

        await this.loadKontragentNameById();
        this.setPeriods([{
            Id: 0,
            Sum: ``,
            DefaultSum: ``,
            Description: ``
        }]);
    };

    @action loadKontragentNameById = async () => {
        const { KontragentId, KontragentName } = this.model.Kontragent;

        if (KontragentId && !KontragentName) {
            const { Name } = await kontragentService.getById({ id: KontragentId });
            runInAction(() => {
                this.model.Kontragent.KontragentName = Name;
            });
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
}

export default RentPaymentActions;
