import { get } from '@moedelo/md-frontendcore/mdCore/HttpClient';

const urls = {
    get: '/Kontragents/Get',
    getName: '/Accounting/Kontragents/GetKontragentOrWorkerName',
    getContacts: '/Kontragents/Contacts/GetByKontragents',
    sendEmail: '/Kontragents/Mail/Send',
    getEmails: '/Kontragents/Autocomplete/GetKontragentWithEmails'
};

export default class KontragentService {
    static get(id) {
        return new Promise((resolve, reject) => {
            get(urls.get, { id })
                .then((response = {}) => {
                    const { Status, Value } = response;
                    if (Status) {
                        resolve(Value);
                    } else {
                        throw Error(`Status of response is ${Status}`);
                    }
                })
                .catch((error) => {
                    reject(error);
                })
        });
    }

    static getName(id) {
        let kontragentType = 1;
        return get(urls.getName, { id, kontragentType }).then(resp => {
            return { Name: resp.Name, Id: id };
        });
    }

    static getContacts(ids) {
        const data = {
            ids
        };
        return new Promise((resolve, reject) => {
            get(urls.getContacts, data)
                .then((response = {}) => {
                    const { Status, List } = response;
                    if (Status) {
                        resolve(List);
                    } else {
                        throw Error(`Status of response is ${Status}`);
                    }
                })
                .catch((error) => {
                    reject(error);
                })
        });
    }

    static getEmails({ query = '', exceptIds = [] }) {
        const data = {
            query,
            count: 5,
            exceptIds
        };
        return get(urls.getEmails, data)
            .then(response => {
                const { Status, List } = response;
                if (Status === true && List) {
                    return List.map(item => {
                        return {
                            text: item.Name,
                            value: item.Id,
                            obj: item
                        }
                    });
                } else {
                    throw Error(response);
                }
            });
    }

    static sendEmail($form, data) {
        return new Promise((resolve, reject) => {
            $form.ajaxSubmit({
                url: urls.sendEmail,
                type: 'POST',
                data,
                success() {
                    resolve()
                },
                error() {
                    reject()
                }
            })
        })
    }

    static autocompleteForDocs({ query = '', count = 5 }) {
        const url = '/Accounting/KontragentClosingDocs/GetAutocompleteForWaybill';
        return get(url, { query, count, docType: 2 }).then(resp => resp.List);
    }
}
