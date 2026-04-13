import { isTPPaymentFoundationTouched } from './index';
import PaymentReasonEnum from '../../enums/PaymentReasonEnum';

describe(`isTPPaymentFoundationTouched for single document number`, () => {
    const isComplexDocumentNumber = false;

    // Должен вернуть true, если старое или новое значение поля 106 равно Tp
    it(`should return true if field 106 value is switching from or to Tr value`, () => {
        const result1 = isTPPaymentFoundationTouched({
            oldPaymentBase: PaymentReasonEnum.FreeDebtRepayment,
            newPaymentBase: PaymentReasonEnum.Tr,
            oldLiteralCode: PaymentReasonEnum.None,
            newLiteralCode: PaymentReasonEnum.None,
            isComplexDocumentNumber
        });
        const result2 = isTPPaymentFoundationTouched({
            oldPaymentBase: PaymentReasonEnum.Tr,
            newPaymentBase: PaymentReasonEnum.FreeDebtRepayment,
            oldLiteralCode: PaymentReasonEnum.None,
            newLiteralCode: PaymentReasonEnum.None,
            isComplexDocumentNumber
        });

        expect(result1).toBe(true);
        expect(result2).toBe(true);
    });

    // Для кейса с изменением даты на значение до даты начала действия составных номеров документа.
    // В этом случае isComplexDocumentNumber уже false, затем сбрасывается literalCode
    it(`should return true if field 108 value is switching from Tr value`, () => {
        const result = isTPPaymentFoundationTouched({
            oldPaymentBase: PaymentReasonEnum.FreeDebtRepayment,
            newPaymentBase: PaymentReasonEnum.FreeDebtRepayment,
            oldLiteralCode: PaymentReasonEnum.Tr,
            newLiteralCode: PaymentReasonEnum.None,
            isComplexDocumentNumber
        });

        expect(result).toBe(true);
    });

    // Должен вернуть false, если старое и новое значение поля 106 не равны Tp
    it(`should return false if field 106 value is switching between not Tr values`, () => {
        const result = isTPPaymentFoundationTouched({
            oldPaymentBase: PaymentReasonEnum.FreeDebtRepayment,
            newPaymentBase: PaymentReasonEnum.CurrentPayment,
            oldLiteralCode: PaymentReasonEnum.None,
            newLiteralCode: PaymentReasonEnum.None,
            isComplexDocumentNumber
        });

        expect(result).toBe(false);
    });
});

describe(`isTPPaymentFoundationTouched for complex document number`, () => {
    const isComplexDocumentNumber = true;

    // Должен вернуть false, если старое и новое значение поля 106 не равны FreeDebtRepayment
    it(`should return false if field 106 value is switching between not FreeDebtRepayment values`, () => {
        const result = isTPPaymentFoundationTouched({
            oldPaymentBase: PaymentReasonEnum.CurrentPayment,
            newPaymentBase: PaymentReasonEnum.PB,
            oldLiteralCode: PaymentReasonEnum.None,
            newLiteralCode: PaymentReasonEnum.None,
            isComplexDocumentNumber
        });

        expect(result).toBe(false);
    });
    // Должен вернуть false, если старое или новое значение поля 106 равно FreeDebtRepayment,
    // но поле 108 имеет значение не Tr
    it(`should return false if field 106 value is switching from or to FreeDebtRepayment value, but 106 field value is not Tp`, () => {
        const result = isTPPaymentFoundationTouched({
            oldPaymentBase: PaymentReasonEnum.CurrentPayment,
            newPaymentBase: PaymentReasonEnum.FreeDebtRepayment,
            oldLiteralCode: PaymentReasonEnum.None,
            newLiteralCode: PaymentReasonEnum.None,
            isComplexDocumentNumber
        });

        expect(result).toBe(false);
    });
    // Должен вернуть true, если старое или новое значение поля 106 равно FreeDebtRepayment,
    // и поле 108 имеет значение Tr
    it(`should return true if field 106 value is switching from or to FreeDebtRepayment value, and 106 field value is Tp`, () => {
        const result = isTPPaymentFoundationTouched({
            oldPaymentBase: PaymentReasonEnum.CurrentPayment,
            newPaymentBase: PaymentReasonEnum.FreeDebtRepayment,
            oldLiteralCode: PaymentReasonEnum.Tr,
            newLiteralCode: PaymentReasonEnum.Tr,
            isComplexDocumentNumber
        });

        expect(result).toBe(true);
    });
    // Должен вернуть false, если старое и новое значение поля 108 не равно Tp
    it(`should return false if field 108 value is not switching from or to Tr value`, () => {
        const result = isTPPaymentFoundationTouched({
            oldPaymentBase: PaymentReasonEnum.FreeDebtRepayment,
            newPaymentBase: PaymentReasonEnum.FreeDebtRepayment,
            oldLiteralCode: PaymentReasonEnum.PB,
            newLiteralCode: PaymentReasonEnum.Pr,
            isComplexDocumentNumber
        });

        expect(result).toBe(false);
    });
    // Должен вернуть true, если старое или новое значение поля 108 равно Tp
    it(`should return true if field 108 value is switching from or to Tr value`, () => {
        const result1 = isTPPaymentFoundationTouched({
            oldPaymentBase: PaymentReasonEnum.FreeDebtRepayment,
            newPaymentBase: PaymentReasonEnum.FreeDebtRepayment,
            oldLiteralCode: PaymentReasonEnum.Tr,
            newLiteralCode: PaymentReasonEnum.Pr,
            isComplexDocumentNumber
        });
        const result2 = isTPPaymentFoundationTouched({
            oldPaymentBase: PaymentReasonEnum.FreeDebtRepayment,
            newPaymentBase: PaymentReasonEnum.FreeDebtRepayment,
            oldLiteralCode: PaymentReasonEnum.Pr,
            newLiteralCode: PaymentReasonEnum.Tr,
            isComplexDocumentNumber
        });

        expect(result1).toBe(true);
        expect(result2).toBe(true);
    });
});
