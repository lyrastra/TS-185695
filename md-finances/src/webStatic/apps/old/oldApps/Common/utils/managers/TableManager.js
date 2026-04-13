(function (common) {
    common.Utils.TableManager = function() {
        this.getOrCreateTable = function (options) {
            throw new Error("Method not implemented");
        };

        this.loadTable = function (options) {
            throw new Error("Method not implemented");
        };

        return this;
    };

    common.Utils.TableManager.extend = function(attrs) {
        var manager = function (){};
        _.extend(manager.prototype, this.prototype, attrs);

        return manager;
    };
    
    _.extend(common.Utils.TableManager.prototype, Backbone.Events);
    
})(Common);