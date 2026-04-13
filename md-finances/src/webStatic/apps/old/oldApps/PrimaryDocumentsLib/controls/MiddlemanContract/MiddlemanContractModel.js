(function (primaryDocuments) {

    primaryDocuments.Models.MiddlemanContractModel = Backbone.Model.extend({
        extendValidation: function(extraValidation) {
            _assert(_.isObject(this.validation), 'Unable extend non-object validation');
            _assert(_.isObject(extraValidation), 'Unable extend validation rules by non-object');

            var validation = this.validation = _.clone(this.validation);
            _.each(extraValidation, function (v, k) {
                if (!validation.hasOwnProperty(k)) {
                    validation[k] = v;
                }
                else if (_.isArray(validation[k])) {
                    var arr = validation[k].slice();
                    if (_.isArray(v)) {
                        $.merge(arr, v);
                    } else {
                        arr.push(v);
                    }
                    validation[k] = arr;
                }
            });
        },

        defaults: function () {
            return {
                ContractType: Common.Data.MiddlemanContractType.Agency,
                IsCompensated: false
            };
        },

        validation: {
            MiddlemanName: [
                {
                    required: true,
                    msg: 'Укажите заказчика'
                },
                {
                    fn: function() {
                        var middlemanId = parseInt(this.get('MiddlemanId'));
                        if (!middlemanId) {
                            return;
                        }

                        var radio = Backbone.Wreqr.radio.channel('document');
                        if (radio.reqres.request('get', 'IsCompensated')){
                            var kontragentId = radio.reqres.request('get', 'KontragentId');
                            if (middlemanId === parseInt(kontragentId)) {
                                return 'Заказчик не может быть тем же самым, что и контрагент';
                            }
                        }
                    }
                }
            ],
            ContractNumber: {
                required: function (){
                    return !this.get('IsNew') && !this.get('Id');
                },
                msg: 'Укажите посреднический договор'
            }
        }
    });

    function _assert(condition, msg) { // spiridonov: I don't know where I can place such function for 'old' code
        if (!condition) {
            throw new Error(msg);
        }
    }

})(PrimaryDocuments);