/* eslint-disable */

(function(common) {
    common.Data.NdsTypes = {
        None: -1,
        Nds0: 0,
        Nds10: 10,
        Nds18: 18,
        Nds110: 110,
        Nds118: 118
    };

    common.Data.BankAndCashNdsTypes = {
        None: -1,
        Nds0: 0,
        Nds10: 1,
        Nds18: 2,
        Nds110: 3,
        Nds118: 4,
        Nds20: 5,
        Nds120: 6,        
        Nds5: 7,
        Nds5To105: 8,
        Nds7: 9,
        Nds7To107: 10,
        Nds22: 11,
        Nds22To122: 12,
        Empty: 99
    };

    common.Data.AccountNdsType = {
        UnknownNds: 0,
        WithoutNds: 1,
        Nds0: 2,
        Nds10: 3,
        Nds18: 4
    };
}(Common.module('Data')));
