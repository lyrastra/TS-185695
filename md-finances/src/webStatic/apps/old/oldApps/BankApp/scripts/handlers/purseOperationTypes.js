(function (md) {

    md.Data.PurseOperationType = {
        Income: 1,

        Transfer: 2,

        Comission: 3,

        OtherOutgoing: 4,

        getDescription: function(val){
            var dictionary = [
                {
                    Name: 'Перевод на р/с',
                    Id: md.Data.PurseOperationType.Transfer
                },
                {
                    Name: 'Удержание комиссии',
                    Id: md.Data.PurseOperationType.Comission
                },
                {
                    Name: 'Прочее',
                    Id: md.Data.PurseOperationType.OtherOutgoing
                },
                {
                    Name: 'Оплата от покупателя',
                    Id: md.Data.PurseOperationType.Income
                }
           ];

            var result = _.findWhere(dictionary, { Id: val }) || { Name: '' };
            return result.Name;
        }
    };

})(Md);
