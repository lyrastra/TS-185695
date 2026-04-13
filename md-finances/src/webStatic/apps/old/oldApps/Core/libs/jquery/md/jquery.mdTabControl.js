(function ($) {

    var className = {
        tab: 'mdTab',
        buttonLine: 'mdtButtonLine',
        button: 'mdtButton',
        content: 'mdtContent',
        selected: 'mdtSelectedTab',
        lBorder: 'mdtLBorder',
        rBorder: 'mdtRBorder',
        hideTab: 'mdtHideTab',
        
        getClassSelector: function(selector) {
            return '.' + selector;
        }
    };

    $.fn.mdTab = function(options) {
        if (!this.length) {
            throw 'parent selector not set';
        }

        var settings = $.extend({
            defaultTabName: 'Tab',
            startIndex: 1,
            selectedTab: 1,
            searchArea: 'body',
            tabs: []
        }, options);

        return this.each(function() {
            var $el = $(this),
                $tab = htmlCreator.createTab(),
                $button = htmlCreator.createButton(),
                $content = htmlCreator.createContent(),
                $buttons, $buttonLine, $contents;

            htmlCreator.createTabControl(settings, $button, $content);

            $buttons = $button.find(className.getClassSelector(className.button));
            $buttonLine = $button.find(className.getClassSelector(className.button));
            $contents = $content.find('[data-tab]');

            setSelectedTab($buttonLine, $contents, settings.selectedTab);
            bindEvent($buttons);
            compileTabControl($el, $tab, $button, $content);
        });
    };
    
    function getCurrentTabObject(settings, index) {
        return settings.tabs.length ? settings.tabs[index] : { tab: settings.defaultTabName + (index + 1) };
    }

    function compileTabControl($el, $tab, $button, $content) {
        $tab.append($button);
        $tab.append($content);
        $el.append($tab);
    }

    function bindEvent($tabs) {
        $tabs.on('click', changeTab);
    }

    function setSelectedTab($tabs, $contents, selectedIndex) {
        selectedIndex = setSelectedIndex($tabs.length, selectedIndex);
        htmlCreator.addSelectedClass($($tabs[selectedIndex]));
        htmlCreator.removeHideClassContent($($contents[selectedIndex]));
    }
    
    function setSelectedIndex(tabsCount, index) {
        if (index > 0) {
            index -= 1;
        }

        if (tabsCount < index) {
            index = tabsCount - 1;
        }

        return index;
    }
    
    function changeTab(e) {
        var $el = $(e.target || e.toElement || e.srcElement).closest(className.getClassSelector(className.button)),
            tab = $el.attr('data-tab'), tabs, content, parent;
        
        if ($el.hasClass(className.selected)) {
            return;
        }

        parent = $el.closest(className.getClassSelector(className.tab));
        tabs = parent.find(className.getClassSelector(className.button));
        content = parent.find(className.getClassSelector(className.content) + ' [data-tab="' + tab + '"]');
        
        if (content.length) {
            changeTabSelected($el, tabs, content, parent);
        }
    }
    
    function changeTabSelected($el, tabs, content, parent) {
        htmlCreator.unselectedAllTab(tabs);
        htmlCreator.addSelectedClass($el);
        htmlCreator.addHideClassAllContentTab(parent);
        htmlCreator.removeHideClassContent(content);
    }
    
    function renderEstablishedContent($content, options, searchArea) {
        var content = options.content;
        createContent($content, content, searchArea);
    }
    
    function createContent($content, content, searchArea) {
        var contentType = typeof content;

        if (contentType === 'string') {
            createContentIfSetSelector($content, content, searchArea);
        } else if (contentType === 'object') {
            $content.append(content);
        }
    }
    
    function createContentIfSetSelector($content, content, searchArea) {
        var area = getSearchArea(searchArea);

        if (area) {
            var $searchContent = area.find(content);

            if ($searchContent.length) {
                $content.append($searchContent);
                removeHtmlContentInArea(area, content);
            }
        }
    }
    
    function removeHtmlContentInArea(area, content) {
        area.find(content).remove();
    }

    function getSearchArea(searchArea) {
        var area = $('body');
        
        if (searchArea) {
            switch (typeof searchArea) {
                case 'string':
                    area = $(searchArea);
                    break;
                case 'object':
                    area = searchArea;
                    break;
            }
        } 

        return area;
    }

    var htmlCreator = {
        addSelectedClass: function ($tab) {
            this.addClassElement($tab, className.selected);
        },
        unselectedAllTab: function($tabs) {
            $tabs.removeClass(className.selected);
        },
        addHideClassAllContentTab: function ($mdTab) {
            var $element = $mdTab.find(className.getClassSelector(className.content) + ' [data-tab]');
            this.addClassElement($element, className.hideTab);
        },
        removeHideClassContent: function($content) {
            $content.removeClass(className.hideTab);
        },
        addLBorderElement: function($currentButton) {
            this.addClassElement($currentButton, className.lBorder);
        },
        addRBorderElement: function ($currentButton) {
            this.addClassElement($currentButton, className.rBorder);
        },
        addClassElement: function ($element, stringClassName) {
            if ($element && $element.addClass) {
                $element.addClass(stringClassName);
            }
        },
        createTab: function() {
            return this.createDiv(className.tab);
        },
        createButton: function() {
            return this.createDiv(className.buttonLine);
        },
        createContent: function() {
            return this.createDiv(className.content);
        },
        createDiv: function(classNameForDiv) {
            return $('<div class="' + classNameForDiv + '">');
        },
        createDivWithDataTab: function (classNameForDiv, dataTab) {
            var div = this.createDiv(classNameForDiv);
            div.attr('data-tab', dataTab);
            return div;
        },
        createSpanHtmlText: function(textForSpan) {
            return '<span>' + textForSpan + '</span>';
        },
        addBorderElement: function ($currentButton, index, count) {
            if (index == 0) {
                htmlCreator.addLBorderElement($currentButton);
            } else if (index + 1 == count) {
                htmlCreator.addRBorderElement($currentButton);
            }
        },
        createTabControl: function(settings, $button, $content) {
            for (var index = 0, count = settings.tabs.length ? settings.tabs.length : 3; index < count; index++) {
                var currentTab = getCurrentTabObject(settings, index),
                    dataTab = settings.defaultTabName + (index + 1),
                    $currentButton = this.createDivWithDataTab(className.button, dataTab),
                    $currentContent = this.createDivWithDataTab(className.hideTab, dataTab);

                this.addBorderElement($currentButton, index, count);

                if (currentTab.content) {
                    renderEstablishedContent($currentContent, currentTab, settings.searchArea);
                }

                $currentButton.append(this.createSpanHtmlText(currentTab.tab));

                $button.append($currentButton);
                $content.append($currentContent);
            }
        }
    };

})(jQuery);
