(function($) {
    'use strict';

    var selectors = {
        'addLink': 'mdSaleAutocomplete-addItem',
    };

    window.keyCodes = {
        tab: 9,
        enter: 13,
        up: 38,
        down: 40,
    };

    window.mdSaleAutocomplete = window.mdAutocomplete = function(options) {
        var autocomplete = this;
        var hoverClass = 'mdSaleAutocomplete-hover';
        this.prefix = 'mdSaleAutocomplete-id';

        this._create = function(options) {
            if (_.isUndefined(options)) {
                throw 'Autocomplete options undefined';
            }

            if (_.isUndefined(options.el)) {
                throw 'Autocomplete element undefined';
            }

            if (_.isUndefined(options.url) && _.isUndefined(options.source)) {
                throw 'Autocomplete url undefined';
            }

            if (!_.isUndefined(options.data)) {
                this.data = function() {
                    return _.isFunction(options.data) ? options.data() : options.data;
                };
            } else {
                this.data = function() {
                    return {};
                };
            }

            this.options = options;
            this.url = options.url;
            this.source = options.source;

            var defaultSettings = {
                addLink: false,
                onlyFromList: true,
                createEventIfEmptyList: true,
                addLinkName: 'клиент',
                count: 5,
                dataList: null,
                selectFirst: false,
                clean: false,
                keepValueOnBlur: false
            };

            this.settings = _.extend(defaultSettings, options.settings);

            this.id = _.uniqueId(this.prefix);
            setActionMethods.call(this, options);
            this.traditional = options.traditional || false;
            this._createView(options);
            this.bindEvents();
            setCloseIconIfNeeded();
        };

        this._createView = function(options) {
            var autocompleteElem = $('<div>');
            autocompleteElem.attr('id', this.id);
            autocompleteElem.addClass('mdSaleAutocomplete');
            if (options.className) {
                autocompleteElem.addClass(options.className);
            }

            this.view = autocompleteElem;

            var itemsElem = $('<ul />');

            itemsElem.addClass('mdSaleAutocomplete-items');
            this.view.append(itemsElem);

            this.el = $(options.el);
            this.el.data('mdSaleAutocomplete', this);
            $('body').append(this.view);
        };

        this._setDefaultCurrentItem = function() {
            this.currentItem = -1;

            if (this.settings.selectFirst === true) {
                this.currentItem = 0;
            }
        };

        this.bindEvents = function() {
            this._setDefaultCurrentItem();

            this.el.on('click.mdSaleAutocomplete', function() {
                autocomplete.search({
                    query: $(this).val(),
                });
            }).on('keyup.mdSaleAutocomplete', function(event) {
                var code = event.which;

                if (((code == keyCodes.up || code == keyCodes.down || code == keyCodes.enter) && autocomplete.view.is(':visible'))) {
                    return;
                }

                clearTimeout($.data(this, 'timer'));
                var wait = setTimeout(autocomplete.search, 200);
                $(this).data('timer', wait);
            }).on('keydown.mdSaleAutocomplete', function(event) {
                var code = event.which;
                if (code == keyCodes.enter) {
                    autocomplete._onBlur(event);
                    autocomplete.close();
                }
                if (code == keyCodes.up) {
                    autocomplete.currentItem--;
                    autocomplete.setFocusedItem();
                }
                if (code == keyCodes.down) {
                    if (autocomplete.view.is(':visible')) {
                        autocomplete.currentItem++;
                        autocomplete.setFocusedItem();
                    }
                }
                _.defer(setCloseIconIfNeeded);
            }).on('blur.mdSaleAutocomplete', function(event, data) {
                if (data && data.mdSaleAutocomplete == true) {
                    updatePreviusValue();
                    return;
                }

                var element = $(document.activeElement);
                if (element.closest('.mdSaleAutocomplete').length !== 0) {
                    updatePreviusValue();
                    return;
                }

                autocomplete.removeCurrentRequest();
                autocomplete._onBlur(event);
                autocomplete.close();
                setCloseIconIfNeeded();
                updatePreviusValue();
            }).on('change', function() {
                setCloseIconIfNeeded();
            });

            var updatePreviusValue = function() {
                var data = $(autocomplete.el).data('mdSaleAutocomplete') || {};
                data.previous = $(autocomplete.el).val();
                $(autocomplete.el).data('mdSaleAutocomplete', data);
            };

            var items = this.view.find('.mdSaleAutocomplete-items');
            items.on('mouseenter', 'li', function() {
                autocomplete.currentItem = items.find('li').index($(this));
                autocomplete.setFocusedItem();
            }).on('mouseleave', function() {
                autocomplete.currentItem = -1;
                autocomplete.setFocusedItem();
            }).on('click', 'a', function() {
                var index = items.find('a').index($(this));
                var selectedObj = autocomplete.collection[index];
                autocomplete._onSelect(selectedObj);
            });

            if (_.result(this.settings, 'addLink')) {
                this.view.on('click', '.mdSaleAutocomplete-addItem', function(event) {
                    autocomplete._onCreate();
                }).on('mouseenter', '.mdSaleAutocomplete-addItem', function() {
                    autocomplete.currentItem = 'new';
                    autocomplete.setFocusedItem();
                }).on('mouseleave', '.mdSaleAutocomplete-addItem', function() {
                    autocomplete.currentItem = -1;
                    autocomplete.setFocusedItem();
                });
            }

            var autocompliteWindow = $('.mdSaleAutocomplete');

            if (!('onhashchange' in window)) {
                Backbone.history.unbind('route');
                Backbone.history.bind('route', function() {
                    autocompliteWindow.hide();
                });
            } else {
                window.onhashchange = function() {
                    autocompliteWindow.hide();
                };
            }

        };

        this._canShowAutocompleteView = function() {
            var disabled = $(autocomplete.el).attr('disabled');
            var readonly = $(autocomplete.el).attr('readonly');
            if ((disabled && disabled !== false) || (readonly && readonly !== false)) {
                return false;
            }
            return true;
        };

        this.setFocusedItem = function() {
            var inputLength = this.el.val().length;

            this.view.find('.' + hoverClass).removeClass(hoverClass);
            if (this.currentItem > this.collection.length) {
                this.currentItem = this.collection.length;
            }

            if (this.currentItem < 0) {
                this.currentItem = -1;

                var hasItems = this.view.find('.mdSaleAutocomplete-items').find('li').length;
                var isAddLink = _.result(this.settings, 'addLink');
                var createEventIfEmptyList = this.settings.createEventIfEmptyList;

                if (!hasItems && isAddLink && createEventIfEmptyList) {
                    this.currentItem = this.collection.length;
                } else {
                    return;
                }
            }

            var items = this.view.find('.mdSaleAutocomplete-items').find('li');
            items.eq(this.currentItem).addClass(hoverClass);

            if (this.currentItem == 'new' || (!this.collection.length && inputLength)) {
                this.view.find('.mdSaleAutocomplete-addItem').addClass(hoverClass);
            }
        };

        this.unBindEvents = function() {
            this.el.off('.mdSaleAutocomplete');
        };

        this.destroy = function() {
            this.unBindEvents();
            this.view.remove();
        };

        this._onSelect = function(selectedItem) {
            autocomplete.trigger('select');
            this.el.val(selectedItem.value).trigger('change', { mdSaleAutocomplete: true });
            if (_.isFunction(this.onSelect)) {
                this.onSelect(selectedItem);
            }

            autocomplete.close();
            setCloseIconIfNeeded();
        };

        function isValueChanged() {
            if (autocomplete.previous != $(autocomplete.el).val()) {
                return true;
            }

            return false;
        }

        var setCloseIconIfNeeded = function() {
            if (!autocomplete.settings.clean) {
                return;
            }

            var val = $(autocomplete.el).val();
            if ($.trim(val).length == 0) {
                removeCloseIcon();
                return;
            }

            createCloseIcon();
        };

        var removeCloseIcon = function() {
            var input = $(autocomplete.el);

            input.parent().find('.mdAutocomplete-closeIcon').remove();
        };

        var createCloseIcon = function() {
            var input = $(autocomplete.el);
            if (input.next().hasClass('mdAutocomplete-closeIcon')) {
                return;
            }

            var icon = $('<span class="mdAutocomplete-closeIcon"></span>');
            icon.on('click', function() {
                $(autocomplete.el).val('');
                if (_.isFunction(autocomplete.settings.clean)) {
                    autocomplete.settings.clean();
                } else {
                    $(autocomplete.el).trigger('change');
                }
                setCloseIconIfNeeded();
            });
            input.after(icon);
        };

        this._onBlur = function(e) {
            var selectedLink = this.view.find('.' + hoverClass).find('a');
            var query = $(e.target).val();

            if (selectedLink.length) {
                selectedLink.trigger('click');
                return;
            }

            if (this.settings.onlyFromList) {
                if (!_.isUndefined(autocomplete.lastSearch)) {
                    if (this.collection.length && _.first(this.collection).value == this.el.val()) {
                        this._onSelect(_.first(this.collection));
                        return;
                    }
                }

                if (isValueChanged() && !this.settings.keepValueOnBlur && !_isPreloadedValue(query)) {
                    var old = this.el.val();
                    this.el.val('');
                    if (old != this.el.val()) {
                        this.el.change();
                    }
                }
            }

            if (_.isFunction(this.onBlur)) {
                this.onBlur(this.el);
            }
        };

        this._onCreate = function() {
            autocomplete.trigger('create');

            if (_.isFunction(this.onCreate)) {
                this.onCreate();
            }

            autocomplete.close();
        };

        this.search = function(options) {
            var maxLengthQuery = 100;

            if (!autocomplete._canShowAutocompleteView()) {
                return;
            }

            var settings = this.settings || {};
            var additionalQueryValues = settings.additionalValue || {};
            options = options || {};
            options.data = options.data || {};
            _.extend(options.data, autocomplete.data());

            var query = options.data.query || options.query || autocomplete.el.val();
            options.data.query = (query || '').substr(0, maxLengthQuery);
            options.data.count = options.count || (autocomplete.settings.count || 5);

            if (additionalQueryValues.Inn) {
                options.data.inn = additionalQueryValues.Inn;
            }

            if (additionalQueryValues.Description) {
                options.data.description = additionalQueryValues.Description;
            }

            if (autocomplete.source) {
                autocomplete.lastSearch = options.data.query;
                autocomplete.collection = autocomplete.parse(autocomplete.source(options.data));
                autocomplete._setDefaultCurrentItem();
                autocomplete.showItems();
            } else {
                autocomplete.el.addClass('mdSaleAutocomplete-loading');
                var requestOptions = {
                    type: autocomplete.options.type || 'GET',
                    url: typeof autocomplete.url === 'function' ? autocomplete.url() : autocomplete.url,
                    data: options.data,
                    contentType: 'application/json; charset=utf-8;',
                    dataType: 'json',
                    traditional: autocomplete.traditional,
                    success: function(data) {
                        if (data) {
                            autocomplete.lastSearch = options.data.query;
                            autocomplete.collection = autocomplete.parse(data.List || data);
                            autocomplete._setDefaultCurrentItem();
                            autocomplete.showItems();
                        }
                    },
                    error: function() {
                    },
                    complete: function() {
                        delete autocomplete._currentRequest;
                        autocomplete.el.removeClass('mdSaleAutocomplete-loading');
                    },
                };

                if (requestOptions.type.toLowerCase() === 'post') {
                    requestOptions.data = JSON.stringify(requestOptions.data);
                }

                autocomplete._currentRequest = $.ajax(requestOptions);
            }
        };

        this.removeCurrentRequest = function() {
            if (autocomplete._currentRequest) {
                autocomplete.el.removeClass('mdSaleAutocomplete-loading');
                autocomplete._currentRequest.abort();
            }
        };

        this.parse = function(data) {
            return $.map(data, function(item) {
                return { label: item.Name, value: item.Name, object: item };
            });
        };

        this._renderItem = function(item) {
            var link = $('<a></a>');
            link.text(item.label);

            return link;
        };

        this.showItems = function() {
            if (!autocomplete._canShowAutocompleteView()) {
                return;
            }
            $('.mdSaleAutocomplete').hide();

            if (this.collection.length == 0 && !_.result(this.settings, 'addLink')) {
                this.view.hide();
                return;
            }

            if (!this.el || !this.el.is(':visible') || !this.el.is(':focus')) {
                this.view.hide();
                return;
            }

            var items = this.view.find('.mdSaleAutocomplete-items');
            items.empty();

            _.each(this.collection, function(val, index) {
                var link = autocomplete._renderItem(val);
                var item = $('<li></li>');
                item.append(link);
                items.append(item);
            });

            createAddLink.call(this);

            this.hideBehindTheEdge();
            this.view.show();
            this.setFocusedItem();
            this.setPosition();

            if ($.fn.truncateString) {
                items.find('a').truncateString();
            }

            var mdAutocomplete = this;
            $(window).on('scroll.mdAutocomplete', function() {
                mdAutocomplete.close();
            });
        };

        this.hideBehindTheEdge = function() {
            this.view.css({
                top: -1000,
                left: -1000,
            });
        },

        this.setPosition = function() {
            try {
                var zIndex = this.getZIndex();
                var items = this.view.find('.mdSaleAutocomplete-items');
                if (items.length) {
                    this.view.css({ width: 'auto' });
                    items.css({ width: 'auto' });

                    var width = 0;

                    var rows = items.find('li');
                    if (rows.length) {
                        var maxWidthItem = _.max(items.find('li'), function(item) {
                            return $(item).outerWidth();
                        });
                        width = $(maxWidthItem).outerWidth();
                    }

                    var borders = 2;
                    if ((width + borders) < this.el.outerWidth()) {
                        this.view.width(this.el.outerWidth() - borders);
                    }

                    var itemsWidth = this.view.width();
                    items.css({
                        width: itemsWidth,
                    });
                }

                var top = this.el.offset().top + this.el.innerHeight();
                var left = this.el.offset().left;
                this.view.css({
                    top: top,
                    left: left,
                    zIndex: zIndex,
                });
            }
            catch(e) {
                console.error(e.message);
            }
        };

        this.getZIndex = function() {
            return getZIndex(this.view);
        };

        function getZIndex(element) {
            var topParent = $(element).parents('body>*');
            if (topParent.length == 0) {
                topParent = $(element).parents().last();
            }

            var parentZIndex = Converter.toInteger($(topParent).css('z-index'));
            var elements = topParent.find(':not(select)');

            var max = _.max(elements, function(el) {
                var zIndex = Converter.toInteger($(el).css('z-index'));

                return zIndex;
            });

            max = Converter.toInteger($(max).css('z-index'));
            var highestIndex = Math.max(max, parentZIndex) + 1;

            return highestIndex;
        }

        this.close = function() {
            this.view.hide();
            this.view.find('.' + hoverClass).removeClass(hoverClass);
            this.currentItem = -1;
            $(window).off('scroll.mdAutocomplete');
        };

        this._create(options);
        _.extend(this, Backbone.Events);

        function _isPreloadedValue(query) {
            var result;
            var preloadedCheck = options.checkPreloadedValue;

            if (preloadedCheck && typeof preloadedCheck === 'function') {
                result = preloadedCheck(query);
            } else {
                result = false;
            }

            return result;
        }
    };

    function createAddLink() {
        var addItemLink = this.view.find('.' + selectors.addLink);
        var addLinkSetting = _.result(this.settings, 'addLink');

        if (addLinkSetting) {
            if (addItemLink.length) {
                return;
            }

            var addItemLinkElem = $('<div>', {
                html: '<a><span class=\'plus-icon\'>+</span> ' + this.settings.addLinkName + '</a>',
                class: selectors.addLink,
            });

            this.view.append(addItemLinkElem);
        } else {
            addItemLink.remove();
        }
    }

    /** @access private */
    function setActionMethods(options) {
        if (_.isFunction(options.onSelect)) {
            this.onSelect = options.onSelect;
        }
        if (_.isFunction(options.onCreate)) {
            this.onCreate = options.onCreate;
        }
        if (_.isFunction(options.onBlur)) {
            this.onBlur = options.onBlur;
        }
    }



}(jQuery));
