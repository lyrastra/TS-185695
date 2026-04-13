window.Common = (function (common) {

    common.Data = common.Data || {};

    common.Data.TaxationSystemType = {
        Default: 0,
        Usn: 1,
        Osno: 2,
        Envd: 3,
        UsnAndEnvd: 4,
        OsnoAndEnvd: 5,
        Patent: 6
    };
    
    return common;
    
})(window.Common);