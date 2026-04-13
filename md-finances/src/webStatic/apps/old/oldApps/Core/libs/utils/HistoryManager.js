var _HistoryManager = function (){
    var manager = this;

    this.initialize = function() {
        manager.History = {
            Current: [],
            Previous: localStorage.getItem("appHistory") || []
        };
    },    
    this._lastPageException = function() {
        this.Previous = _.initial(this.Previous);
    },    
    this.getPreviosPage = function() {
        var last = _.last(manager.History.Previous);
        if (last) {
            if (last.route == Backbone.history.fragment) {
                return manager.History.Previous[manager.History.Previous.length - 2];
            }
            return last;
        }
    },
    this.goToPreviosPage = function() {
        var backUrl = undefined;
        if (manager.History.Previous && manager.History.Previous.length > 1) {
            
            while (manager.History.Previous.length && _.last(manager.History.Previous).route == Backbone.history.fragment) {
                manager.History.Previous = _.initial(manager.History.Previous);
            }

            if (manager.History.Previous.length) {
                backUrl = _.last(manager.History.Previous).root;
                
                //чтоб страница не перезагружалась, если путь без решетки
                if (backUrl.indexOf("#") === -1 && window.location.href.indexOf(backUrl) !== -1) {
                    backUrl += "#";
                }

                window.location = backUrl;
            }
        }

        return backUrl;
    },    
    this.excludePage = function(url) {
        if(_.isUndefined(url)) {
            url = Backbone.history.fragment;
        }

        if (manager.History.Previous && manager.History.Previous.length > 1) {

            while (manager.History.Previous.length && _.last(manager.History.Previous).route == url) {
                manager.History.Previous = _.initial(manager.History.Previous);
            }
        }

        localStorage.setItem("appHistory", manager.History.Previous);
    },
    this.goBackWithSamePath = function() {
        var currentPath = compactUrl(window.location.pathname);
        this.excludePage();
        var prev = this.getPreviosPage();
        while (prev && compactUrl(prev.path) != currentPath) {
            this.excludePage(prev.route);
            prev = this.getPreviosPage();
        }
        return this.goToPreviosPage();
    },
    
    this.goBackFrom = function (route) {
        var backUrl = undefined;

        if (manager.History.Previous && manager.History.Previous.length > 1) {
            var lastIndex = _.lastIndexOf(_.map(manager.History.Previous, function (item) {
                return item.route;
            }), route);
            if (lastIndex > -1) {
                manager.History.Previous = _.initial(manager.History.Previous, manager.History.Previous.length - lastIndex);
                
                while (manager.History.Previous.length && _.last(manager.History.Previous).route == route) {
                    manager.History.Previous = _.initial(manager.History.Previous);
                }

                if (manager.History.Previous.length) {
                    backUrl = _.last(manager.History.Previous).root;
                    window.location = backUrl;
                }
            }
        }

        return backUrl;
    },

    this.storeRoute = function () {
        var currentRoute = Backbone.history.fragment,
            currentPath = Backbone.history.location.pathname,
            currentRoot = Backbone.history.location.href,
            lastElem = _.last(manager.History.Current),
            moneyNotDialog = new RegExp(/^(?!moneyDialog).*/);

        if (!moneyNotDialog.test(currentRoute)) {
            return;
        }
        if (!lastElem) {
            lastElem = _.last(manager.History.Previous);
        }
        
        if (lastElem && lastElem.route == currentRoute && lastElem.root == currentRoot) {
            return;
        }

        manager.History.Current.push({
            route: currentRoute,
            root: currentRoot,
            path: currentPath
        });

        if (manager.History.Previous.length > 10) {
            manager.History.Previous = _.rest(manager.History.Previous, manager.History.Previous.length - 10);
        }

        manager.History.Previous.push({
            route: currentRoute,
            root: currentRoot,
            path: currentPath
        });

        localStorage.setItem('appHistory', manager.History.Previous);
    };   
    

    function compactUrl(url) {
        return "/" + _.compact(url.split("/")).join("/");
    }
    
    return this;
};

window.HistoryManager = new _HistoryManager();

$(document).ready(function () {
    HistoryManager.initialize();
});