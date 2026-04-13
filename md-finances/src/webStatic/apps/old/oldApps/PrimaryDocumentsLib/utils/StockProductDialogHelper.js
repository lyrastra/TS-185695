(function (primaryDocuments, components) {
    'use strict';

    primaryDocuments.Utils.StockProductDialogHelper = {
        showDialog: function(documentItem, save, cancel, settings) {
            var productDialog;
            var nomenclatures = new Stock.Models.NomenclatureModel();
            nomenclatures.fetch({
                success: function () {
                    var dialogSettings = {
                        onSave: function (savedModel) {
                            var productModel = productDialog.model;

                            var nds = productModel.get('NdsInfo');

                            var model = {
                                StockProductId: savedModel.SavedId,
                                ShortName: productModel.get('ProductName'),
                                Article: productModel.get('Article'),
                                Unit: productModel.get('UnitOfMeasurement'),
                                UnitCode: productModel.get('UnitCode'),
                                Price: Converter.toFloat(productModel.get('SalePrice')) || '',
                                Nds: getNds(productModel),
                                NdsPositionType: nds ? nds.get('Position') : null,
                                Count: '',
                                SubcontoId: savedModel.SubcontoId,
                                Type: productModel.get("Type")
                            };
                            
                            if (_.isFunction(save)) {
                                save(model);
                            }
                        },
                        cancel: function () {
                            if (_.isFunction(cancel)) {
                                cancel();
                            }
                        },
                        canCreatePostings: new Common.FirmRequisites().get('TariffMode') === Common.Data.TariffModes.UsnAccountant
                    };

                    if (documentItem) {
                        dialogSettings.editModel = {
                            Id: documentItem.get("StockProductId"),
                            Name: documentItem.get("ShortName"),
                            SalePrice: documentItem.get("Price"),
                            UnitOfMeasurement: documentItem.get("Unit"),
                            UnitCode: documentItem.get("UnitCode"),
                            NDS: documentItem.get("NdsType")
                        };
                    }

                    if (settings) {
                        _.extend(dialogSettings, settings);
                    }

                    productDialog = new components.StockProductDialog.StockProductDialogView(dialogSettings);

                    productDialog.on('DownloadDialogDataComplete', function () {
                        window.ZIndex.refresh();
                        productDialog.afterLoadTemplate();
                        productDialog.open();
                    });

                    productDialog.loadDialog();
                },
                error: function () {
                    Stock.Helpers.MessageDialog.ErrorSaveOperation('Ошибка инициализации диалога...');
                }
            });
        }
    };
    
    /** access private */
    function getNds(productModel) {
        var ndsModel = productModel.get('NdsInfo');

        if(ndsModel){
            var type = ndsModel.get && ndsModel.get('Type') || 1;
            try{
                var converter = Md.Converters.Nds.NdsType;
                return converter.accountToBizType(type);
            } catch(e){
                throw 'NdsType converter must be defined';
            }
        }
    }
})(PrimaryDocuments, Md.Components);
