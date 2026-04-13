(function (primaryDocuments, posting, common) {
    primaryDocuments.Utils.TaxPostingTooltipMixin = function (tableView) {
        
        tableView.getItemById = function (id) {
            id = Converter.toInteger(id);
            return this.collection.find(function (item) { return item.get("Id") == id; });
        };

        function definitionClassForTaxationSystem(item) {
            if (!item) { return; }
            var date = Converter.toDate(item.get("Date"));

            const ts = new Money.Collections.Common.TaxationSystemCollection(window._preloading.TaxationSystems);
            const taxationSystem = ts.Current(date);

            if (taxationSystem.isOsno) {
                return "osnoTooltip";
            }
        }

        var showTaxPostings = function (event) {
            var cell = $(event.currentTarget || event.target),
                id = cell.closest(".mdTableRow").attr("data-id"),
                item = this.getItemById(id);

            if(!item){
                return;
            }

            var hasPosting = item.get("ProvideInTax") != common.Data.TaxPostingStatus.No && item.get("ProvideInTax") != common.Data.TaxPostingStatus.NotTax;
            
            if (cell.hasClass("hover")) {
                return;
            }

            if (hasPosting) {
                cell.addClass("hover");
                _.delay(function () {
                    if (cell.hasClass("hover")) {
                        if (item.get("DocumentBaseId")) {
                            posting.Utils.TooltipTaxPostingHelper.show(item.get("DocumentBaseId"), {
                                target: cell,
                                additionalClass: definitionClassForTaxationSystem(item)
                        });
                        }
                    }
                }, 200);
            }
        };

        var hideTaxPostingsOnHover = function (event) {
            var cell = $(event.currentTarget || event.target);
            cell.removeClass("hover");
            posting.Utils.TooltipTaxPostingHelper.hide();
        };
        
        var hideTaxPostingsOnClick = function (event) {
            $(event.target).closest(".mdTableCell").removeClass("hover");
            posting.Utils.TooltipTaxPostingHelper.hide();
        };

        tableView.collection.on('reset', posting.Utils.TooltipTaxPostingHelper.clearCash, posting.Utils.TooltipTaxPostingHelper);

        _.extend(tableView.events, {
            "mouseover .mdTableRow .ttProvideInTax": showTaxPostings,
            "mouseout .mdTableRow .ttProvideInTax": hideTaxPostingsOnHover,
            "mousedown .mdTableRow .ttProvideInTax": hideTaxPostingsOnClick
        });
  
        return this;
    };
})(PrimaryDocuments, AccountingPosting, Common);