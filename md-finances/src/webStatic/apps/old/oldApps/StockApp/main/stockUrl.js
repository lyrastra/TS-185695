(function(url) {

    var prefix = '/Accounting/',
        prefixStock = '/Stock/';

    url.RootPathPage = '';
    url.BaseTemplate = 'ClientSideApps/StockApp/templates/';

    // firmStock 

    url.GetStock = prefixStock + 'FirmStock/GetFirmStock/';
    url.SaveStock = prefixStock + 'FirmStock/Save/';

    // nomenclature

    url.GetStockNomenclature = prefixStock + 'StockNomenclature/GetFirmStockNomenclature/';
    url.SaveStockNomenclature = prefixStock + 'StockNomenclature/Save/';
    url.SaveStockNomenclatureSubTree = prefixStock + 'StockNomenclature/SaveNomenclatureSubTree/';

    // product 

    url.GetProduct = prefixStock + 'StockProduct/GetProduct/';
    url.SaveProduct = prefixStock + 'StockProduct/Save/';

    url.GetProducts = prefixStock + 'StockProduct/GetProducts/';
    url.GetProductsBalance = prefixStock + 'StockProduct/GetProductBalance/';
    url.GetProductsCount = prefixStock + 'StockProduct/GetProductsCount/';
    url.GetStockProduct = prefixStock + 'StockProduct/GetProductsByListId';

    url.DeleteStock = prefixStock + 'FirmStock/Delete/';
    url.DeleteNomenclature = prefixStock + 'StockNomenclature/Delete/';

    // product Stock

    url.StockSaveProduct = prefixStock + 'StockProduct/Save';

    // operation info

    url.GetOperationOverProducts = prefixStock + 'StockOperation/GetOperationOverProducts/';

    // debit operation

    url.GetDebitOperationModel = prefix + 'StockOperation/GetDebitOperationModel';
    url.GetDebitOperationModelByWaybill = prefix + 'StockOperation/GetDebitOperationModelByWaybill';
    url.GetDebitOperationModelByAdvanceStatement = prefix + 'StockOperation/GetDebitOperationModelByAdvanceStatement';
    url.SaveDebitOperation = prefixStock + 'StockOperation/SaveDebitOperation';

    // requisition waybill operation

    url.GetRequisitionWaybill = prefixStock + 'StockOperation/GetRequisitionWaybill';
    url.SaveRequisitionWaybill = prefixStock + 'StockOperation/SaveRequisitionWaybill';

    // movement operation

    url.GetDefaultMoveUrl = prefixStock + 'StockOperation/GetDefaultMovementModel/';
    url.SaveMoveUrl = prefixStock + 'StockOperation/SaveMovementOperation/';

    // other

    url.GetNextOperationNumber = prefixStock + 'StockOperation/GetNextStockOperationNumber';
    url.CheckNumberOperationIsFree = prefixStock + 'StockOperation/NumberOperationIsfree';
    url.DeleteStockOperations = prefixStock + 'StockOperation/DeleteStockOperations';

})(StockUrl.module('Main')); 
