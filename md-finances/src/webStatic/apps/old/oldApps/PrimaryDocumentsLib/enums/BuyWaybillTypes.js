(function (primaryDocuments, main) {
    main.PrimaryDocuments = primaryDocuments || {};
    main.PrimaryDocuments.BuyWaybillTypes = {
        Buy: {
            Code: 100,
            Name: "Покупка"
        },

        BuyDonation: {
            Code: 101,
            Name: "Безвозмездное получение"
        }
    };
})(PrimaryDocuments, window);