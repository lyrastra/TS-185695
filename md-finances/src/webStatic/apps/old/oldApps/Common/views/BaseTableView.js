(function(common) {

    common.Views.BaseTableView = Backbone.View.extend({
       
        templateRow: '',

        events: { },

        initialize: function (options) {
            this.el = options.el;
            this.options = options;

            this.setRowTemplate(options.templateName);
            this.setCollectionTable(options.data);
            this.onInitialize();

            this.render();
        },

        render: function () {
            this.beforeRender();

            if (this.collection.length > 0) {
                this.fillTemplate();
            } else {
                for (var i = 0; i < 3; i++) {
                    this.addItem();
                }
            }

            this.onRender();
        },

        setRowTemplate: function(templateName) {
            var template = this.$(templateName).html();
            this.templateRow = template;
            this.$(templateName).remove();
        },
        
        setCollectionTable: function (data) {
            var tableCollection = Backbone.Collection,
                list = [];

            if (data) {
                if (data.collection) {
                    tableCollection = data.collection;
                } 
                
                list = data.list;
            }
            
            this.collection = new tableCollection(list);
        },

        addItem: function() {
            var row = $("<li class='row'>").append(this.templateRow),
                item = new Backbone.Model();

            if (this.initializeCustomElement) {
                this.initializeCustomElement(row);
            }

            if (this.collection && this.collection.model) {
                item = new this.collection.model();
            }

            this.collection.add(item);
            row.render(item.toJSON(), this.getDirectives().Items);

            this.$("[data-bind='Items']").append(row);

            this.onAddItem();
        },
        
        removeRow: function (event) {
            var icon = $(event.target);
            var row = icon.closest("li");
            var id = row.attr("id");

            this.collection.remove(this.collection.getByCid(id));
            row.remove();

            if (this.collection.length == 0) {
                this.addItem();
            }

            this.onRemoveItem();
        },
        
        fillTemplate: function () {
            if (this.collection) {
                var row = $("<li class='row'>").append(this.templateRow);
                this.$("[data-bind='Items']").append(row);
                this.$el.render({ Items: this.collection.toJSON() }, this.getDirectives());
            }
        },

        beforeRender: function () { },
        
        preAddEvent: function () { },

        onInitialize: function () { },

        onRender: function () { },
        
        onAddItem: function () { },
        
        onRemoveItem: function () { },

        getDirectives: function () { }
    });

})(Common);