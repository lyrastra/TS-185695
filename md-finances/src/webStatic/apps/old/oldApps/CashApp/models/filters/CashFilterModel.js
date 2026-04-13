/* eslint-disable */
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';

(function(cash) {
    cash.Models.Filters.CashFilterModel = Backbone.FilterObject.BaseFilterObject.extend({
        
        localStorage: new Store('CashFilterModel'),

        defaults: {
            Sorter: {
                Sort: Enums.TableSorter.Types.Date,
                SortDirection: Enums.TableSorter.SortOrder.Desc
            },
            Filter: {}
        },

        description: {
            DestinationName: {
                Name: 'контрагент/сотрудник'
            },
            Number: {
                Name: 'номер'
            },
            Sum: {
                Name: 'сумма'
            },
            MinSum: {
                Name: 'сумма больше'
            },
            MaxSum: {
                Name: 'сумма меньше'
            },
            SyntheticAccount: {
                Name: 'кор. счет'
            },
            IsPosting: {
                getValue(isPosting) {
                    if (isPosting) {
                        return 'проведен';
                    }
                    return 'непроведен';
                }
            },
            IsIncoming: {
                getValue(isIncoming) {
                    if (isIncoming) {
                        return 'входящий';
                    }
                    return 'исходящий';
                }
            },
            DirectionType: {
                getValue(type) {
                    if (type == Direction.Outgoing) {
                        return 'расходный ордер';
                    }
                    return 'приходный ордер';
                }
            },
            ProvideInAccounting: {
                getValue(val) {
                    return val ? 'проведенные' : 'непроведенные';
                }
            },
            Query: {
                Name: ''
            }
        }
    });
}(Cash));
