(function (stockModule) {

    stockModule.Helpers.StockMenuTemplate = {
        StockLiTemplate: function () {
            return '<div><li data-bind="Id"><div class="textItem"><span data-bind="Name"></span><span class="isMainStockInfo" data-bind="IsMain"></span></div><div class="contextMenuElement"><div class="contextMenuButton"></div></div></li></div>';
        },
        NomenclatureLiTemplate: function () {
            return '<li nomenclatureid=<%=Id%> class="<%=ChildClass%>"><div class="textItem"><span><%=Name%></span></div><div class="contextMenuElement"><div class="contextMenuButton"><% if (ChildClass == "firstChild") { %> + <% } %></div></div></li>';
        },
        ContextMenuDiv: function () {
            return '<div class="stockContextMenu boxShadows"></div>';
        },
        AddStockContextSection: function () {
            return '<div class="addStockContextMenu"><div id="AddWholesaleStock" class="menuItem"><span>оптовый склад</span></div><div id="AddRetailStock" class="menuItem"><span>розничный склад</span></div></div>';
        },
        StockTopContextSection: function () {
            return '<div class="stockTopContextMenu"><div id="Inventory" class="menuItem"><span>Инвентаризация</span></div><div id="ImportExcel" class="menuItem"><span>Импорт из экселя</span></div></div>';
        },
        ContextMenuDownSection: function (disableDelete) {
            return '<div class="stockDownContextMenu"><div id="Rename" class="menuItem"><span>Переименовать</span></div><div id="Delete" class="menuItem ' + disableDelete + '"><span>Удалить...</span></div></div>';
        },
        NomenclatureTopContextmenu: function (disableAddItem) {
            return '<div class="stockTopContextMenu"><div id="AddNomenclatureGroup" class="menuItem ' + disableAddItem + '" ><span>Добавить</span></div></div>';
        },
        NewStockDialogContent: function () {
            return '<div id="addStockDialog" class="dialog form"><div class="dialog-content form-content"><ul><li><label for="stockName">Название:</label><input type="text" id="stockName" class="standart" data-bind="Name" /><li><label for="">Тип склада:</label><select id="StockTypeList" class="standart" data-bind="Type"><%_.forEach(StockTypeList, function (n) {%><option value="<%=n.TypeId%>"><%=n.TypeName%></option><%})%></select></li></li></ul></div><div class="dialog-footer form-footer"><ul><li><label></label><div id="stockSave" class="button"><a>Сохранить</a></div><div id="stockCancel" class="inline-block"><span class="link">Отменить</span></div></li></ul></div></div>';
        },
        NewNomenclatureDialogContent: function () {
            return '<div id="addNomenclatureDialog" class="dialog form"><div class="dialog-content form-content"><ul><li><label for="nomenclatureName">Название:</label><input type="text" id="nomenclatureName" class="standart" data-bind="Name" /></li></ul></div><div class="dialog-footer form-footer"><ul><li><label></label><div id="nomenclatureSave" class="button"><a>Сохранить</a></div><div id="nomenclatureCancel" class="inline-block"><span class="link">Отменить</span></div></li></ul></div></div>';
        },
        RenameStockInput: function () {
            return '<div class="mdCustomLineInput renameMenuInput bottomBlackShadow"><input type="text" id="stockName" class="mdCustomInput" value="" /><div class="mdCustomIcon mdCustomIcon-button"></div></div>';
        },
        RenameNomenclatureInput: function () {
            return '<div class="mdCustomLineInput renameMenuInput bottomBlackShadow"><input type="text" id="nomenclatureName" class="mdCustomInput" value="" /><div class="mdCustomIcon mdCustomIcon-button"></div></div>';
        },
        DialogNomenclatureContextMenu: function (disableAddItem, addDisabled) {
            return '<div id="dialogContextMenu" class="dialogContextMenu stockContextMenu"><div id="AddNomenclature" class="menuItem ' + addDisabled + '"><span>Добавить</span></div><div id="RenameNomenclature" class="menuItem"><span>Переименовать</span></div><div id="DeleteNomenclature" class="menuItem ' + disableAddItem + '" ><span>Удалить</span></div></div>';
        }
    };

})(Stock);