(function (primaryDocuments, common) {

    primaryDocuments.Controls.NdsRateControl = common.Controls.BaseControl.extend({

//        template: "controls/NdsControl",
        
        ndsRates: function() {
            return [
                { name: 'None', text: '', value: common.Data.NdsTypes.None },
                { name: 'Nds18', text: '18%', value: common.Data.NdsTypes.Nds18 },
                { name: 'Nds10', text: '10%', value: common.Data.NdsTypes.Nds10 },
                { name: 'Nds110', text: '10/110', value: common.Data.NdsTypes.Nds110 },
                { name: 'Nds118', text: '18/118', value: common.Data.NdsTypes.Nds118 }
            ];
        },

        initialize: function (options) {
            this.options = options;
            
            common.Controls.BaseControl.prototype.initialize.call(this, options);
            common.Helpers.Mixer.addMixin(this, common.Mixin.OnChangeFieldMixin, true);
            this.model.on("change:InvoiceType", this.onChangeDocType, this);
        },

        onRender: function () {
            var view = this;
            view.fillNdsRates();
            view.onChangeDocType();
        },
        
        fillNdsRates: function () {
            var view = this,
                offNdsRates = view.options.offNdsRates || [],
                ndsRatesNode = view.$('[name=NdsType]');

            view.excludeNdsRates(offNdsRates);

            ndsRatesNode.render(_.result(view, 'ndsRates'), {
                type: {
                    value: function () {
                        return this.value;
                    },
                    text: function () {
                        return this.text;
                    }
                }
            });

            ndsRatesNode.mdSelectToUlSelect();
            view.selectNdsRate();
        },
        
        selectNdsRate: function () {
            var view = this,
                defaultRate = view.options.defaultRate,
                value;
            
            if (view.model.get('NdsType')) {
                value = view.model.get('NdsType');
            } else if (defaultRate) {
                value = defaultRate;
            } else {
                value = view.$('[name=NdsType]').children().first().val();
            }

            view.$('[name=NdsType]').val(value).triggerHandler('change');
            view.model.set('NdsType', value);
        },

        excludeNdsRates: function (offNdsRates) {
            var view = this;
            var ndsRates = _.result(view, 'ndsRates');

            ndsRates = _.map(ndsRates, function (obj) {
                if (_.contains(offNdsRates, obj.value)) {
                    return null;
                }
                return obj;
            });

            view.ndsRates = _.compact(ndsRates);
        },
        
        onChangeDocType: function() {
            var model = this.model,
               invoiceType = parseInt(model.get("InvoiceType"), 10);

            if (invoiceType !== common.Data.InvoiceType.Advance) {
                this.hide();
            } else {
                this.selectNdsRate();
                this.show();
            }
        }
    });

})(PrimaryDocuments, Common, window);
