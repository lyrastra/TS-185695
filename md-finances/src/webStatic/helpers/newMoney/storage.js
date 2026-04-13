import { getId } from '@moedelo/frontend-core-v2/helpers/companyId';

const storage = {
    save(key, obj) {
        const companyStorage = JSON.parse(localStorage.getItem(getId()) || `null`) || {};

        companyStorage[key] = obj;
        localStorage.setItem(getId(), JSON.stringify(companyStorage));
    },

    get(key) {
        const companyStorage = JSON.parse(localStorage.getItem(getId()) || `null`) || {};

        return companyStorage[key];
    }
};

export default storage;
