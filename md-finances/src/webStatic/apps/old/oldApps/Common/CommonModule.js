(function(window) {
	'use strict';
	if(!window.Md){
		window.Md = {};
	}
	var md = window.Md ;

	md.Common = window.Common = {
        Views: {
            Main:{},
            Dialogs: {}
        },
        Models: {
            Dialogs: {}
        },
        Collections: {},
        Data: {
            
        },
        Helpers: {

        },
        Utils: {
            Widgets: {},
            Creators: {}
        },
        Behaviours: {},
        Filters: {},
        Main: {},
        Tests: {},
        Mixin: {}, // хз почему он так называется
        Mixins: {},
        Enums: {},
        Controls: {},
        Urls: {},
        Options: {},
        Controllers: {},
        module: function() {
            return function() {
                return window.Common;
            };
        }()
    };

})(window);