(function (mainModule) {
    'use strict';

    mainModule.Collections.Main.FakeDialogCollection = Backbone.Collection.extend({
        url: '/BizDocuments/Confirming/GetByType',
        parse: parse
    });
    
    /** @access public */
    function parse(response) {
        return response && response.List;
    }

})(PrimaryDocuments);