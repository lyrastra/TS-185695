/* eslint-disable */

import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

(function($) {
    'use strict';

    var projectForPurseAutocompleteUrl = '/Contract/Autocomplete/ContractForPurseOperationsAutocomplete';

    $.fn.projectAutocomplete = function(model, options) {
        var settings = options || {};
        var handlers = settings.handlers || {};
        var $el = $(this);
        var defaults = {
            url: '/Contract/Autocomplete',
            onSelect: function(item) {
                updateModelWithContract(model, item.object || {});
            },
            getData: function() {
                if (model && model.get) {
                    return {
                        kontragentId: model.get('KontragentId'),
                        withMainContract: !_.isUndefined(settings.withMainContract) ? settings.withMainContract : true,
                        kind: settings.kind
                    };
                }
            },
            onBlur: function() {
                clearModel(model, {
                    needClearNumber: settings.needClearNumber
                });
            },
            onCreate: function() {
                mdNew.Contracts.addDialogHelper.showDialog({
                    data: {
                        Direction: settings.direction,
                        KontragentId: model.get('KontragentId'),
                        KontragentName: model.get('KontragentName'),
                        isReceivedOperation: settings.isReceivedOperation,
                        isMediationOperation: settings.isMediationOperation
                    },
                    onSave: function(options) {
                        $el.val(options.ProjectNumber).change();

                        var data = {
                            KontragentId: options.KontragentId,
                            KontragentName: options.KontragentName,
                            IsMainContract: false,
                            Number: String(options.ProjectNumber),
                            DocumentBaseId: options.DocumentBaseId,
                            Id: options.ProjectId
                        };

                        updateModelWithContract(model, data);
                        updateKontragentModelWithContract(model, data);

                        handlers.onCreate && handlers.onCreate(data);
                    }
                });
            },
            clean: function() {
                clearModel(model, {
                    needClearNumber: settings.needClearNumber
                });
            },
            addLink: true,
            addLinkName: 'договор'
        };
        if (model && model.on) {
            model.on('change:KontragentId', function() {
                if (model.get('ContractKontragentId') !== model.get('KontragentId')) {
                    $el.val('').blur();
                    clearModel(model, {
                        needClearNumber: settings.needClearNumber
                    });
                }
            });
        }

        var _options = _.extend({}, defaults, settings);

        var autocomplete = new window.mdAutocomplete({
            url: _options.url,
            el: $el,
            className: 'projectAutocomplete',
            onSelect: _options.onSelect,
            onBlur: _options.onBlur,
            onCreate: _options.onCreate,
            data: _options.getData,
            settings: _options
        });

        autocomplete.parse = function(list) {
            return _.map(list, function(contract) {
                var label = contract.IsMainContract ? contract.Name : getContractLabel(contract);
                return {
                    label: label,
                    value: contract.IsMainContract ? contract.Name : contract.Number,
                    object: contract
                };
            });
        };

        return autocomplete;
    };

    $.fn.projectForPurseAutocomplete = function(model, options) {
        options = options || {};

        options.url = projectForPurseAutocompleteUrl;
        options.getData = function() {
            if (model && model.get) {
                return {
                    kontragentId: model.get('KontragentId'),
                    withPaymentAgents: options.withPaymentAgents
                };
            }
        };
        options.needClearNumber = false;
        return $.fn.projectAutocomplete.call(this, model, options);
    };

    function clearModel(model, options) {
        var needClearNumber = options.needClearNumber;

        if (model && model.unset) {
            model.get('ProjectId') && model.unset('ProjectId');
            needClearNumber !== false && model.get('ProjectNumber') && model.unset('ProjectNumber');
            model.get('ContractBaseId') && model.unset('ContractBaseId');
            model.get('ContractKontragentId') && model.unset('ContractKontragentId');
        }
    }

    function updateModelWithContract(model, contract) {
        if (model && model.set) {
            var kontragentId = contract.KontragentId;

            model.set({
                ContractKontragentId: kontragentId,
                KontragentId: kontragentId,
                ProjectNumber: contract.IsMainContract ? contract.Name : contract.Number,
                ContractBaseId: contract.DocumentBaseId,
                ProjectId: contract.Id
            });
        }
    }

    function updateKontragentModelWithContract(model, contract) {
        if (model && model.set) {
            model.set({
                KontragentName: contract.KontragentName
            });

            if (contract.KontragentName) {
                model.set({
                    KontragentName: contract.KontragentName
                });
            }
        }
    }

    function getContractLabel(contract) {
        var minDate = '01.01.0001';

        var number = '№ ' + contract.Number;
        var date = '';
        if (contract.Date && contract.Date !== minDate) {
            var dateObj = dateHelper(contract.Date, 'DD.MM.YYYY');
            var format = dateObj.isSame(dateHelper(), 'year') ? 'D MMMM' : 'D MMMM YYYY';
            date = ' от ' + dateObj.format(format);
        }
        return [number, date].join('');
    }

})(jQuery);
