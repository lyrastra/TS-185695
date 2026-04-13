/** Патчер для backbone версии ниже 1
*   Реализует методы версии 1 и старше
*   Реализация методов может отличаться
*/
Backbone.Patcher = (function (Backbone, _, $) {
    "use strict";

    Backbone.Collection.prototype.get = function (obj) {
        if (obj == null) return void 0;
        return this._byId[obj.id ? obj.id : obj] || this._byCid[obj.cid ? obj.cid : obj];
    };

    var eventSplitter = /\s+/;

    var eventsApi = function (obj, action, name, rest) {
        if (!name) return true;
        if (typeof name === 'object') {
            for (var key in name) {
                obj[action].apply(obj, [key, name[key]].concat(rest));
            }
            return false;
        }
        if (eventSplitter.test(name)) {
            var names = name.split(eventSplitter);
            for (var i = 0, l = names.length; i < l; i++) {
                obj[action].apply(obj, [names[i]].concat(rest));
            }
            return false;
        }

        return true;
    };

    var once = Backbone.Events.once || function (name, callback, context) {
        if (!eventsApi(this, 'once', name, [callback, context]) || !callback) return this;
        var self = this;
        var once = _.once(function () {
            self.off(name, once);
            callback.apply(this, arguments);
        });
        once._callback = callback;
        return this.on(name, once, context);
    };

    function stopListening(obj, name, callback) {
        var listeningTo = this._listeningTo;
        if (!listeningTo) return this;
        var remove = !name && !callback;
        if (!callback && typeof name === 'object') callback = this;
        if (obj) (listeningTo = {})[obj._listenId] = obj;
        for (var id in listeningTo) {
            obj = listeningTo[id];
            obj.off(name, callback, this);
            if (remove || _.isEmpty(obj._events)) delete this._listeningTo[id];
        }
        return this;
    }

    Backbone.Model.prototype.once = Backbone.View.prototype.once = once;

    Backbone.Events.stopListening = stopListening;
    Backbone.View.prototype.stopListening = stopListening;

    var listenMethods = { listenTo: 'on', listenToOnce: 'once' };
 
      _.each(listenMethods, function(implementation, method) {
          Backbone.Events[method] = Backbone.View.prototype[method] = function (obj, name, callback) {
              var listeningTo = this._listeningTo || (this._listeningTo = {});
              var id = obj._listenId || (obj._listenId = _.uniqueId('l'));
              listeningTo[id] = obj;
              if (!callback && typeof name === 'object') callback = this;
              obj[implementation](name, callback, this);
              return this;
          };
      });

    Backbone.$ = $;
    
} (Backbone, _, $));