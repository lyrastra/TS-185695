(function (bank) {

    var purses;

    bank.PurseGetter = function(){
        initialize();

        return {
            getPurses: getPurses,
            loadPurses: loadPurses,
            clearCache: _clearCache
        };

        function initialize(options){
            if(!purses) {
                purses = new Backbone.Collection();
            }
        }

        function loadPurses(){
            if(!purses.xhr){
                purses.xhr = $.ajax('/Accounting/PurseOperation/GetPurseKontragentList').done(onLoad);
            }

            return purses.xhr;
        }

        function onLoad(resp){
            purses.reset(resp.List);
        }

        function getPurses(){
            return purses;
        }
    };

    function _clearCache() {
        purses.xhr = null;
    }

    bank.PurseGetter.clearCache = _clearCache;

})(Bank);