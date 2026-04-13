const PrimaryDocsStatusEnum = {
    CantHaveAnyDocs: 0, // не может иметь никаких связанных документов
    CantHavePrimaryDocs: 1, // не может иметь первичных документов
    UnpaidPrimaryDocs: 2, // не оплачено/не полностью оплачено
    PaidPrimaryDocs: 3 // полностью оплачено
};

export default PrimaryDocsStatusEnum;
