const TaxStatusEnum = {
    /* Не облагается по законодательству */
    NotTax: 0,

    /* Вручную */
    ByHand: 1,

    /* Облагается, есть проводки */
    Yes: 2,

    /* Облагается, нет проводок */
    No: 3,

    /* По связанному документу */
    ByLinkedDocument: 4
};

export default TaxStatusEnum;
