import { makeObservable, observable, reaction } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import Actions from './Actions';
import MoneyOperationTypeResources from '../../../../../../../../resources/MoneyOperationTypeResources';
import defaultModel from './Model';
import validationRules from './../validationRules';
import OrderType from '../../../../../../../../enums/OrderTypeEnum';
import notTaxableReasonGetter from './../notTaxableReasonGetter';
import requiredFieldForTaxPostings from '../../../../../../../../resources/newMoney/requiredFieldForTaxPostings';
import requiredFieldForAccountingPostings from '../../../../../../../../resources/newMoney/requiredFieldForAccountingPostings';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import AvailableTaxPostingDirection from '../../../../../../../../enums/newMoney/AvailableTaxPostingDirectionEnum';
import { getValidTaxationSystemType } from '../../../../../../../../mappers/taxationSystemMapper';
import PostingDirection from '../../../../../../../../enums/newMoney/TaxPostingDirectionEnum';
import PostingTransferType from '../../../../../../../../enums/newMoney/TaxPostingTransferTypeEnum';
import TaxPostingTransferKind from '../../../../../../../../enums/newMoney/TaxPostingTransferKindEnum';
import TaxPostingNormalizedCostType from '../../../../../../../../enums/newMoney/TaxPostingNormalizedCostTypeEnum';
import { calculateNdsBySumAndType } from '../../../../../../../../helpers/newMoney/ndsCalculationHelper';
import NdsTypesEnum from '../../../../../../../../enums/newMoney/NdsTypesEnum';

class GoodsPaidByCreditCardStore extends Actions {
    @observable validationState = {
        Number: ``,
        Date: ``,
        Sum: ``,
        Description: ``,
        AcquiringCommissionDate: ``,
        SaleDate: ``,
        TaxationSystemType: ``,
        Patent: ``,
        NdsSum: ``
    };

    @observable model = { ...defaultModel };

    @observable accountingPostingsLoading = false;

    @observable taxPostingsLoading = false;

    availableTaxPostingDirection = AvailableTaxPostingDirection.Incoming;

    /* override */
    messageNoAccountingObjects = ``;

    constructor(options) {
        super(options);
        makeObservable(this);

        this._initModel(defaultModel, options.operation);

        /** костыль от старого бэка, выпилить после полного перевода */
        if (!this.model.AcquiringCommissionDate && !this.model.AcquiringCommission) {
            const { AcquiringCommission } = defaultModel;

            Object.assign(this.model, {
                AcquiringCommission,
                AcquiringCommissionDate: this.model.Date
            });
        }

        if (!this.model.SaleDate) {
            Object.assign(this.model, {
                SaleDate: this.model.Date
            });
        }

        if (typeof this.model.IncludeNds !== `boolean`) {
            this.setIncludeNds({ checked: false });
            this.setNdsSum({ value: null });
        }

        this.model.TaxationSystemType = getValidTaxationSystemType({
            taxationSystem: options.taxationSystem,
            taxationSystemType: options.operation.TaxationSystemType,
            isOoo: options.requisites.IsOoo
        });
        this.setActivePatents(options.activePatents);

        this.model.TaxPostingsMode = options.operation.TaxPostingsInManualMode
            ? ProvidePostingType.ByHand
            : ProvidePostingType.Auto;
        // родительский store ожидает поле PostingsAndTaxMode
        this.model.PostingsAndTaxMode = this.model.TaxPostingsMode;

        this.initAccountingPostings();
        this.initTaxPostings();
        this.initReactions();
    }

    initReactions = () => {
        reaction(() => this.model.Date, async Date => {
            if (!this.validationState.Date) {
                this.setTaxationSystem(await taxationSystemService.getTaxSystem(Date));
                this.updateActivePatents();
            }
        });

        reaction(this.getFieldsForTaxPostings, this.loadTaxPostings);
        reaction(this.getFieldsForAccountingPostings, this.loadAccountingPostings);
        reaction(() => [this.model.AcquiringCommission, this.model.NdsType, this.model.IncludeNds], this.calculateNdsBySumAndType);
    };

    isValid() {
        return this.isValidModel && this.isValidTaxPostings;
    }

    validateModel() {
        Object.keys(validationRules).forEach(fieldName => {
            this.validateField(fieldName, this.model[fieldName]);
        });
    }

    modelForSave() {
        const { model, TaxationSystem } = this;
        const operationType = MoneyOperationTypeResources.MemorialWarrantReceiptGoodsPaidCreditCard;

        let postingsAndTaxMode = model.TaxPostingsMode;

        if (!model.ProvideInAccounting) {
            postingsAndTaxMode = ProvidePostingType.ByHand;
        }

        const customPostings = postingsAndTaxMode === ProvidePostingType.ByHand ? model.TaxPostings.Postings : [];
        const TaxPostings = this.getTaxPostingForSave(customPostings, postingsAndTaxMode, TaxationSystem);

        return {
            Id: model.Id,
            BaseDocumentId: model.BaseDocumentId,
            DocumentBaseId: model.DocumentBaseId,
            OrderType: OrderType.MemorialWarrant,
            OperationType: operationType.value,
            Direction: operationType.Direction,
            Number: model.Number,
            Date: dateHelper(model.Date, `DD.MM.YYYY`).format(`YYYY-MM-DD`),
            SettlementAccountId: model.SettlementAccountId,
            Sum: model.Sum,
            Acquiring: {
                IsAcquiring: model.AcquiringCommission > 0,
                CommissionSum: model.AcquiringCommission,
                CommissionDate: dateHelper(model.AcquiringCommissionDate, `DD.MM.YYYY`).format(`YYYY-MM-DD`)
            },
            Nds: {
                Type: model.NdsType,
                IncludeNds: model.IncludeNds,
                Sum: model.NdsSum || 0
            },
            AcquiringCommission: model.AcquiringCommission,
            AcquiringCommissionDate: model.AcquiringCommissionDate,
            SaleDate: dateHelper(model.SaleDate, `DD.MM.YYYY`).format(`YYYY-MM-DD`),
            Description: model.Description,
            PaymentPriority: model.PaymentPriority,
            ProvideInAccounting: model.ProvideInAccounting,
            TaxationSystemType: model.TaxationSystemType,
            PatentId: this.patentIdForSave,
            PostingsAndTaxMode: ProvidePostingType.ByHand,
            TaxPostings,
            IsMediation: model.IsMediation
        };
    }

    calculateNdsBySumAndType = ([Sum, NdsType, IncludeNds, NdsSum]) => {
        if (NdsSum || NdsSum === 0) {
            return;
        }
    
        if (IncludeNds) {
            const ndsSum = NdsType === null ? `` : calculateNdsBySumAndType(Sum, NdsType);
    
            this.setNdsSum({ value: ndsSum });
        }
    };

    getFieldsForTaxPostings = () => {
        return JSON.stringify([
            this.model.Date,
            this.model.Sum,
            this.model.AcquiringCommission,
            this.model.AcquiringCommissionDate,
            this.model.Description,
            this.model.TaxationSystemType,
            this.model.IsMediation,
            this.model.IncludeNds,
            this.model.NdsSum,
            this.model.NdsType
        ]);
    };

    getRequiredFieldsForTaxPostingMsg = () => {
        if (!this.model.Date || this.validationState.Date) {
            return requiredFieldForTaxPostings.date;
        }
        
        if (!toFloat(this.model.Sum) && !toFloat(this.model.AcquiringCommission)) {
            return `Не учитывается. Заполните сумму или комиссию`;
        }
        
        if (toFloat(this.model.AcquiringCommission) && !this.model.AcquiringCommissionDate) {
            return requiredFieldForTaxPostings.date;
        }
        
        if (toFloat(this.model.AcquiringCommission) && this.model.IncludeNds && this.model.NdsType !== NdsTypesEnum.Nds0 && this.model.NdsType !== NdsTypesEnum.None && !this.model.NdsSum) {
            return requiredFieldForTaxPostings.ndsSum;
        }

        return null;
    };

    getTaxPostingsExplainingMsg = () => {
        return notTaxableReasonGetter.get({ taxationSystem: this.taxationForPostings })
            || this.getRequiredFieldsForTaxPostingMsg();
    };

    getFieldsForAccountingPostings = () => {
        return JSON.stringify([
            this.model.Date,
            this.model.SettlementAccountId,
            this.model.Sum,
            this.model.AcquiringCommission,
            this.model.AcquiringCommissionDate,
            this.model.TaxationSystemType,
            this.model.ProvideInAccounting,
            this.model.SaleDate,
            this.model.IsMediation,
            this.model.IncludeNds,
            this.model.NdsSum,
            this.model.NdsType
        ]);
    };

    getRequiredFieldsForAccountingPostingMsg = () => {
        if (!this.model.Date || this.validationState.Date) {
            return requiredFieldForAccountingPostings.date;
        }

        if (!toFloat(this.model.Sum) && !toFloat(this.model.AcquiringCommission)) {
            return `Не учитывается. Заполните сумму или комиссию`;
        }

        if (toFloat(this.model.AcquiringCommission) && !this.model.AcquiringCommissionDate) {
            return requiredFieldForAccountingPostings.acquiringCommissionDate;
        }

        return null;
    };

    getAccountingPostingsExplainingMsg = () => {
        return this.getRequiredFieldsForAccountingPostingMsg();
    };

    getTransferType = ({ Direction }) => {
        if (Direction === PostingDirection.Outgoing) {
            return [
                PostingTransferType.Direct,
                PostingTransferType.Indirect,
                PostingTransferType.NonOperating
            ];
        }

        return [
            PostingTransferType.NonOperating,
            PostingTransferType.OperationIncome
        ];
    };

    getTransferKind = ({ Direction, Type }) => {
        if (Direction === PostingDirection.Outgoing) {
            if (Type === PostingTransferType.Direct) {
                return [TaxPostingTransferKind.Material];
            }

            if (Type === PostingTransferType.Indirect) {
                return [
                    TaxPostingTransferKind.Material,
                    TaxPostingTransferKind.Salary,
                    TaxPostingTransferKind.Amortization,
                    TaxPostingTransferKind.OtherOutgo

                ];
            }

            return [TaxPostingTransferKind.None];
        }

        if (Type === PostingTransferType.OperationIncome) {
            return [
                TaxPostingTransferKind.Service,
                TaxPostingTransferKind.ProductSale,
                TaxPostingTransferKind.PropertyRight,
                TaxPostingTransferKind.OtherPropertySale
            ];
        }

        return [TaxPostingTransferKind.None];
    };

    getNormalizedCostType = ({ Direction }) => {
        if (Direction === PostingDirection.Outgoing) {
            return Object.values(TaxPostingNormalizedCostType);
        }

        return [TaxPostingNormalizedCostType.None];
    };
}

export default GoodsPaidByCreditCardStore;
