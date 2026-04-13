import { MdCollectionView } from '@moedelo/md-frontendcore/mdCore';
import WorkerChargesListRow from '../WorkerChargesListRow';
import template from './template.hbs';
import style from './style.m.less';

export default MdCollectionView.extend({
    template,

    initialize({ collection, handlers, service, readonly}) {
        this.collection = collection;
        this.handlers = handlers;
        this.service = service;
        this.readonly = readonly;
    },

    childViewContainer: 'ul',

    createChildView: function(model) {
        return new WorkerChargesListRow({
            model,
            service: this.service,
            handlers: this.handlers,
            readonly: this.readonly
        });
    },

    templateHelpers() {
        return {
            style
        }
    }
});
