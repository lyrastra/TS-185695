(function (main) {

    var openContextMenuFlag = false;

    var parentLine;
    var menuItem = '.menuItem';
    var contextMenuDiv = '.stockContextMenu';
    var inventoryId = '#Inventory';
    var importExcelId = '#ImportExcel';
    var addNomenclature = '#AddNomenclatureGroup';
    var renameId = '#Rename';
    var deleteId = '#Delete';
    var uniteId = '#Unite';
    var addWholesaleStockId = '#AddWholesaleStock';
    var addRetailStockId = '#AddRetailStock';

    main.Helpers.ContextMenu = function (options) {

        var settings = {
            $el: '',
            parentEl: '',
            html: '',
            inventory: function () { },
            unite: function () { },
            importExcel: function () { },
            rename: function () { },
            deleteItem: function () { },
            addWholesaleStock: function () { },
            addRetailStock: function () { }
        };

        $.extend(settings, options);

        parentLine = settings.parentLine || 'li';

        function bindEvents() {
            $(contextMenuDiv + ' ' + inventoryId).on('click', settings.inventory);
            $(contextMenuDiv + ' ' + importExcelId).on('click', settings.importExcel);
            $(contextMenuDiv + ' ' + addNomenclature).on('click', settings.add);
            $(contextMenuDiv + ' ' + renameId).on('click', settings.rename);
            $(contextMenuDiv + ' ' + deleteId).on('click', settings.deleteItem);
            $(contextMenuDiv + ' ' + uniteId).on('click', settings.unite);
            $(contextMenuDiv + ' ' + addWholesaleStockId).on('click', settings.addWholesaleStock);
            $(contextMenuDiv + ' ' + addRetailStockId).on('click', settings.addRetailStock);

            $(document).on('mousedown.StoreContextMenu', closeContextMenuEvent);
            $(document).on('click.StoreContextMenu', closeContextMenuAfterSelectItemEvent);
        }
        
        function unbindEvents() {
            $(document).off('mousedown.StoreContextMenu');
            $(document).off('click.StoreContextMenu');
        }

        function openContextMenu() {
            var $parent = $(settings.parentEl),
                xPosition, yPosition, index = 1000;

            if (settings.position) {
                xPosition = settings.position.left;
                yPosition = settings.position.top;
            } else {
                xPosition = $parent.position().left + 1;
                yPosition = $parent.position().top + 17;
            }
            
            if (settings.zIndex) {
                index = settings.zIndex;
            }

            $(settings.html).css({ top: yPosition, left: xPosition, 'z-index': index });

            if (settings.render) {
                settings.render();
            } else {
                $parent.closest(parentLine).after(settings.html);
            }
            
            bindEvents();
            openContextMenuFlag = true;
        }

        function clickToOutPicker(e) {
            if ($(e.target).closest(contextMenuDiv).length == 0) {
                return false;
            }

            return true;
        }

        // чтобы постоянно не писать, в обработчики пунктов, закрытие контекстного меню
        function clickToOutPickerAfterFuncDo(e) {
            if (($(e.target).closest(contextMenuDiv).length == 0 || $(e.target).closest(menuItem)) && !$.contains(settings.$el, e.target)) {
                return false;
            }

            return true;
        }

        function closeContextMenu() {
            if (settings.closeMenu) {
                settings.closeMenu();
            } else {
                $(settings.parentEl).closest(parentLine).next(contextMenuDiv).remove();
            }
            openContextMenuFlag = false;
        }

        function closeContextMenuEvent(e) {         
            if (!clickToOutPicker(e)) {
                closeContextMenu();
                unbindEvents();
            }
        }

        function closeContextMenuAfterSelectItemEvent(e) {
            if (!clickToOutPickerAfterFuncDo(e)) {
                closeContextMenu();
            }
        }

        return {
            Open: function () {
                if (openContextMenuFlag == true) {
                    closeContextMenu();
                    return;
                }

                openContextMenu();
            },
            Close: function () {
                closeContextMenu();
            }
        };
    };

})(Stock.module('Main'));