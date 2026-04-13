import * as mobx from 'mobx';

const {
    observable, action, computed, makeObservable
} = mobx;

class OperationModel {
    @observable Checked = false;

    PaidStatus;

    constructor(store, options) {
        makeObservable(this);
        Object.keys(options).forEach(key => {
            this[key] = options[key];
        });
    }

    @action uncheck() {
        this.Checked = false;
    }

    @action toggleChecked() {
        this.Checked = !this.Checked;
    }

    @computed get isChecked() {
        return this.Checked;
    }

    @computed get canShowImportRules() {
        return this.ImportRules?.length > 0;
    }
}

export default OperationModel;
