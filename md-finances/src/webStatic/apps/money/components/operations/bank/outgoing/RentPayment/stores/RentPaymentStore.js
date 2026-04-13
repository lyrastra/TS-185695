import { observable, reaction, makeObservable } from 'mobx';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import { toAmountString, toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import NavigateHelper from '@moedelo/frontend-core-v2/helpers/NavigateHelper';
import UsnTypeEnum from '@moedelo/frontend-enums/mdEnums/UsnType';
import AvailableTaxPostingDirection from '../../../../../../../../enums/newMoney/AvailableTaxPostingDirectionEnum';
import { getContractorAutocomplete, getSettlementAccounts } from '../../../../../../../../services/newMoney/contractorService';
import validationRules from '../validationRules';
import { autocomplete } from '../../../../../../../../services/contractService';
import { getFixedAssetAutocomplete, getInventoryCardByBaseId } from '../../../../../../../../services/fixedAssetService';
import { getPeriodAutocomplete } from '../../../../../../../../services/periodService';
import { mapForAutocomplete } from '../../../../../../../../helpers/newMoney/contractHelper';
import { mapForFixedAssetAutocomplete } from '../../../../../../../../helpers/newMoney/fixedAssetHelper';
import { mapForPeriodAutocomplete, mapPeriodsToSavingModel, mapServerPeriodsToModel } from '../../../../../../../../mappers/periodMapper';
import MoneyOperationTypeResources from '../../../../../../../../resources/MoneyOperationTypeResources';
import requiredFieldForAccountingPostings from '../../../../../../../../resources/newMoney/requiredFieldForAccountingPostings';
import requiredFieldForTaxPostings from '../../../../../../../../resources/newMoney/requiredFieldForTaxPostings';
import defaultModel from './RentPaymentModel';
import RentPaymentActions from './RentPaymentActions';
import KontragentType from '../../../../../../../../enums/KontragentType';
import DocumentStatusEnum from '../../../../../../../../enums/DocumentStatusEnum';
import { calculateNdsBySumAndType } from '../../../../../../../../helpers/newMoney/ndsCalculationHelper';
import notTaxableRentGetter from './../notTaxableRentGetter';
import { getKontragentSettlements } from '../../../../../../../../helpers/newMoney/kontragentHelper';

class RentPaymentStore extends RentPaymentActions {
    @observable validationState = {
        Number: ``,
        Date: ``,
        Kontragent: ``,
        KontragentSettlementAccount: ``,
        Description: ``,
        Contract: ``,
        Period: ``,
        SumPeriod: ``,
        DefaultSumPeriod: ``,
        TotalSum: ``,
        NdsSum: ``,
        FixedAsset: ``
    };

    @observable model = { ...defaultModel };

    @observable contractLoading = false;

    availableTaxPostingDirection = AvailableTaxPostingDirection.Outgoing;

    needAllTotalSumValidation = true;

    constructor(options) {
        super(options);
        makeObservable(this);

        this._initModel(defaultModel, options.operation);

        this.mapOperationToModel(options.operation);
        this.initNds();
        this.updateCurrencyRate();
        this.initializeKontragentSettlements();

        this.initTaxPostings();
        this.initAccountingPostings();
        this.initReactions();
    }

    initReactions = () => {
        reaction(() => [this.validationState.KontragentSettlementAccount, this.validationState.KontragentInn, this.validationState.KontragentKpp], this.checkKontragentRequisitesVisibility);
        reaction(() => this.model.Kontragent, this.loadKontragentRequisites);
        reaction(() => this.model.SettlementAccountId, this.updateCurrencyRate);
        reaction(() => this.model.Date, async Date => {
            if (!this.validationState.Date) {
                this.setTaxationSystem(await taxationSystemService.getTaxSystem(Date));
                this.updateCurrencyRate();
            }
        });
        reaction(this.getFieldsForAccountingPostings, this.loadAccountingPostings);
        reaction(this.getFieldsForTaxPostings, this.loadTaxPostings);
        reaction(() => [this.model.TotalSum, this.model.NdsType, this.model.IncludeNds], this.calculateNdsBySumAndType);
        reaction(() => [this.model.TotalSum, this.model.IncludeNds, this.model.NdsSum], this.handleDescriptionMessage);
    };

    handleDescriptionMessage = () => {
        if (!this.isNew) {
            return;
        }

        const { leaseTotalSum, redemptionTotalSum } = this;
        const { IncludeNds, NdsSum, NdsType } = this.model;
        let msg = ``;

        if (leaseTotalSum && (!IncludeNds || !NdsSum)) {
            msg = `Лизинговый платеж на сумму ${toAmountString(leaseTotalSum)} руб. НДС не облагается.`;
        }

        if (redemptionTotalSum && (!IncludeNds || !NdsSum)) {
            msg += `${msg.length > 0 ? `  ` : ``}Выкупной платеж на сумму ${toAmountString(redemptionTotalSum)} руб. НДС не облагается.`;
        }

        if (leaseTotalSum && IncludeNds && NdsSum) {
            msg = `Лизинговый платеж на сумму ${toAmountString(leaseTotalSum)} руб. В т. ч. НДС ${toAmountString(NdsSum)} руб.`;
        }

        if (redemptionTotalSum && IncludeNds && NdsSum) {
            const redemptionNdsSum = calculateNdsBySumAndType(redemptionTotalSum, NdsType);

            msg += `${msg.length > 0 ? `  ` : ``}Выкупной платеж на сумму ${toAmountString(redemptionTotalSum)} руб. В т. ч. НДС ${toAmountString(redemptionNdsSum)} руб.`;
        }

        this.setDescription(msg);
    };

    loadKontragentRequisites = async () => {
        const kontragent = this.model.Kontragent;

        this.kontragentSettlements = kontragent.KontragentId
            ? await getSettlementAccounts({ kontragentId: kontragent.KontragentId })
            : [{ Number: kontragent.KontragentSettlementAccount, BankName: kontragent.KontragentBankName }];

        if (this.kontragentSettlements.length) {
            const {
                Number, BankName, Bik, KontragentBankCorrespondentAccount
            } = this.kontragentSettlements[0];

            this.setContractorSettlementAccount({ value: Number });
            this.setContractorBankName({ value: BankName });
            this.setContractorBankCorrespondentAccount({ value: KontragentBankCorrespondentAccount });
            this.setContractorBankBIK({ value: Bik });
        }
    };

    initNds = () => {
        if (typeof this.model.IncludeNds !== `boolean`) {
            const { IsUsn, isAfter2025, IsOsno } = this.isAfter2025WithTaxation;

            if (isAfter2025 && IsUsn) {
                this.setIncludeNds({ checked: true });
            } else {
                this.setIncludeNds({ checked: IsOsno });
            }

            this.setNdsType({ value: this.ndsTypes[0].value });
        }

        if (this.model.IsTypeChanged) {
            this.calculateNdsBySumAndType([this.model.TotalSum, this.model.NdsType, this.model.IncludeNds]);
        }
    };

    /* override */
    initTaxPostings = () => {
        this.model.TaxPostings.ExplainingMessage = this.getTaxPostingsExplainingMsg();
        this.model.TaxPostingsMode = this.model.PostingsAndTaxMode;
        this.setTaxPostingList({ Postings: this.model.TaxPostings.Postings });
    };

    calculateNdsBySumAndType = ([Sum, NdsType, IncludeNds, NdsSum]) => {
        if (NdsSum || NdsSum === 0) {
            return;
        }

        if (IncludeNds) {
            const ndsSum = NdsType === null ? `` : calculateNdsBySumAndType(Sum, NdsType);

            this.setNdsSum({ value: ndsSum });
        }
    };

    initializeKontragentSettlements = async () => {
        this.kontragentSettlements = await getKontragentSettlements(this.model);
    };

    isValid = () => {
        const modelIsValid = !Object.keys(this.validationState).find(key => {
            return this.validationState[key] !== ``;
        });

        return modelIsValid;
    };

    validateModel() {
        Object.keys(validationRules).forEach(fieldName => {
            this.validateField(fieldName, this.model[fieldName]);
        });

        this.validateTaxPostingsList();
    }

    getContractAutocomplete = async ({ query = `` }) => {
        const response = await autocomplete({
            query,
            kind: [4],
            kontragentId: this.model.Kontragent.KontragentId,
            withMainContract: false
        });

        return mapForAutocomplete(response, query);
    };

    showAddContractDialog = () => {
        NavigateHelper.push(`/Contract#new/rent/${this.model.BaseDocumentId || `new`}`);
    };

    getFixedAssetAutocomplete = async ({ query = `` }) => {
        const response = await getFixedAssetAutocomplete({ query, contractId: this.model.Contract.ProjectId, kontragentId: this.model.Kontragent.KontragentId });

        return mapForFixedAssetAutocomplete(response, query);
    };

    getPeriodAutocomplete = async ({ query = `` }) => {
        const excludeIds = this.model.Periods.reduce((res, element) => {
            if (element.Id) {
                res.push(element.Id);
            }

            return res;
        }, []);
        const response = await getPeriodAutocomplete({
            query,
            contractBaseId: this.model.Contract.ContractBaseId,
            paymentDocumentBaseId: this.model.BaseDocumentId || 0,
            excludeIds: excludeIds.join(),
            withMinDate: this.isRentRemains
        });

        // для периода с последним месяцем ввода остатков, сумма не должна заполняться
        if (this.isRentRemains && response?.length > 0 && response[0].Id === this.firstPeriodId) {
            response[0].Sum = 0;
        }

        return mapForPeriodAutocomplete(response, query);
    };

    mapOperationToModel = async operation => {
        if (!operation) {
            return;
        }

        if (operation.InventoryCard && operation.InventoryCard.Data) {
            const { Data } = operation.InventoryCard;

            this.model.FixedAsset.DocumentBaseId = Data.DocumentBaseId;
            this.model.FixedAsset.Name = Data.FixedAssetName;
            this.model.FixedAsset.Number = Data.InventoryNumber;

            const inventoryCard = await getInventoryCardByBaseId({ documentBaseId: Data.DocumentBaseId });

            this.model.FixedAsset.IsRentRemains = inventoryCard?.IsFromBalances;
            this.model.FixedAsset.Id = inventoryCard?.Id;
            await this.setFirstPeriodId(inventoryCard?.RentContract?.Id);
        }

        this.model.TotalSum = operation?.RentPeriods?.reduce((acc, el) => acc + el.Sum, 0) || 0;
        this.model.Sum = 0;

        if (!this.model.IsTypeChanged) {
            this.model.NdsSum = operation.NdsSum;
        }

        this.validateField(`NdsSum`);

        if (operation.RentPeriods) {
            this.model.Periods = mapServerPeriodsToModel(operation.RentPeriods);
        }
    };

    modelForSave = () => {
        const { model } = this;
        const operationType = MoneyOperationTypeResources.RentPayment;
        let contractorType;

        if (model.Kontragent.KontragentId > 0) {
            contractorType = KontragentType.Kontragent;
        } else {
            contractorType = KontragentType.Ip;
        }

        return {
            DocumentBaseId: model.DocumentBaseId,
            OperationType: operationType.value,
            Direction: operationType.Direction,
            Number: model.Number,
            Date: dateHelper(model.Date, `DD.MM.YYYY`).format(`YYYY-MM-DD`),
            SettlementAccountId: model.SettlementAccountId,
            Contractor: {
                Id: model.Kontragent.KontragentId,
                Name: model.Kontragent.KontragentName,
                Inn: model.Kontragent.KontragentINN,
                Kpp: model.Kontragent.KontragentKPP,
                SettlementAccount: model.Kontragent.KontragentSettlementAccount,
                BankName: model.Kontragent.KontragentBankName,
                BankBik: model.Kontragent.KontragentBankBIK,
                BankCorrespondentAccount: model.Kontragent.KontragentBankCorrespondentAccount
            },
            ContractorType: contractorType,
            Sum: model.TotalSum,
            Description: model.Description,
            Contract: {
                DocumentBaseId: model.Contract.ContractBaseId
            },
            Nds: {
                IncludeNds: model.IncludeNds,
                Sum: model.NdsSum || 0,
                Type: model.NdsType
            },
            IsPaid: model.Status === DocumentStatusEnum.Payed,
            ProvideInAccounting: model.ProvideInAccounting,
            InventoryCard: {
                DocumentBaseId: model.FixedAsset.DocumentBaseId
            },
            RentPeriods: mapPeriodsToSavingModel(model.Periods)
        };
    };

    getTaxPostingsExplainingMsg = () => {
        const { UsnType, IsUsn } = this.TaxationSystem;

        if (UsnType !== UsnTypeEnum.ProfitAndOutgo) {
            return notTaxableRentGetter.get({ taxationSystem: { IsUsn, UsnType } });
        }

        return this.getRequiredFieldsForTaxPostingMsg() || notTaxableRentGetter.get({ taxationSystem: { IsUsn, UsnType } });
    };

    /* override */
    getFieldsForAccountingPostings = () => {
        return JSON.stringify([
            this.model.TotalSum,
            this.model.Date,
            this.model.KontragentAccountCode,
            this.model.Kontragent.KontragentId,
            this.model.SettlementAccountId,
            this.model.Status,
            this.model.ProvideInAccounting
        ]);
    }

    /* override */
    getFieldsForTaxPostings = () => {
        return JSON.stringify([
            this.model.Date,
            this.model.TotalSum,
            this.model.Kontragent.KontragentId,
            this.model.FixedAsset.Id,
            this.model.Status
        ]);
    };

    /* override */
    getRequiredFieldsForAccountingPostingMsg = () => {
        if (!this.model.Date || this.validationState.Date) {
            return requiredFieldForAccountingPostings.date;
        }

        if (!this.model.Kontragent.KontragentId) {
            return requiredFieldForAccountingPostings.outgoingPayer;
        }

        if (!toFloat(this.model.TotalSum)) {
            return requiredFieldForAccountingPostings.sum;
        }

        if (this.model.Status === DocumentStatusEnum.NotPayed) {
            return requiredFieldForAccountingPostings.notPaid;
        }

        return null;
    };

    /* override */
    getAccountingPostingsExplainingMsg = () => {
        return this.getRequiredFieldsForAccountingPostingMsg();
    };

    /* override */
    getRequiredFieldsForTaxPostingMsg = () => {
        if (!this.model.Date || this.validationState.Date) {
            return requiredFieldForTaxPostings.date;
        }

        if (!this.model.Kontragent.KontragentId) {
            return requiredFieldForTaxPostings.outgoingPayer;
        }

        if (!this.model.FixedAsset.Name) {
            return requiredFieldForTaxPostings.fixedAsset;
        }

        if (!this.model.Periods || this.validationState.Period) {
            return requiredFieldForTaxPostings.period;
        }

        if (!toFloat(this.model.TotalSum)) {
            return requiredFieldForTaxPostings.sum;
        }

        if (this.model.Status === DocumentStatusEnum.NotPayed) {
            return requiredFieldForTaxPostings.notPaid;
        }

        return null;
    };

    getContractorAutocomplete = async ({ query = `` }) => {
        const { SettlementAccountId, Date } = this.model;

        return getContractorAutocomplete({
            kontragentType: [1], // Kontragent
            onlyFounders: false,
            count: 5,
            SettlementAccountId,
            Date,
            query
        });
    };
}

export default RentPaymentStore;
