class ChargesStore {
    constructor() {
        this.chargesList = {};
    }

    get = ({ workerId, paymentType } = {}) => {
        return this.chargesList
            && this.chargesList[workerId]
            && this.chargesList[workerId][paymentType];
    }

    set = ({ workerId, paymentType, list }) => {
        if (workerId && typeof paymentType !== `undefined` && list) {
            if (!this.chargesList[workerId]) {
                this.chargesList[workerId] = {};
            }

            this.chargesList[workerId][paymentType] = list;
        }
    }

    clear = () => {
        this.chargesList = {};
    }

    getFullState = () => {
        return this.chargesList;
    }
}

export default ChargesStore;
