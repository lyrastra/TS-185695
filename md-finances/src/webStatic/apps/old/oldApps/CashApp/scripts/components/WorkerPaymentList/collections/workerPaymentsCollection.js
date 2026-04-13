/* global _, Cash */
// eslint-disable-next-line func-names
(function(components) {
    // eslint-disable-next-line no-unused-vars
    const TableRowModel = Backbone.Model.extend({
        validation: {
            Sum: {
                required() {
                    return this.isFirstItem() || !this.isEmpty();
                },
                msg: `Введите сумму`
            },

            WorkerName: {
                required() {
                    return this.isFirstItem() || !this.isEmpty();
                },
                msg: `Введите сотрудника`
            }
        },

        isEmpty() {
            return !(this.get(`Sum`) || this.get(`WorkerId`));
        },

        isFirstItem() {
            return this.collection.indexOf(this) === 0;
        }
    });

    const TablePayrollRowModel = Backbone.Model.extend({
        validation: {
            WorkerName: {
                required() {
                    return this.isFirstItem() || !this.isEmpty();
                },
                msg: `Введите сотрудника`
            }
        },

        isEmpty() {
            return !(this.get(`Charges`) || this.get(`WorkerId`));
        },

        isFirstItem() {
            return this.collection.indexOf(this) === 0;
        }
    });

    // eslint-disable-next-line no-param-reassign
    components.WorkerPaymentsCollection = Backbone.Collection.extend({
        model(attrs, options) {
            return new TablePayrollRowModel(attrs, options);
        },

        initialize(models) {
            if (!models || models.length === 0) {
                this.add({});
            }
        },

        getData() {
            let data = this.map((item) => {
                if (!item.isEmpty()) {
                    return item.toJSON();
                }
                return null;
            });

            data = _.compact(data);

            return data;
        }
    });
}(Cash.Components));
