import { action, override } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import kontragentService from '@moedelo/frontend-common-v2/apps/kontragents/services/kontragentService';
import defaultModel from '../stores/Model';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';
import accountingPostingsValidator from '../../../../validation/accountingPostingsValidator';
import validationRules from '../validationRules';
import { mapKontragentToModel } from '../../../../../../../../helpers/newMoney/kontragentHelper';
import validator from '../../../../validation/validator';
import Computed from './Computed';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import {
    mapLinkedDocumentsTaxPostingsToModel,
    osnoTaxPostingsToModel,
    usnTaxPostingsToModel
} from '../../../../../../../../mappers/taxPostingsMapper';
import PostingDirection from '../../../../../../../../enums/newMoney/TaxPostingDirectionEnum';
import TaxStatusEnum from '../../../../../../../../enums/TaxStatusEnum';
import PayerStatus from '../enums/PayerStatus';
import { isBailiffPayment } from '../helpers/payerStatusHelper';

class Actions extends Computed {
    @override setTaxPostingList({ Postings = [], LinkedDocuments = [], TaxStatus } = {}) {
        const postings = Postings.slice();

        if ([TaxStatusEnum.No, TaxStatusEnum.ByHand].includes(TaxStatus) && !Postings.length) {
            postings.push({ Direction: PostingDirection.Outgoing });
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

    @override editTaxPostingList(list = []) {
        const listWithDirections = list.map(posting => {
            if (!Number.isInteger(posting.Direction)) {
                return { ...posting, Direction: PostingDirection.Outgoing };
            }

            return posting;
        });

        this.model.TaxPostingsMode = ProvidePostingType.ByHand;

        this.setTaxPostingList({ ...this.model.TaxPostings, Postings: listWithDirections });
    }

    @action setSum = ({ value }) => {
        const sum = toFloat(value);

        if (sum === toFloat(this.model.Sum)) {
            return;
        }

        this.model.Sum = sum;

        this.validateField(`Sum`);
    };

    @action setPaymentPriority = ({ value }) => {
        this.model.PaymentPriority = value;
    };

    @action setIsBudgetaryDebt = ({ checked }) => {
        this.model.IsBudgetaryDebt = checked;

        if (checked && !this.model.Uin) {
            this.model.Uin = `0`;
        }
    };

    @action setDeductionWorker = ({ original }) => {
        this.model.DeductionWorkerId = original?.Id;
        this.model.DeductionWorkerFio = original?.Fio;
        this.model.DeductionWorkerInn = original?.Inn;
        this.model.DeductionWorkerDocumentNumber = original?.DefaultDocumentNumber;
        this.model.DefaultDocumentNumber = original?.DefaultDocumentNumber;
        this.validateField(`DeductionWorkerFio`);
        this.clearValidation(`DeductionWorkerDocumentNumber`);
    };

    @action setKbk = ({ value }) => {
        this.model.Kbk = value;
        this.validateField(`Kbk`);
    };

    @action setOktmo = ({ value }) => {
        this.model.Oktmo = value;
        this.validateField(`Oktmo`);
    };

    @action setUin = ({ value }) => {
        this.model.Uin = value;
        this.validateField(`Uin`);
    };

    @action setDeductionWorkerDocumentNumber = ({ value }) => {
        this.model.DeductionWorkerDocumentNumber = value;
        this.validateField(`DeductionWorkerDocumentNumber`);
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

    /* kontragent */

    @action setContractor = async ({ original }) => {
        const previous = this.model.Kontragent;
        const newValues = mapKontragentToModel(original);

        if (this.contractIsMainFirm) {
            this.setContract();
        }

        if (!newValues.KontragentId || newValues.SalaryWorkerId
            || (previous.KontragentId && newValues.KontragentId !== previous.KontragentId)
        ) {
            this.setContract();
        }

        this.model.Kontragent = newValues;

        if (this.validationState.Kontragent) {
            this.validateField(`Kontragent`);
        }

        await this.loadKontragentNameById();
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

    @action clearValidation = validationField => {
        const rules = validationRules[validationField];

        if (!rules) {
            return;
        }

        this.validationState[validationField] = null;
    };

    @action validateTaxPostingsList = () => {
        this.model.TaxPostings.Postings = taxPostingsValidator.getValidatedNegativeUsnList(this.model.TaxPostings.Postings, { isOsno: this.isOsno });
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
    }

    @action setPayerStatus = ({ value }) => {
        this.model.PayerStatus = value;
        this.validationState.PayerStatus = undefined;
    }

    @action correctPayerStatusUsingKontragentSettlementAccount = () => {
        if (isBailiffPayment(this.model.Kontragent?.KontragentSettlementAccount)) {
            this.setPayerStatus({ value: PayerStatus.BailiffPayment });
        }
    }
}

export default Actions;
