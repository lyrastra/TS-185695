(function ($) {
    window.keyCodes = {
        tab: 9,
        enter: 13,
        up: 38,
        down: 40
    };

    window.mdFilter = function (Options) {
        var filter = this;
        
        this.prefix = "mdFilter-id";
        
        this._create = function (options) {
            if (_.isUndefined(options)) {
                throw "Filter options undefined";
            }

            if (_.isUndefined(options.el)) {
                throw "Filter element undefined";
            }

            if(options.el.hasClass("mdFilter-inputElement"))
            {
                return;
            }

            this.input = options.el;
            this.url = options.url;
            this.onSearch = options.onSearch;
            this.ShowExtendedFilter = options.ShowExtendedFilter;

            var defaultSettings = {
                shortFilterList:[]
            };
            this.settings = _.extend(defaultSettings, options.settings);

            this.id = _.uniqueId(this.prefix);
            
            this._createView();
            this.bindEvents();

            this.input.data("mdFilter", this);
        };

        this._createView = function () {
            var container = $("<div />").addClass("mdFilter-control").attr("id", this.id);

            this.input.addClass("mdFilter-inputElement");
            this.input.wrap(container);

            this.view = this.input.parent();

            if (this.ShowExtendedFilter) {
                this.view.append("<span class='mdFilter-extendedFilterButton' />");
            }
            
            this.view.append("<span class='mdFilter-splitter' />");
            
            var searchButton = $("<span class='mdFilter-searchButton' />");
            searchButton.html("&#0035;");
            this.view.append(searchButton);
            
            this._createShortFilterList();
        };

        this._createShortFilterList = function() {
            if (this.settings.shortFilterList.length > 0) {
                var list = $("<ul />");
                list.addClass("mdFilter-shortList");
                
                var controlWidth = this.view.width() || '100%';
                list.width(controlWidth);
                
                _.each(this.settings.shortFilterList, function (element, index) {
                    var row = $("<li />");
                    row.html(element.Name);
                    row.data(element);

                    list.append(row);
                });
                this.view.append(list);
            }
        };

        this.bindEvents = function () {
            this.currentItem = -1;
            
            this.input
                .on('click.mdFilter', this.showShortFilter)
                .on("keyup.mdFilter", function (event) {
                    var code = event.which;
                    if (code == keyCodes.enter || code == keyCodes.tab) {
                        filter.selectShortFilter();
                    }
                    if (code == keyCodes.up) {
                        filter.currentItem--;
                        filter.setFocusedItem();
                    }
                    if (code == keyCodes.down) {
                        filter.currentItem++;
                        filter.setFocusedItem();
                    }
                });

            this.view.find(".mdFilter-searchButton").on("click.mdFilter", this.search);
            
            this.view.find(".mdFilter-shortList li")
                .on("click.mdFilter", this.selectShortFilter)
                .on("mouseenter.mdFilter", this.selectShortListItem)
                .on("mouseleave.mdFilter", this.removeShortListSelection);

            this.view.find(".mdFilter-extendedFilterButton").on("click.mdFilter", this._showExtendedFilter);

            $(document, "input[type=checkbox]").click(function (event) {
                var element = $(event.target);
                if (element.closest("#" + filter.id).length == 0) {
                    filter.closeShortFilter();
                }
            });
        };

        this._hoverClass = "mdFilter-shortList-item-hover";
        this.selectShortListItem = function(event) {
            var item = $(event.target);
            item.addClass(filter._hoverClass);
        };

        this.removeShortListSelection = function (event) {
            var item = $(event.target);
            item.removeClass(filter._hoverClass);
        };
        
        this.setFocusedItem = function () {
            filter.view.find("." + filter._hoverClass).removeClass(filter._hoverClass);

            var maxLength = filter.view.find(".mdFilter-shortList li").length - 1;

            if (filter.currentItem > maxLength) {
                filter.currentItem = maxLength;
            }

            if (filter.currentItem < 0) {
                filter.currentItem = -1;
            }

            if (filter.currentItem >= 0) {
                var items = filter.view.find(".mdFilter-shortList li");
                items.eq(filter.currentItem).addClass(filter._hoverClass);
            }
        };

        this.showShortFilter = function (event) {
            filter.view.find("." + filter._hoverClass).removeClass(filter._hoverClass);
            filter.view.find(".mdFilter-shortList").show();
        };
        
        this.closeShortFilter = function (event) {
            filter.currentItem = -1;
            filter.view.find(".mdFilter-shortList").hide();
        };

        this.selectShortFilter = function (event) {
            var selectedItem = filter.view.find("." + filter._hoverClass);
            
            filter._shortFilter = selectedItem.data();
            filter.search();
        };

        this._showExtendedFilter = function() {
            if ( _.isFunction(filter.ShowExtendedFilter)) {
                filter.ShowExtendedFilter();
            }
        };

        this.search = function() {
            filter.closeShortFilter();
            
            if (_.isFunction(filter.onSearch)) {
                var searchData = {
                    ShortFilter: filter._shortFilter,
                    Query: filter.input.val()
                };
                
                filter.onSearch(searchData);
                
                delete filter._shortFilter;
            }
            mdNew.mrkStatService.sendEvent({
                event: 'nachat_poisk_stranitsa_dengi_uchet_click_icon_money',
                st5: filter.input.val()
            });
        };

        this._create(Options);
        _.extend(this, Backbone.Events);
    };
    

    $.fn.mdFilter = function (action, options) {
        var filter = $(this).data("mdFilter");
        if (!filter) {
            return;
        }

        switch (action) {
            case "search":
                filter.search();
                break;
            case "settings":
                return filter.settings;
        }
    };
        
}(jQuery));