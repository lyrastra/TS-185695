(function (primaryDocuments, main) {
    main.PrimaryDocuments = primaryDocuments || {};
    main.PrimaryDocuments.SalesWaybillTypes = {
        Sale: {
            Code: 200,
            Name: 'Продажа'
        },

        SaleDonation: {
            Code: 201,
            Name: 'Безвозмездная передача'
        },
        
        Return: {
            Code: 103
        }
    };
})(PrimaryDocuments, window);