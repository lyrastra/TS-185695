import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

/* eslint-disable */
(function (taxPosting, common) {
    taxPosting.Views.TaxPostingTableView = common.Views.BaseView.extend({
        templateUrl: "ClientSideApps/TaxPosting/templates/",

        template: "TaxPostingTable",

        events: {
            "click .add>span.link": "addItem",
            "change input,textarea": "updateModel",
            "click li:not(.open)": "editRow",
            "click .ttDelete .icon": "removeRow",
            "click .ttType .typeSwitcher .icon": "changeType",
            "keydown textarea": "resizeTextarea",
            "focusin textarea": "resizeTextarea",
            "blur textarea": "resizeTextarea"
        },

        resizeTextarea: function (event) {
            _.defer(function() {
                var control = event.target;
                $(control).attr("rows", 1);
                var rowCount = Math.floor((control.scrollHeight) / 20);

                $(control).attr("rows", rowCount);
            });
        },

        initialize: function (options) {
            options = options || {};
            this.defaultDate = dateHelper(options.Date || new Date()).format('DD.MM.YYYY');

            this.collection = new Backbone.Collection(options.data);
            this.render();
        },

        getDataForRender: function () {
            return {
                Items: this.collection.toJSON()
            };
        },

        getDirectives: function () {
            return {
                Items: {
                    Date: _.extend({}, common.Utils.DirectiveHelper.inputDateDirective("Date"), common.Utils.DirectiveHelper.textDateDirective("Date")),
                    Sum: common.Utils.DirectiveHelper.textMoneyDirective("Sum")
                }
            };
        },

        beforeRender: function() {
            this.emptyRowTemplate = this.$("[data-bind='Items']").html();

            this.$(".typeSwitcher .icon.incoming").attr("data-val", taxPostingsDirection.Incoming);
            this.$(".typeSwitcher .icon.outgoing").attr("data-val", taxPostingsDirection.Outgoing);
        },

        onRender: function () {
            this.turnIntoText();

            _.each(this.$(".mdTableBody li"), function (row, index) {
                var item = this.collection.at(index);
                $(row).attr("id", item.cid);

                this.setType($(row), item.get("Direction"));
            }, this);

            $.validator.unobtrusive.parse(this.$el);

            if (this.collection.length == 0) {
                this.addItem();
            }
        },

        turnIntoText: function(el) {
            var rows;
            if (el) {
                rows = el.closest("li");
            } else {
                rows = this.$(".mdTableBody li");
            }
            rows.each(function(index, row) {
                row = $(row);
                row.find(".read").show();
                row.find(".update").hide();
                row.removeClass("open");
            });
        },

        turnIntoInput: function (el) {
            el = el || this.$el;

            el.find(".read").hide();
            el.find(".update").show();
            el.addClass("open");
        },

        updateModel: function(event) {
            var input = $(event.target);
            var row = input.closest("li");
            var id = row.attr("id");

            var model = this.collection.getByCid(id);
            model.set(input.attr("name"), input.val());
        },

        editRow: function(event) {
            var row = $(event.target).closest("li");
            var id = row.attr("id");

            this.turnIntoInput(row);
            this.listen(id);
        },

        addItem: function () {
            var row = $(this.emptyRowTemplate);
            row.find("[name=Date]").mdDatepicker();

            var item = new Backbone.Model({
                Date: this.defaultDate,
                Direction: taxPostingsDirection.Incoming
            });
            this.collection.add(item);
            row.render(item.toJSON(), this.getDirectives().Items);

            row.attr("id", item.cid);
            this.turnIntoInput(row);
            this.listen(item.cid);

            row.find(".typeSwitcher .icon.incoming").attr("data-val", taxPostingsDirection.Incoming);
            row.find(".typeSwitcher .icon.outgoing").attr("data-val", taxPostingsDirection.Outgoing);
            this.setType(row, taxPostingsDirection.Incoming);

            this.$("[data-bind='Items']").append(row);
            $.validator.unobtrusive.parseDynamicContent(row);
        },

        listen: function (id) {
            var view = this;
            _.defer(function() {
                $("body").on("click." + id, function (event) {
                    event.stopPropagation();

                    if ($(event.target).closest("#" + id).length == 0) {
                        view.onBlur.call(view, id);
                    }
                });
            });
        },

        isValid: function (row) {
            if (!this.isEmpty(row)) {
                var validator = row.closest("form").validate();
                return validator.element(row.find("[name=Date]")) &&
                    validator.element(row.find("[name=Sum]")) &&
                    validator.element(row.find("[name=Description]"));
            }
            return false;
        },

        isEmpty: function(row) {
            return $.trim(row.find("[name=Sum]").val()) == 0 && $.trim(row.find("[name=Description]").val()) == 0;
        },

        onBlur: function (id) {
            var row = this.$("#" + id);

            if (this.isValid(row)) {
                $("body").off("click." + id);

                this.turnIntoText(row);
                row.render(this.collection.getByCid(id).toJSON(), this.getDirectives().Items);
            }
        },

        removeRow: function(event) {
            var icon = $(event.target);
            var row = icon.closest("li");
            var id = row.attr("id");

            this.collection.remove(this.collection.getByCid(id));
            row.remove();

            if (this.collection.length == 0) {
                this.addItem();
            }
        },

        changeType: function (event) {
            var icon = $(event.target);
            icon.closest(".typeSwitcher").find(".icon").removeClass("selected");

            var row = icon.closest("li");
            var id = row.attr("id");
            this.collection.getByCid(id).set("Direction", icon.attr("data-val"));

            this.setType(row, icon.attr("data-val"));
        },

        setType: function(row, direction) {
            var selected = row.find(".typeSwitcher").find(String.format(".icon[data-val='{0}']", direction));
            row.find(".ttType .read").html(selected.clone());

            selected.addClass("selected");
        }
    });

    var taxPostingsDirection = {
        Incoming: 1,
        Outgoing: 2
    };

})(TaxPosting, Common);
