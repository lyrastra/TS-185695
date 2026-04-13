(function (stockModule, common) {

    var selectedLineClass = 'mdSelectedItem',
        parentLineElement = 'li',
        parentLevelUl = 'ul',
        parentLineItem = 'mdLMenuItem',
        disabledClass = 'mdDisableItem',
        lastMenuItemClass = 'fourthLevel',
        firstChild = 'firstLevel';

    var stockItemList = '#stockList',
        stockMenu = '.stockMenu',
        stockMenuItemsSelector = stockItemList + ' ul.secondLevel',
        stockIdList = 'stockList',
        stockMenuClass = 'stockMenu',
        stockIdAttr = 'stockid',
        isMainStockAttr = 'isMain';

    var nomenclatureItemList = '#nomenclatureList',
        nomenclatureMenu = '.stockNomenclatureMenu',
        nomenclatureMenuClass = 'stockNomenclatureMenu',
        nomenclatureIdList = 'nomenclatureList',
        nomenclatureIdAttr = 'nomenclatureid';

    var contextMenuElemenet = '.contextMenuElement',
        contextMenuDiv = '.stockContextMenu';

    stockModule.Events = {
        SelectStockItem: function (e) {
            var changeItem;
            if (e) {
                changeItem = selectStockItem(e, this);
            } else {
                setStockItem(0, this.$el);
                this.selectStockItemId = 0;
                changeItem = true;
            }

            if (e !== undefined && changeItem) {
                selectItemTrigger(this);
            }
        },
        SelectNomenclatureItem: function (e) {
            var changeItem;
            if (e) {
                changeItem = selectNomenclatureItem(e, this);
            } else {
                setNomenclatureItem(0, this.$el);
                this.selectedNomenclatureItemId = 0;
                changeItem = true;
            }

            if (e !== undefined && changeItem) {
                selectItemTrigger(this);
            }
        },
        
        StockContextMenu: function (e) {
            var self = this;
            
            invokeEventFunction(e, function() {
                initializeStockContextMenu(e, self);
                e.stopPropagation();
            });
        },
        StockAddMenu: function (e) {
            var self = this;
            
            invokeEventFunction(e, function() {
                initializeStockAddMenu(e, self);
                e.stopPropagation();
            });
        },
        NomenclatureContextMenu: function (e) {
            var self = this;

            invokeEventFunction(e, function() {
                initializeNomenclatureContextMenu(e, self);
                e.stopPropagation();
            });
        },
        
        StockDialog: function (e, type) {
            if (type == undefined) {
                throw 'необходимо указать тип создаваемого склада';
            }

            var self = this;
            var element = this.getElementEvent(e);
            var $li = $(element).closest('.' + parentLineItem);
            var model = new stockModule.Models.StockRowModel();

            model.set('Type', type);
            var dialog = new stockModule.Views.AddStockDialogView(model, true);
            dialog.messageAfterSave = 'Добавлен склад...';
            dialog.on('SaveSuccess', function (m) { self.stockView.addItemCollection(m); self.setCorrectTabIndexAttr(); self.refreshAddButton(); });
            dialog.openInline($li);
        },
        NomenclatureDialog: function (e, parent) {
            var self = this,
                element = this.getElementEvent(e),
                $li = $(element).closest(parentLineElement),
                $item = $(element).closest('.' + parentLineItem);

            invokeEventFunction(e, function () {
                if (parent) {
                    $li = $(contextMenuDiv).prev(parentLineElement);
                    $item = $li.find('.' + parentLineItem).first();
                }

                var nomId = parseInt($li.attr(nomenclatureIdAttr));

                var dialog = new stockModule.Views.AddNomenclatureDialogView({ model: new stockModule.Models.NomenclatureRowModel({ ParentId: nomId }) }, true);
                dialog.messageAfterSave = 'Добавлена подгруппа...';
                dialog.on('SaveSuccess', function (model) { self.stockNomenclatureView.addItemCollection(model); self.setCorrectTabIndexAttr(); });
                dialog.openInline($item);
            });
        },
        
        RenameStockDialog: function (e, li) {
            var self = this;

            var line = li ? li : $(contextMenuDiv).prev(parentLineElement);
            var model = self.stockView.getModelById(parseInt(line.attr(stockIdAttr)));
            var dialog = new stockModule.Views.AddStockDialogView(model, true);
            dialog.messageAfterSave = 'Переименование склада...';
            dialog.on('SaveSuccess', function (m) { self.stockView.renameStockItem(m); });
            dialog.openInline(line, true);
        },
        RenameNomenclatureDialog: function (e, li) {
            var self = this;
            
            var line = li ? li : $(contextMenuDiv).prev(parentLineElement);

            var model = self.stockNomenclatureView.getModelById(parseInt(line.attr(nomenclatureIdAttr)));

            var dialog = new stockModule.Views.AddNomenclatureDialogView({ model: new stockModule.Models.NomenclatureRowModel(model) }, true);
            dialog.messageAfterSave = 'Переименование подгруппы...';
            dialog.on('SaveSuccess', function (m) { self.stockNomenclatureView.renameItem(m); });
            dialog.openInline(line, true);
        },
        
        DeleteNomenclatureDialog: function (e, li) {
            var self = this;

            var line = li ? li : $(contextMenuDiv).prev(parentLineElement);
            var deleteId = parseInt(line.attr(nomenclatureIdAttr));

            var model = new stockModule.Models.DeleteStockModel();
            model.set('DeleteId', deleteId);
            model.set('DeleteName', line.find('span').text());
            model.set('ItemList', self.stockNomenclatureView.getListForSelectorWithout(deleteId));

            var dialog = new stockModule.Views.DeleteStockDialog({
                model: model,
                text: {
                    title: 'Удаление подгруппы',
                    deleteText: 'Подгруппа удалена...',
                    textForDeleteConfirm: 'Операция удаления является необратимой. Вы уверены, что хотите удалить подгруппу и переместить все товары?'
                },
                isNomenclature: true
            });

            dialog.on('loadTemplateComplete', function () { openDeleteDialog(dialog); });
            dialog.on('DeleteSuccess', function (m) { self.stockNomenclatureView.deleteItem(m); });

            if (dialog.isTemplateLoad) {
                openDeleteDialog(dialog);
            }
        },
        DeleteStockDialog: function (e, item) {
            var self = this,
                countStock = $(stockItemList + ' ' + parentLineElement).length;
            if (e) {
                var element = this.getElementEvent(e);
                var $el = $(element).closest('div');

                if (!$el.hasClass(disabledClass) && countStock > 1) {
                    deleteStock(self, null);
                }
            } else if (item && countStock > 1) {
                deleteStock(self, item);
            } else { }
        },
        
        MenuKeyEvent: function (e) {
            e = e || window.event;
            var key = e.charCode || e.keyCode || e.which;

            if (key == common.Data.KeyCodeEnum.Enter) {
                enterOnMenu(this, e);
            } else if (key == common.Data.KeyCodeEnum.F2) {
                f2OnMenu(this, e);
            } else if (key == common.Data.KeyCodeEnum.Del) {
                delOnMenu(this, e);
            }
        },
        OpenInventoryOperation: function (e) {
            var line = $(contextMenuDiv).prev(parentLineElement);
            var stockId = parseInt(line.attr(stockIdAttr));
            this.router.navigate(stockModule.Routers.InventoryRoute + '/' + stockId, { trigger: true });
        }
    };

    function openDeleteDialog(dialog) {
        dialog.open();
    }

    function selectStockItem(e, view) {
        var element = view.getElementEvent(e),
            li = $(element).closest(parentLineElement),
            parseId = parseInt($(li).attr(stockIdAttr));

        var currentItem = parseInt(view.selectStockItemId);
        var selectedItem = li.length > 0 ? _.isNaN(parseId) ? 0 : parseId : 0;

        if (currentItem == selectedItem) {
            return false;
        }
        
        resetSelectedItem(view.$el.find(stockMenu));
        setStockItem(selectedItem, view.$el);

        view.selectStockItemId = selectedItem;

        return true;
    }

    function resetSelectedItem($el) {
        $el.find('.' + selectedLineClass).removeClass(selectedLineClass);
    }

    function setStockItem(idItem, $el) {
        var li;

        if (idItem === 0) {
            li = $el.find(stockItemList + ' ' + parentLineElement).first().find(' .' + parentLineItem);
        } else {
            li = $el.find(stockMenuItemsSelector + ' ' + parentLineElement + '[' + stockIdAttr + '="' + idItem + '"] .' + parentLineItem);
        }
        li.addClass(selectedLineClass);
    }

    function setNomenclatureItem(idItem, $el) {
        var li;

        if (idItem === 0) {
            li = $el.find(nomenclatureItemList + ' ' + parentLineElement).first().find(' .' + parentLineItem);
        } else {
            li = $el.find(parentLineElement + '[' + nomenclatureIdAttr + '="' + idItem + '"] .' + parentLineItem).first();
        }

        li.addClass(selectedLineClass);
    }

    function selectNomenclatureItem(e, view) {
        var element = view.getElementEvent(e),
            li = $(element).closest(parentLineElement),
            parseId = parseInt($(li).attr(nomenclatureIdAttr));

        var currentItem = parseInt(view.selectedNomenclatureItemId);
        var selectedItem = li.length > 0 ? _.isNaN(parseId) ? 0 : parseId : 0;

        if (currentItem == selectedItem) {
            return false;
        }

        resetSelectedItem(view.$el.find(nomenclatureMenu));
        setNomenclatureItem(selectedItem, view.$el);

        view.selectedNomenclatureItemId = selectedItem;

        return true;
    }
    
    function initializeStockAddMenu(e, view) {
        var element = view.getElementEvent(e);
        var parent = $(element).closest(contextMenuElemenet);
        
        var contextMenuDivHtml = stockModule.Helpers.StockMenuTemplate.ContextMenuDiv();
        var topContextMenuHtml = stockModule.Helpers.StockMenuTemplate.AddStockContextSection();
        
        var jqHtml = $(contextMenuDivHtml).append(topContextMenuHtml);
        
        var options = {
            parentEl: parent,
            parentLine: '.mdLMenuItem',
            html: jqHtml,
            addWholesaleStock: function() {
                stockModule.Events.StockDialog.call(view, e, 1);//1 - оптовый
            },
            addRetailStock: function () {
                stockModule.Events.StockDialog.call(view, e, 2);//2 - розничный
            }
        };

        var contextMenu = new stockModule.Helpers.ContextMenu(options);
        contextMenu.Open();
    }

    function initializeStockContextMenu(e, view) {
        var element = view.getElementEvent(e);
        var parent = $(element).closest(contextMenuElemenet);
        var disableDelete = '';

        if (parent.closest(parentLineElement).attr(isMainStockAttr) === 'true') {
            disableDelete = disabledClass;
        }

        var contextMenuDivHtml = stockModule.Helpers.StockMenuTemplate.ContextMenuDiv();
        var topContextMenuHtml = stockModule.Helpers.StockMenuTemplate.StockTopContextSection();
        var downContextMenuHtml = stockModule.Helpers.StockMenuTemplate.ContextMenuDownSection(disableDelete);

        var jqHtml = $(contextMenuDivHtml).append(downContextMenuHtml);

        var options = {
            parentEl: parent,
            html: jqHtml,
            inventory: function (event) { stockModule.Events.OpenInventoryOperation.call(view, event); },
            importExcel: function () { },
            rename: function (event) { stockModule.Events.RenameStockDialog.call(view, event); },
            deleteItem: function (event) { stockModule.Events.DeleteStockDialog.call(view, event); }
        };

        var contextMenu = new stockModule.Helpers.ContextMenu(options);
        contextMenu.Open();
    }

    function initializeNomenclatureContextMenu(e, view) {
        var element = view.getElementEvent(e),
            parent = $(element).closest(contextMenuElemenet),
            parentUl = $(element).closest(parentLevelUl),
            disableDelete = '', disableAddItem = '',
            isLastLevelTree = parentUl.hasClass(lastMenuItemClass);

        if (isLastLevelTree) {
            disableAddItem = disabledClass;
        }

        var contextMenuDivHtml = stockModule.Helpers.StockMenuTemplate.ContextMenuDiv();
        var topContextMenuHtml = stockModule.Helpers.StockMenuTemplate.NomenclatureTopContextmenu(disableAddItem);
        var downContextMenuHtml = stockModule.Helpers.StockMenuTemplate.ContextMenuDownSection(disableDelete);

        var jqHtml = $(contextMenuDivHtml).append(topContextMenuHtml).append(downContextMenuHtml);

        var options = {
            parentEl: parent,
            html: jqHtml,
            unit: function (event) { },
            add: function (event) { if (!isLastLevelTree) { stockModule.Events.NomenclatureDialog.call(view, event, parent); } },
            rename: function (event) { stockModule.Events.RenameNomenclatureDialog.call(view, event); },
            deleteItem: function (event) { stockModule.Events.DeleteNomenclatureDialog.call(view, event); }
        };

        var contextMenu = new stockModule.Helpers.ContextMenu(options);
        contextMenu.Open();
    }

    function selectItemTrigger(view) {
        var stockId = parseInt(view.selectStockItemId);
        var nomenclatureId = parseInt(view.selectedNomenclatureItemId);

        nomenclatureId = nomenclatureId === 0 ? null : nomenclatureId;

        if (nomenclatureId !== null) {
            var nomenclatureList = view.stockNomenclatureView.nomenclatureTree.GetSubTree(nomenclatureId);
            if (nomenclatureList !== null) {
                nomenclatureId = nomenclatureList;
            }
        }

        rejectionEvent(view, stockId, nomenclatureId);
    }

    function rejectionEvent(view, stock, nomenclature) {
        view.trigger('SelectedItemMenu', nomenclature, stock);
    }

    function deleteStock(self, item) {
        var stockUrl = StockUrl.module('Main');

        var line = item ? item : $(contextMenuDiv).prev(parentLineElement);
        var deleteId = parseInt(line.attr(stockIdAttr));

        if (line.attr(isMainStockAttr) === 'true') {
            return;
        }

        var model = new stockModule.Models.DeleteStockModel();
        model.url = stockUrl.DeleteStock;
        model.set('DeleteId', deleteId);
        model.set('DeleteName', line.find('span').text());
        model.set('ItemList', self.stockView.getModelListWithout(deleteId));

        var dialog = new stockModule.Views.DeleteStockDialog({
            model: model,
            text: {
                title: 'Удаление склада',
                deleteText: 'Склад удален...',
                textForDeleteConfirm: 'Операция удаления является необратимой. Вы уверены, что хотите удалить склад и переместить все товары?'
            }
        });        

        dialog.on('loadTemplateComplete', function () { openDeleteDialog(dialog); });
        dialog.on('DeleteSuccess', function (m) { self.stockView.deleteItem(m); self.refreshAddButton(); });

        if (dialog.isTemplateLoad) {
            openDeleteDialog(dialog);
        }
    }

    function f2OnMenu(view, e) {
        var li = view.getElementEvent(e),
            $parent = $(li).parent();

        var parentId = $parent.get(0).id;

        if (parentId != '') {
            switch (parentId) {
                case stockIdList:
                    stockModule.Events.RenameStockDialog.call(view, null, $(li));
                    break;
                case nomenclatureIdList:
                    stockModule.Events.RenameNomenclatureDialog.call(view, null, $(li));
                    break;
                default:
                    break;
            }
        }
    }

    function enterOnMenu(view, e) {
        var li = view.getElementEvent(e),
            $parent = $(li).parent(),
            selectStock = 'SelectStockItem',
            selectNomenclature = 'SelectNomenclatureItem',
            method;

        var parentId = $parent.get(0).id;

        if (parentId != '') {
            switch (parentId) {
                case stockIdList:
                    method = selectStock;
                    break;
                case nomenclatureIdList:
                    method = selectNomenclature;
                    break;
                default:
                    method = null;
                    break;
            }
        } else {
            if ($parent.hasClass(stockMenuClass)) {
                method = selectStock;
            } else if ($parent.hasClass(nomenclatureMenuClass)) {
                method = selectNomenclature;
            } else {
                method = null;
            }
        }

        callEventsMethod(method, view, e);
    }

    function delOnMenu(view, e) {
        var li = view.getElementEvent(e),
            $parent = $(li).parent(),
            event = e,
            method,
            data;

        var deleteStockMethod = 'DeleteStockDialog',
            deleteNomenclatureMethod = 'DeleteNomenclatureDialog';

        var parentId = $parent.get(0).id;
        data = $(li);

        if (parentId != '') {
            switch (parentId) {
                case stockIdList:
                    method = deleteStockMethod;
                    event = null;
                    break;
                case nomenclatureIdList:
                    if (!data.hasClass(firstChild)) {
                        method = deleteNomenclatureMethod;
                        event = null;
                    } else {
                        method = null;
                    }
                    break;
                default:
                    method = null;
                    break;
            }
        } else {
            method = null;
            event = null;
        }

        callEventsMethod(method, view, event, data);
    }

    function callEventsMethod(method, view, e, data) {
        if (method !== null) {
            stockModule.Events[method].call(view, e, data);
        }
    }
    
    function canEditPage() {
        if (common.Utils.CommonDataLoader.FirmRequisites.get("CurrentPay") == Enums.PaymentStatus.Unpaid) {
            return false;
        }

        return true;
    }
    
    function invokeEventFunction(e, func) {
        var companyId = Md.Core.Engines.CompanyId;

        if (canEditPage()) {
            func();
        } else {
            var $elem = $(e.target);
            var url = companyId.getLinkWithParams('/Pay/');
            var message = 'Чтобы добавлять, редактировать, удалять <a href="' + url + '">оплатите</a> доступ к сервису.';

            ToolTip.popupMessage(message, $elem, $elem);
        }
    }

})(Stock, Common);
