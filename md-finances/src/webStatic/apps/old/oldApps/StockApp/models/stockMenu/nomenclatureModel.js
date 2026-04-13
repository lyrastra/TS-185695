(function (stockModule) {

    var stockUrl = StockUrl.module('Main');

    stockModule.Models.NomenclatureModel = stockModule.Models.BaseModel.extend({
        url: stockUrl.GetStockNomenclature,
        firstChildClass: 'firstChild',
        secondChildClass: 'secondChild',
        thirdChildClass: 'thirdChild',
        fourthChildClass: 'fourthChild',
        defaults: {
            Status: false,
            StatisticsAction: 0,
            List: new stockModule.Collections.NomenclatureMenuCollection,
            ChildClass: ''
        },
        parse: function (resp) {
            resp = stockModule.Models.BaseModel.prototype.parse.call(this, resp);

            return resp;
        },
        toExpandedArray: function () {
            var list = this.get('List');
            return this.getList(list, 1);
        },
        getList: function (child, level) {
            var result = [];

            for (var i = 0; i < child.length; i++) {
                child[i].ChildClass = this.getLevelClass(level);
                result.push(child[i]);
                if (child[i].ChildNomenclatures.length > 0) {
                    result = result.concat(this.getList(child[i].ChildNomenclatures, level + 1));
                }
            }
            return result;
        },
        getLevelClass: function (level) {
            switch (level) {
                case 1:
                    return this.firstChildClass;
                case 2:
                    return this.secondChildClass;
                case 3:
                    return this.thirdChildClass;
                case 4:
                    return this.fourthChildClass;
                default:
                    return '';
            }
        }
    });
    
    stockModule.Models.NomenclatureTreeModel = stockModule.Models.BaseModel.extend({
        url: stockUrl.SaveStockNomenclatureSubTree,
        
        defaults: {
            clientData: []
        }
        
    });

})(Stock);