(function (common) {

    common.Mixin.ListControlMixin = {
        events: {
            "click .addItem": "addItem",
            "click .delete_link": "remove"
        },

        setItemTemplate: function () {
            this.template = this.$(".newItem");
            this.$(".newItem").remove();
        },

        addItem: function () {
            this.collection.add({});
            this.add(this.collection.last());
            _.defer(function() { $('body').trigger('resize'); });
        },

        addRange: function(models) {
            var control = this;
            _.each(models, function(model){
                control.add(model);
            });
        },

        add: function (model) {
            var id = model.cid;
            var newItem = $('<li />').append(this.template.html()).attr("data-id", id);

            newItem.render(model.toJSON());
            this.$('ul').append(newItem);
            newItem.find('input').first().focus();

            if (this.collectionBinder) {
                this.collectionBinder.unbind();
                this.collectionBinder.bind(this.collection, this, 'li');
            }

            common.Mixin.BindViewValidationEvent.bindCollectionValidation(this);
            this.rowAdditionalProcessing && this.rowAdditionalProcessing(newItem, model);
            this.setControls(newItem, model);
        },

        remove: function (event) {
            var row = $(event.target).parent("li");
            this.removeItem(row, { fromUI: true });
        },

        removeAll: function () {
            _.each(this.$("li"), function (row) {
                this.removeItem($(row));
            }, this);
        },

        removeItem: function (row, options) {
            options = options || {};
            var model = this.getRowModel(row);
            row.remove();
            this.collection.remove(model, options);
        },

        removeAllOther: removeAllOther,

        resetItems: function(){
            this.removeAll();
            this.addRange(this.collection.models);
            this.showAddLink();
        },

        showAddLink: function () {
            if (!this.collection.length) {
                this.$(".empty").show();
                this.$("ul+.add").hide();
            } else {
                this.$(".empty").hide();
                this.$("ul+.add").show();
            }
        },

        getRowModel: function (row) {
            var id = row.attr("data-id");
            if (this.collection.getByCid) {
                return this.collection.getByCid(id);
            }
            else {
                return this.collection.get(id);
            }
        }
    };

    /** @access private */
    function removeAllOther(id) {
        var modelsForRemove = this.collection.filter(function (model) {
            return model.get('KontragentId') !== id;
        });
        var modelsArr = _.map(modelsForRemove, function (model) {
            return model.cid;
        });

        _.each(this.$('li'), function (row) {
            var $row = $(row);
            var id = $row.attr('data-id');

            if(!_.contains(modelsArr, id)){
                this.removeItem($row);
            }
        }, this);
    }

})(Common);