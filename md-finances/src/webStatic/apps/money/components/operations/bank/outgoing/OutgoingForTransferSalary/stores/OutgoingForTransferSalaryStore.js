import { makeObservable, observable, reaction } from 'mobx';
import { toFloat, toFinanceString } from '@moedelo/frontend-core-v2/helpers/converter';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import NotificationManager from '@moedelo/frontend-core-react/helpers/notificationManager';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import Actions from './Actions';
import MoneyOperationTypeResources from '../../../../../../../../resources/MoneyOperationTypeResources';
import defaultModel from './Model';
import MoneyOperationService from '../../../../../../../../services/newMoney/moneyOperationService';
import validationRules from './../validationRules';
import DocumentStatusEnum from '../../../../../../../../enums/DocumentStatusEnum';
import notTaxableReasonGetter from './../notTaxableReasonGetter';
import requiredFieldForAccountingPostings from '../../../../../../../../resources/newMoney/requiredFieldForAccountingPostings';
import AvailableTaxPostingDirection from '../../../../../../../../enums/newMoney/AvailableTaxPostingDirectionEnum';
import ContractTypesEnum from '../../../../../../../../enums/newMoney/ContractTypesEnum';

class OutgoingForTransferSalaryStore extends Actions {
    @observable validationState = {
        Number: ``,
        Date: ``,
        Sum: ``,
        Description: ``
    };

    @observable model = { ...defaultModel };

    @observable workerCharges = [];

    availableTaxPostingDirection = AvailableTaxPostingDirection.Outgoing;

    constructor(options) {
        super(options);
        makeObservable(this);

        this._initModel(defaultModel, options.operation);

        /* UnderContract в дальшейшем заменить на PaymentType */
        Object.assign(this.model, { UnderContract: options.operation.PaymentType });

        /** Для ИП !!!         *
         * если вдруг после импорта операций приходит невалидный тип договора:
         * дивиденды, зарплатный.
         *
         * нужно сменить его на Трудовой.
         * */
        const invalidContractTypesForIp = [
            ContractTypesEnum.Dividends.value
        ];

        if (!this.Requisites.IsOoo && invalidContractTypesForIp.includes(options.operation.UnderContract)) {
            Object.assign(this.model, { UnderContract: ContractTypesEnum.Employment.value });
        }

        !this.model.Description && this.handleDescriptionMessage();

        this.initWorkerCharges();

        /** необходимость такова, что нужно определять
         *  возможность скачивания при открытии операции.
         *  Если оставить это в computed поле, то при изменении
         *  списка сотрудников вохможность будет возникать */
        this.customCanDownload = (!this.isCopy() && this.model.Id > 0 && this.workerCharges.length === 1) || this.isSalaryProject;

        this.initAccountingPostings();
        this.initTaxPostings();
        this.initReactions();
    }

    initReactions = () => {
        reaction(() => [this.model.UnderContract], () => {
            this.workerCharges = [];
            this.addEmptyWorkerCharge();
            this.setSum();
        });

        reaction(() => this.model.Date, async Date => {
            if (!this.validationState.Date) {
                this.setTaxationSystem(await taxationSystemService.getTaxSystem(Date));
            }
        });

        reaction(this.getFieldsForTaxPostings, this.loadTaxPostings);
        reaction(this.getFieldsForAccountingPostings, this.loadAccountingPostings);
    };

    handleDescriptionMessage = () => {
        if (!this.isNew) {
            return;
        }

        const workers = this.workerCharges.filter(worker => worker.WorkerId > 0);
        const isDividends = this.model.UnderContract === ContractTypesEnum.Dividends.value;

        let msg = `Выплата ${isDividends ? `дивидендов` : `физлицам`}. НДС не облагается.`;

        if (workers.length === 1) {
            const { WorkerName, Charges } = workers[0];
            const descriptions = [];
            let deductionsSum = 0;
            Charges.filter(charge => charge.ChargeId !== 0).forEach(charge => {
                descriptions.push(charge.Description);
                deductionsSum += charge.DeductionSum || 0;
            });
            const descriptionsStr = descriptions.join(`, `);
            const name = WorkerName.length ? `${WorkerName}` : ``;
            const paymentSubject = isDividends ? `дивидендов` : `физлицу`;
            const deductionStr = deductionsSum > 0
                ? `//ВЗС//${toFinanceString(deductionsSum).replace(/\s+/g, ``).replace(`,`, `-`)}//`
                : ``;
            msg = `Выплата ${paymentSubject} ${name}${descriptionsStr.length ? ` ${descriptionsStr}` : ``}.${deductionStr} НДС не облагается.`;
        }

        if (workers.length > 1) {
            msg = `Выплата по з/п проекту от ${this.model.Date} на сумму ${this.formattedTotalSum} руб.`;
        }

        this.setDescription(msg);
    };

    /* override */
    getFieldsForTaxPostings = () => {
        return JSON.stringify([
            this.model.Date,
            this.model.Sum,
            this.model.Status,
            this.model.UnderContract
        ]);
    };

    isValid = () => {
        return !Object.keys(this.validationState).find(key => {
            return this.validationState[key] !== ``;
        }) && this.isAllWorkersValid;
    };

    validateModel() {
        Object.keys(validationRules).forEach(fieldName => {
            this.validateField(fieldName, this.model[fieldName]);
        });
        this.validateWorkers();
    }

    validateWorkers = () => {
        const addedWorkersList = this.workerCharges.filter(worker => worker.WorkerId > 0 || worker.hasChargesWithSum);

        if (addedWorkersList.length) {
            addedWorkersList.forEach(worker => worker.fullWorkerValidation());
        } else {
            this.workerCharges[0].fullWorkerValidation();
        }
    }

    /* override */
    modelForSave = () => {
        const { model } = this;
        const operationType = MoneyOperationTypeResources.PaymentOrderOutgoingForTransferSalary;
        const EmployeePayments = this.workerCharges.filter(worker => worker.WorkerId > 0).map(workerModel => workerModel.toJS());

        return {
            Id: model.Id,
            BaseDocumentId: model.BaseDocumentId,
            DocumentBaseId: model.DocumentBaseId,
            OperationType: operationType.value,
            Direction: operationType.Direction,
            Date: dateHelper(model.Date, `DD.MM.YYYY`).format(`YYYY-MM-DD`),
            Number: model.Number,
            SettlementAccountId: model.SettlementAccountId,
            PaymentType: model.UnderContract,
            Description: model.Description,
            ProvideInAccounting: model.ProvideInAccounting,
            Sum: model.Sum,
            IsPaid: model.Status === DocumentStatusEnum.Payed,
            EmployeePayments
        };
    }

    /* override */
    getTaxPostingsExplainingMsg = () => {
        return notTaxableReasonGetter.get({ taxationSystem: this.TaxationSystem, UnderContract: this.model.UnderContract, isIp: this.isIp });
    };

    /* override */
    getFieldsForAccountingPostings = () => {
        return JSON.stringify([
            this.model.Sum,
            this.model.Date,
            this.model.SettlementAccountId,
            this.model.Status,
            this.model.ProvideInAccounting,
            ...(this.workerCharges.map(worker => worker.WorkerId))
        ]);
    };

    /* override */
    getRequiredFieldsForAccountingPostingMsg = () => {
        if (!this.model.Date || this.validationState.Date) {
            return requiredFieldForAccountingPostings.date;
        }

        if (this.model.Status === DocumentStatusEnum.NotPayed) {
            return requiredFieldForAccountingPostings.notPaid;
        }

        if (!this.workerCharges.some(worker => worker.WorkerName.length)) {
            return requiredFieldForAccountingPostings.workerName;
        }

        if (!toFloat(this.model.Sum)) {
            return requiredFieldForAccountingPostings.workerSum;
        }

        return null;
    };

    /* override */
    getAccountingPostingsExplainingMsg = () => {
        return this.getRequiredFieldsForAccountingPostingMsg();
    }

    handleWorkerRemove = workerModel => {
        if (this.workerCharges.length > 1) {
            this.workerCharges.remove(workerModel);
        }
    }

    getContractTypesData = () => {
        const contractTypesData = Object.values(ContractTypesEnum)
            .filter(type => type.value !== ContractTypesEnum.SalaryProject.value &&
                type.value !== ContractTypesEnum.GPDBySalaryProject.value &&
                type.value !== ContractTypesEnum.DividendsBySalaryProject.value)
            .map(type => type);

        if (this.isSalaryProject) {
            return [ContractTypesEnum.SalaryProject, ContractTypesEnum.GPDBySalaryProject, ContractTypesEnum.DividendsBySalaryProject];
        }

        if (!this.Requisites.IsOoo) {
            return contractTypesData.filter(type => type.value !== ContractTypesEnum.Dividends.value || type.value !== ContractTypesEnum.DividendsBySalaryProject.value);
        }

        return contractTypesData;
    }

    getAddedWorkersIdsArray(currentId) {
        return this.workerCharges.filter(charge => charge.WorkerId && charge.WorkerId !== currentId).map(charge => charge.WorkerId);
    }

    downloadRegistry = () => {
        return MoneyOperationService.downloadRegistry(this.model.BaseDocumentId).catch(e => {
            const errorMessages = Object.keys(e.errors[0]).map(i => {
                return e.errors[0][i][0];
            });

            NotificationManager.show({
                message: `<div>${errorMessages.join(`</br>`)}</div>`,
                type: `warning`,
                duration: 5000,
                onClick: () => NotificationManager.hide()
            });
        });
    }
}

export default OutgoingForTransferSalaryStore;
