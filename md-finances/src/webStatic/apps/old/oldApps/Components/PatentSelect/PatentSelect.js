import patentMapper from './mappers/patentMapper';
import template from './template.hbs';

export default Marionette.ItemView.extend({
    template,

    initialize(options) {
        this.model = options.model;
        this.activePatents = options.activePatents;
    },

    onRender() {
        this.bind();
        this.$(`[data-bind=PatentId]`).change();
    },

    bindings: {
        'select[data-bind=PatentId]': {
            observe: `PatentId`,
            selectOptions: {
                collection() {
                    const activePatents = [{ Id: 0, ShortName: `&nbsp;` }].concat(this.activePatents);
                    return patentMapper.mapToSelect(activePatents);
                }
            }
        }
    }
});
