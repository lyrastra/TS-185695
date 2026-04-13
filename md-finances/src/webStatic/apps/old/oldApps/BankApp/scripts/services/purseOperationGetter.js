/* eslint-disable */

import Direction from '@moedelo/frontend-enums/mdEnums/Direction';

(function(bank) {
    bank.PurseOperationGetter = function() {
        const incoming = {
            model: bank.Models.IncomingPurseDocument,
            view: bank.Views.IncomingPurseDocument
        };
        const outgoing = {
            model: bank.Models.OutgoingPurseDocument,
            view: bank.Views.OutgoingPurseDocument
        };

        return {
            createNewOperation,
            createOperationWithData: createOperation,
            loadOperation,
            copyOperation
        };

        function createNewOperation(direction, purseId) {
            const options = direction === Direction.Incoming ? incoming : outgoing;

            const model = new options.model();
            const view = new options.view({ model });
            model.load({
                success() {
                    model.set('PurseId', purseId);
                    view.render();
                }
            });
            view.once('save', bank.PurseGetter.clearCache);
            return view;
        }

        function getOperationByType(type) {
            return type === Md.Data.PurseOperationType.Income ? incoming : outgoing;
        }

        function loadOperation(id, onLoad, context) {
            return $.ajax({
                url: '/Accounting/PurseOperation/GetPurseOperation/',
                data: { documentBaseId: id }
            }).done((resp) => {
                const view = createOperation(resp);
                onLoad.call(context, view);
            });
        }

        function createOperation(data) {
            const options = getOperationByType(data.PurseOperationType);
            data = options.model.prototype.parse(data);
            if (data?.NdsType === null) {
                data = {...data, NdsType: 99}
            }

            let model = new options.model(data);

            let action = model.get('Id') > 0 ? 'edit' : 'new';

            if (window.location.hash.match(/copy/gi)) {
                action = 'copy';
            }

            model.set('action', action);

            const operation = new options.view({ model }).render();

            operation.once('save', bank.PurseGetter.clearCache);

            return operation;
        }

        function copyOperation(id, onLoad, context) {
            return $.ajax('/Accounting/PurseOperation/CopyOperationByBaseId/{0}'.format(id)).done((resp) => {
                const view = createOperation(resp);
                onLoad.call(context, view);
            });
        }
    };
}(Bank));
