/* Биндер для простого связывания коллекции с уже отрисованным view
    элементам представления проставляются id = id + '_' + collection.Item.cid
*/

(function () {

    var cid = 1;

    if (!Backbone) {
        throw 'include backbone.js before mdCollectionBinder';
    }

    if (!Backbone.MdModelBinder) {
        throw 'include Backbone.MdModelBinder.js before Backbone.MdCollectionBinder.js';
    }

    Backbone.MdCollectionBinder = function (options) {
        _.each(_.functions(binder), fn => {
            this[fn] = this[fn].bind(this);
        });

        this.currentOptions = options || {};
        this.Cid = cid++;
    };

    var binder = {
        bind: function (collection, view, lineEl) {
            this.modelBinder = new Backbone.MdModelBinder();

            this.collection = collection;
            this.view = view;
            this.lineEl = lineEl;

            if (!this.collection) {
                throw 'collection must be specified';
            }
            if (!this.view) {
                throw 'view must be specified';
            }
            if (!this.lineEl) {
                throw 'lineEl must be specified';
            }

            this.bindCollectionRowWithModel();
        },

        unbind: function () {
            if (!this.collection) {
                return;
            }

            this.unbindCollectionModel();
        },

        bindCollectionRowWithModel: function () {
            var collectionRow = this.getAllViewRow(),
                currentRow = 0,
                self = this;
            this.collection.each(function (item) {
                self.setIdCollectionRow(collectionRow[currentRow], item.cid);
                self.setIdCollectionItem(collectionRow[currentRow], item.cid);

                self.bindCollectionToView(item);
                self.bindViewToCollection(collectionRow[currentRow]);

                currentRow++;
            });
        },

        unbindCollectionModel: function () {
            var collectionRow = this.getAllViewRow(),
                currentRow = 0,
                self = this;

            this.collection.each(function (item) {
                self.unbindCollectionToView(item);
                self.unbindViewToCollection(collectionRow[currentRow]);
                self.removeIdCollectionItem(collectionRow[currentRow]);
                currentRow++;
            });

            this.collection = undefined;
        },

        setIdCollectionRow: function (row, cid) {
            $(row).attr('id', cid);
        },

        setIdCollectionItem: function (row, cid) {
            var self = this,
                el = $(row).find('[data-bind]'),
                oldId,
                newIdPostfix,
                oldDataBindAttr,
                oldNameAttr;

            el.each(function () {
                var item = $(this);
                oldId = $.trim(item.attr('id')).length == 0 ? item.attr('data-bind') : self.getDefaultIfUndefinedAttr(item.attr('id'), 'id');
                oldDataBindAttr = self.getDefaultIfUndefinedAttr(item.attr('data-bind'), 'data-bind');
                oldNameAttr = self.getDefaultIfUndefinedAttr(item.attr('name'), 'name');

                newIdPostfix = '_' + cid;
                item.attr('id', oldId + newIdPostfix);
                item.attr('data-bind', oldDataBindAttr + newIdPostfix);
                item.attr('name', oldNameAttr + newIdPostfix);
            });
        },

        removeIdCollectionItem: function(row) {
            var el = $(row).find('[data-bind]');

            el.each(function() {
                var item = $(this),
                    id = item.attr('id'),
                    dataBind = item.attr('data-bind'),
                    name = item.attr('name');

                if (id) {
                    item.attr('id', removeBindingPostfix(id));
                }

                if (dataBind) {
                    item.attr('data-bind', removeBindingPostfix(dataBind));
                }

                if (name) {
                    item.attr('name', removeBindingPostfix(name));
                }
            });
        },

        getAllViewRow: function () {
            return this.view.$el.find(this.lineEl);
        },

        bindCollectionToView: function (model) {
            model.on('change', this.collectionChange, this);
        },

        bindViewToCollection: function (view) {
            $(view).on('change.mdCollectionBinder', this.viewChange);
            $(view).delegate('[contenteditable]', 'blur', this.viewChange);
        },

        unbindCollectionToView: function (model) {
            model.off('change', this.collectionChange);
        },

        unbindViewToCollection: function (view) {
            $(view).off('change.mdCollectionBinder');
            $(view).undelegate('[contenteditable]', 'blur');
        },

        collectionChange: function (e) {
            for (var changeAttr in e.changedAttributes()) {
                this.collectionToViewValue(changeAttr, e.cid);
            }
        },

        collectionToViewValue: function (attr, cid) {
            if (attr) {
                var value,
                    elBindings = this.getElBindings(attr, cid);

                if (this.collection.getByCid) {
                    var model = this.collection.getByCid(cid);
                    value =  model && model.get(attr);
                } else {
                    value = this.collection.get(cid).get(attr);
                }


                for (var i = 0; i < elBindings.length; i++) {
                    if (!_.isObject(value)) {
                        this.elValue($(elBindings[i]), value);
                    }
                }
            }
        },

        viewChange: function (event) {
            var el = $($(event.target)[0]),
                attr = el.attr('data-bind'),
                attrName = attr,
                attrCid = el.closest(this.lineEl).attr('id');

            var path = attr.split('_');
            if (path.length > 1) {
                attrName = _.initial(path).join('_');
            }

            if (attrName != null) {
                this.viewToCollectionValue(el, attrName, attrCid);
            }
        },

        viewToCollectionValue: function (el, attrName, attrCid) {
            var value = this.getViewElValue(el);
            this.elViewValue(attrName, attrCid, value);
        },

        elValue: function (el, value) {
            if (el.attr('type')) {
                var oldValue;
                switch (el.attr('type')) {
                    case 'radio':
                        oldValue = el.attr('checked');
                        if (el.val() === value) {
                            el.attr('checked', 'checked');
                        }
                        break;
                    case 'checkbox':
                        oldValue = el.attr('checked');
                        if (value) {
                            el.attr('checked', 'checked');
                        } else {
                            el.removeAttr('checked');
                        }
                        break;
                    default:
                        oldValue = el.val();
                        el.val(value);
                        break;
                }

                if (oldValue !== value) {
                    el.change();
                }

            } else if (el.is('input') || el.is('select') || el.is('textarea')) {
                el.val(value);
            } else {
                el.text(value);
            }
        },

        elViewValue: function(attrName, attrCid, value) {
            var model = this.collection.getByCid ? this.collection.getByCid(attrCid) : this.collection.get(attrCid);

            if (!updateFieldAtDeepModel(model, attrName, value)) {
                model.set(attrName, value);
            }
        },

        getViewElValue: function (el) {
            switch (el.attr('type')) {
                case 'checkbox':
                    return el.prop('checked') ? true : false;
                default:
                    if (el.attr('contenteditable') !== undefined) {
                        return el.html();
                    } else {
                        return el.val();
                    }
            }
        },

        getElBindings: function (attr, cid) {
            return $(this.view.$el.find(this.lineEl + '#' + cid)).find('[data-bind=' + attr + '_' + cid + ']');
        },

        getDefaultIfUndefinedAttr: function(attr, type) {
            if (attr == undefined) {
                switch(type) {
                    case 'id':
                        return 'id';
                    case 'name':
                        return 'name';
                    case 'data-bind':
                        return 'data-bind';
                }
            }

            return attr;
        }
    };

    _.extend(Backbone.MdCollectionBinder.prototype, binder);

    function removeBindingPostfix(attr) {
        var path = attr.split('_');
        if (path.length <= 1) {
            return attr;
        }

        return _.initial(path).join('_');
    }

    function updateFieldAtDeepModel(model, attrName, value) {
        var path = attrName.split('_');
        if (path.length > 1) {
            var prop = model.get(path[0]);
            for (var i = 1; i < path.length - 1; i++) {
                var attr = path[i];
                prop = prop[attr];
            }

            var index = parseInt(path[path.length - 1]);

            if (_.isArray(prop)) {
                while (prop.length < index) {
                    prop.push(null);
                }
            }

            prop[index] = value;
            return true;
        }
        return false;
    }
})();