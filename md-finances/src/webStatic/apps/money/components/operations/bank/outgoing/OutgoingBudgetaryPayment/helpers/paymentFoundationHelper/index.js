import PaymentReasonEnum from '../../enums/PaymentReasonEnum';

export const isTPPaymentFoundationTouched = ({
    oldPaymentBase, newPaymentBase, oldLiteralCode, newLiteralCode, isComplexDocumentNumber
}) => {
    if (!isComplexDocumentNumber) {
        return [newPaymentBase, oldPaymentBase, oldLiteralCode].includes(PaymentReasonEnum.Tr);
    }

    if ([newPaymentBase, oldPaymentBase].includes(PaymentReasonEnum.FreeDebtRepayment)) {
        return [newLiteralCode, oldLiteralCode].includes(PaymentReasonEnum.Tr);
    }

    return false;
};

export const isTPPaymentFoundationSelected = ({ paymentBase, literalCode, isComplexDocumentNumber }) => {
    if (isComplexDocumentNumber) {
        return literalCode === PaymentReasonEnum.Tr;
    }

    return paymentBase === PaymentReasonEnum.Tr;
};
