(function (main) {
    'use strict';

    var md = Md;

    main.PrimaryDocuments = {
        Models: {
            Main:{}
        },
        Collections: {
            Main:{}
        },
        Views: {
            Main:{},
            Documents: {
                BaseDocumentView: {},
                Actions: {},
                Mixins:  {}
            }
        },
        Mixins: {},
        Controls: {},
        Utils: {},
        Urls: {},
        module: function() {
            return main.PrimaryDocuments;
        }
    };

    if(md){
        md.PrimaryDocuments = main.PrimaryDocuments;
    }

})(window);