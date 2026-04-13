import PostingTransferType from '../../enums/newMoney/TaxPostingTransferTypeEnum';
import TaxPostingTransferKind from '../../enums/newMoney/TaxPostingTransferKindEnum';
import TaxPostingNormalizedCostType from '../../enums/newMoney/TaxPostingNormalizedCostTypeEnum';

export default {
    transferTypePostings: {
        [PostingTransferType.Direct]: `Прямой`,
        [PostingTransferType.Indirect]: `Косвенный`,
        [PostingTransferType.NonOperating]: `Внереализационный`,
        [PostingTransferType.OperationIncome]: `От реализации`
    },

    transferKindPostings: {
        [TaxPostingTransferKind.None]: `Нет`,
        [TaxPostingTransferKind.Service]: `Реализация услуг`,
        [TaxPostingTransferKind.ProductSale]: `Реализация покупных товаров`,
        [TaxPostingTransferKind.PropertyRight]: `Реализация имущественных прав`,
        [TaxPostingTransferKind.OtherPropertySale]: `Реализация прочего имущества`,
        [TaxPostingTransferKind.ManufacturedProductSale]: `Реализация продукции`,
        [TaxPostingTransferKind.Material]: `Материальный`,
        [TaxPostingTransferKind.Salary]: `Оплата труда`,
        [TaxPostingTransferKind.Amortization]: `Амортизация`,
        [TaxPostingTransferKind.OtherOutgo]: `Прочий расход`,
        [TaxPostingTransferKind.Manufacturing]: `Производство`
    },

    normalizedCostTypePostings: {
        [TaxPostingNormalizedCostType.None]: `Нет`,
        [TaxPostingNormalizedCostType.CompensationForEmployeesLoans]: `Расходы на возмещение затрат работников по уплате процентов по кредитам на приобретение или строительство жилого помещения п.24.1 ч.2 ст.255 НК РФ`,
        [TaxPostingNormalizedCostType.ResearchAndDevelopment]: `Расходы на НИОКР ст.262 НК РФ`,
        [TaxPostingNormalizedCostType.EntertainmentCosts]: `Представительские расходы пп.22 п.1 и п.2 ст.264 НК РФ`,
        [TaxPostingNormalizedCostType.Advertisement]: `Расходы на рекламу пп.28 п.1 и п.4 ст.264 НК РФ`,
        [TaxPostingNormalizedCostType.InterestOnDebt]: `Расходы в виде процентов по долговым обязательствам пп.2 п.1 ст.265 и ст.269 НК РФ`,
        [TaxPostingNormalizedCostType.Cession]: `Убыток от уступки права требования п.1 ст.279 НК РФ`,
        [TaxPostingNormalizedCostType.DamageDuringStorage]: `Потери от недостачи и/или порчи при хранении и транспортировке МПЗ Постановление Правительства РФ от 12.11.2002 № 814`,
        [TaxPostingNormalizedCostType.CompensationForTransportationFarNorth]: `Расходы по оплате стоимости проезда и провоза багажа работнику организации, расположенной в районах Крайнего Севера и приравненных к ним местностям, и членам его семьи п.12.1 ч.2 ст.255 НК РФ`,
        [TaxPostingNormalizedCostType.RequiredPropertyInsurance]: `Расходы на обязательное страхование имущества п.2 ст.263 НК РФ`,
        [TaxPostingNormalizedCostType.CompensationForUsagePersonalTransport]: `Расходы на компенсацию за использование для служебных поездок личного транспорта Постановление Правительства РФ от 08.02.2002 № 92`,
        [TaxPostingNormalizedCostType.Notarization]: `Плата государственному и/или частному нотариусу за нотариальное оформление ст.333.24 НК РФ`,
        [TaxPostingNormalizedCostType.ServiceProductionAndFarms]: `Расходы обслуживающих производств и хозяйств ст. 275.1 НК РФ`
    }
};
