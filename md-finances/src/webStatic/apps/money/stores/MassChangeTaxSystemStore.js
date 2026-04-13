import {
    observable, action, computed, makeObservable
} from 'mobx';
import mrkStatService from '@moedelo/frontend-common-v2/apps/marketing/services/mrkStatService';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import {
    getTaxationSystemsForChange,
    parseOperationsForChangeTaxStructure
} from '../../../helpers/MoneyOperationHelper';
import OperationTablesService from '../../../services/newMoney/operationTablesService';

const changeTaxSystemMrkEventId = `mass_change_tax_system`;

export default class MassChangeTaxSystemStore {
    @observable loading = false;
    @observable isModalVisible = false;
    @observable validList = [];
    @observable checkedOperations = [];
    @observable invalidCount = 0;
    @observable newTaxSystem = null;

    constructor(options) {
        makeObservable(this);

        this.taxationSystemsForAllYears = options.taxationSystemsForAllYears;
        this.patentsForAllYears = options.patentsForAllYears;
        this.currentTaxationSystem = options.currentTaxationSystem;
        this.hasPatents = options.hasPatents;
        this.lastClosedPeriod = options.lastClosedPeriod;

        this.initDefaultTaxSystemData();
    }

    initDefaultTaxSystemData = () => {
        this.availableTaxSystems = this.getTaxationSystemsForDropdown();
        this.newTaxSystem = this.availableTaxSystems[0]?.value;
    };

    getTaxationSystemsForDropdown = () => {
        const { taxationSystemsForAllYears, patentsForAllYears } = this;
        const years = new Set();

        this.checkedOperations.forEach(operation => {
            const year = dateHelper(operation.Date).year();

            years.add(year);
        });

        return getTaxationSystemsForChange(years, taxationSystemsForAllYears, patentsForAllYears);
    };

    @action setLoading = isLoading => {
        this.loading = isLoading;
    };

    @action setModalVisibility = isVisible => {
        this.isModalVisible = isVisible;

        if (!isVisible) {
            setTimeout(() => this.clearOperations(), 200);
        }
    };

    @action clearOperations = () => {
        this.initDefaultTaxSystemData();
    };

    @action setCheckedOperations = (checkedOperations = []) => {
        this.checkedOperations = checkedOperations;

        this.validateCheckedOperations();
    };

    @action validateCheckedOperations = () => {
        const {
            lastClosedPeriod, taxationSystemsForAllYears, patentsForAllYears, checkedOperations, newTaxSystem
        } = this;
        const { validList, invalidCount } = parseOperationsForChangeTaxStructure(checkedOperations, lastClosedPeriod, taxationSystemsForAllYears, patentsForAllYears, newTaxSystem);

        this.validList = validList;
        this.invalidCount = invalidCount;
    };

    @action updateInvalidCount = () => {
        const {
            lastClosedPeriod, taxationSystemsForAllYears, patentsForAllYears, checkedOperations, newTaxSystem
        } = this;
        const { invalidCount } = parseOperationsForChangeTaxStructure(checkedOperations, lastClosedPeriod, taxationSystemsForAllYears, patentsForAllYears, newTaxSystem);

        this.invalidCount = invalidCount;
    };

    @action setNewTaxSystem = ({ value }) => {
        this.newTaxSystem = value;
    };

    @action changeOperationsTaxSystem = async () => {
        const data = {
            DocumentBaseIds: this.validList.map(o => o.DocumentBaseId),
            TaxationSystemType: this.newTaxSystem
        };

        this.setLoading(true);

        mrkStatService.sendEventWithoutInternalUser(changeTaxSystemMrkEventId);

        await OperationTablesService.changeOperationTaxationSystem(data);

        if (this.validList.length < 2) {
            this.setLoading(false);
        } else {
            setTimeout(() => this.setLoading(false), 10000);
        }

        this.setModalVisibility(false);
    };

    @computed get canShowChangeButton() {
        return this.validList.length > 0 && this.isDualTaxationSystem();
    }

    isDualTaxationSystem = () => {
        return this.getTaxationSystemsForDropdown().length > 1;
    };
}

