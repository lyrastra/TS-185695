/* global Marionette */
/* temporary, while using Backbone/Marionette */

import React from 'react';
import ReactDOM from 'react-dom';
import OperationBills from './OperationBills';

const operationBillsBehavior = Marionette.Behavior.extend({
    initialize(self, data) {
        this.view = data.options;
        this.model = data.options.model;
        this.$additionalBox = data.options.$additionalBox || $('<div>');

        if (this.model.operationBillsStore) {
            this.view.billsComponent = React.createElement(OperationBills, {
                store: this.model.operationBillsStore
            }, null);

            ReactDOM.render(this.view.billsComponent, this.$additionalBox[0], () => {
                this.$additionalBox.removeClass('hidden');
            });
        }
    },

    destroy() {
        ReactDOM.unmountComponentAtNode(this.$additionalBox[0]);
        this.$additionalBox.html('').addClass('hidden');
    }
});

export default operationBillsBehavior;