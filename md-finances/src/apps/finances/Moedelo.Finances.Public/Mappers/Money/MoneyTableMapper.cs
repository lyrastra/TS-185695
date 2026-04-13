using System;
using System.Linq;
using Moedelo.Common.Enums.Enums.PostingEngine;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;
using Moedelo.Finances.Domain.Enums.Money.Table;
using Moedelo.Finances.Domain.Models.Money.Operations;
using Moedelo.Finances.Domain.Models.Money.Table;
using Moedelo.Finances.Public.ClientData.Money.Table;
using Moedelo.Finances.Public.ClientData.Money;
using Moedelo.Finances.Public.ClientData.Money.Table.Main;
using Moedelo.Common.Enums.Enums.SettlementAccounts;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Public.ClientData.Money.Table.OutsourceProcessing;

namespace Moedelo.Finances.Public.Mappers.Money
{
    public static class MoneyTableMapper
    {
        public static MoneyTableRequest MapTableRequestToDomain(this MoneyTableRequestClientData clientData)
        {
            return new MoneyTableRequest
            {
                Count = clientData.Count,
                Offset = clientData.Offset,
                SourceType = clientData.SourceType,
                SourceId = clientData.SourceId,
            };
        }

        public static OutsourceProcessingTableRequest MapOutsourceProcessingPageFilterToDomain(this OutsourceProcessingPageFilterClientData clientData)
        {
            clientData ??= new OutsourceProcessingPageFilterClientData();
            var result = MapOutsourceProcessingFilterToDomain(clientData);

            result.Count = clientData.Count;
            result.Offset = clientData.Offset;

            return result;
        }

        public static OutsourceProcessingTableRequest MapOutsourceProcessingFilterToDomain(this OutsourceProcessingFilterClientData clientData)
        {
            clientData ??= new OutsourceProcessingFilterClientData();
            
            return new OutsourceProcessingTableRequest
            {
                Count = 0,
                Offset = 0,
                
                Query = MapQuery(clientData.Query),
                StartDate = clientData.StartDate,
                EndDate = clientData.EndDate,
                KontragentType = clientData.KontragentType,
                SourceType = clientData.SourceType,
                SourceId = clientData.SourceId,
                KontragentId = clientData.KontragentId,
                Direction = clientData.Direction,
                OperationTypes = MapOperationTypes(clientData.OperationType),
                BudgetaryType = MapBudgetaryPaymentType(clientData.BudgetaryType),
                ExtraBudgetaryType = MapExtraBudgetaryPaymentType(clientData.BudgetaryType),
                SumCondition = clientData.SumCondition,
                Sum = clientData.Sum,
                SumFrom = clientData.SumFrom,
                SumTo = clientData.SumTo
            };
        }

        public static MainMoneyTableRequest MapMainTableRequestToDomain(this MainMoneyTableRequestClientData clientData)
        {
            return new MainMoneyTableRequest
            {
                Count = clientData.Count,
                Offset = clientData.Offset,
                Query = MapQuery(clientData.Query),
                SortType = clientData.SortType,
                SortColumn = clientData.SortColumn,
                StartDate = clientData.StartDate,
                EndDate = clientData.EndDate,
                KontragentType = clientData.KontragentType,
                SourceType = clientData.SourceType,
                SourceId = clientData.SourceId,
                KontragentId = clientData.KontragentId,
                Direction = clientData.Direction,
                OperationTypes = MapOperationTypes(clientData.OperationType),
                BudgetaryType = MapBudgetaryPaymentType(clientData.BudgetaryType),
                ExtraBudgetaryType = MapExtraBudgetaryPaymentType(clientData.BudgetaryType),
                SumCondition = clientData.SumCondition,
                Sum = clientData.Sum,
                SumFrom = clientData.SumFrom,
                SumTo = clientData.SumTo,
                ProvideInTax = clientData.ProvideInTax,
                ClosingDocumentsCondition = clientData.ClosingDocumentsCondition,
                TaxationSystemType = clientData.TaxationSystemType,
                PatentId = clientData.PatentId,
                IsApproved = MapApprovedCondition(clientData.ApprovedCondition)
            };
        }

        private static string MapQuery(string query)
        {
            var result = query?.Trim();
            return result == "null" ? null : result;
        }

        private static OperationType[] MapOperationTypes(string operationTypes)
        {
            try
            {
                return operationTypes?.Split(',')
                    .Select(int.Parse)
                    .Where(x => Enum.IsDefined(typeof(OperationType), x))
                    .Cast<OperationType>()
                    .Where(x => x != OperationType.Default)
                    .ToArray() ?? Array.Empty<OperationType>();
            }
            catch (InvalidCastException) { }
            return Array.Empty<OperationType>();
        }

        private static SyntheticAccountCode? MapBudgetaryPaymentType(MoneyTableFilterBudgetaryType? budgetaryType)
        {
            switch (budgetaryType)
            {
                case MoneyTableFilterBudgetaryType.Ndfl:
                    return SyntheticAccountCode._68_01;
                case MoneyTableFilterBudgetaryType.Nds:
                    return SyntheticAccountCode._68_02;
                case MoneyTableFilterBudgetaryType.ProfitTax:
                    return SyntheticAccountCode._68_04;
                case MoneyTableFilterBudgetaryType.TransportTax:
                    return SyntheticAccountCode._68_07;
                case MoneyTableFilterBudgetaryType.PropertyTax:
                    return SyntheticAccountCode._68_08;
                case MoneyTableFilterBudgetaryType.MerchantTax:
                    return SyntheticAccountCode._68_09;
                case MoneyTableFilterBudgetaryType.Envd:
                    return SyntheticAccountCode._68_11;
                case MoneyTableFilterBudgetaryType.EnvdForUsn:
                    return SyntheticAccountCode._68_12;
                case MoneyTableFilterBudgetaryType.LandTax:
                    return SyntheticAccountCode._68_13;
                case MoneyTableFilterBudgetaryType.OtherTax:
                    return SyntheticAccountCode._68_10;
                case MoneyTableFilterBudgetaryType.FssFee:
                    return SyntheticAccountCode._69_01;
                case MoneyTableFilterBudgetaryType.InsuranceFee:
                    return SyntheticAccountCode._69_10;
                case MoneyTableFilterBudgetaryType.FssInjuryFee:
                    return SyntheticAccountCode._69_11;
                case MoneyTableFilterBudgetaryType.FomsFee:
                    return SyntheticAccountCode._69_03;
                case MoneyTableFilterBudgetaryType.PfrInsuranceFee:
                    return SyntheticAccountCode._69_02_01;
                case MoneyTableFilterBudgetaryType.PfrAccumulateFee:
                    return SyntheticAccountCode._69_02_02;
                default:
                    return null;
            }
        }

        private static MoneyTableExtraBudgetaryFilterType? MapExtraBudgetaryPaymentType(MoneyTableFilterBudgetaryType? budgetaryType)
        {
            switch (budgetaryType)
            {
                case MoneyTableFilterBudgetaryType.AllInsuranceFee:
                    return MoneyTableExtraBudgetaryFilterType.AllInsuranceFee;
                case MoneyTableFilterBudgetaryType.AllInsuranceFeeIP:
                    return MoneyTableExtraBudgetaryFilterType.AllInsuranceFeeIP;
                default:
                    return null;
            }
        }

        public static UnrecognizedMoneyTableResponseClientData MapUnrecognizedTableResponseToClient(this UnrecognizedMoneyTableResponse response)
        {
            return new UnrecognizedMoneyTableResponseClientData
            {
                TotalCount = response.TotalCount,
                Operations = response.Operations.Select(Map).ToList(),
            };
        }

        public static ImportedMoneyTableResponseClientData MapToClient(this ImportedMoneyTableResponse response)
        {
            return new ImportedMoneyTableResponseClientData
            {
                TotalCount = response.TotalCount,
                Operations = response.Operations.Select(Map).ToList(),
            };
        }
        
        public static OutsourceProcessingMoneyTableResponseClientData MapToClient(this OutsourceProcessingMoneyTableResponse response)
        {
            return new OutsourceProcessingMoneyTableResponseClientData
            {
                TotalCount = response.TotalCount,
                Operations = response.Operations.Select(Map).ToList(),
            };
        }
        
        private static OutsourceProcessingMoneyTableOperationClientData Map(this MoneyTableOperation operation)
        {
            return new OutsourceProcessingMoneyTableOperationClientData
            {
                DocumentBaseId = operation.DocumentBaseId,
                Date = operation.Date,
                Number = operation.Number,
                Direction = operation.Direction,
                KontragentName = operation.KontragentName,
                OperationType = operation.OperationType,
                PaidStatus = operation.PaidStatus,
                Sum = operation.Sum,
                Currency = GetCurrency(operation),
                Taxes = operation.Taxes.Select(Map).ToList(),
                Description = operation.Description,
                LinkedDocumentsCount = operation.LinkedDocumentsCount,
                PrimaryDocsStatus = operation.PrimaryDocStatus,
                UncoveredSum = operation.UncoveredSum,
                ImportRules = operation.ImportRules.Select(Map).ToArray(),
                CanDownload = operation.CanDownload,
                HasUnbindedSalaryChargePayments = operation.HasUnbindedSalaryChargePayments
            };
        }

        private static UnrecognizedMoneyTableOperationClientData Map(UnrecognizedMoneyTableOperation operation)
        {
            return new UnrecognizedMoneyTableOperationClientData
            {
                DocumentBaseId = operation.DocumentBaseId,
                Date = operation.Date,
                Direction = operation.Direction,
                KontragentName = operation.KontragentName,
                OperationType = operation.OperationType,
                OperationState = operation.OperationState,
                Sum = operation.Sum,
                Currency = operation.Currency,
                Description = operation.Description,
                BaseOperation = operation.BaseOperation?.Map(),
                ImportRules = operation.ImportRules.Select(Map).ToArray(),
                OutsourceImportRules = operation.OutsourceImportRules.Select(Map).ToArray(),
            };
        }

        private static ImportedMoneyTableOperationClientData Map(this ImportedMoneyTableOperation operation)
        {
            return new ImportedMoneyTableOperationClientData
            {
                DocumentBaseId = operation.DocumentBaseId,
                Date = operation.Date,
                Number = operation.Number,
                Direction = operation.Direction,
                KontragentName = operation.KontragentName,
                OperationType = operation.OperationType,
                PaidStatus = operation.PaidStatus,
                Sum = operation.Sum,
                Currency = GetCurrency(operation),
                Taxes = operation.Taxes.Select(Map).ToList(),
                Description = operation.Description,
                LinkedDocumentsCount = operation.LinkedDocumentsCount,
                PrimaryDocsStatus = operation.PrimaryDocStatus,
                UncoveredSum = operation.UncoveredSum,
                ImportRules = operation.ImportRules.Select(Map).ToArray(),
                CanDownload = operation.CanDownload,
                HasUnbindedSalaryChargePayments = operation.HasUnbindedSalaryChargePayments
            };
        }

        private static Currency GetCurrency(MoneyTableOperation operation)
        {
            return operation.SettlementType == SettlementAccountType.Default ? Currency.RUB : operation.Currency;
        }

        private static MainMoneyOperationClientData Map(MainMoneyTableOperation operation)
        {
            return new MainMoneyOperationClientData
            {
                Id = operation.Id,
                DocumentBaseId = operation.DocumentBaseId,
                Date = operation.Date,
                Number = operation.Number,
                Direction = operation.Direction,
                KontragentName = operation.KontragentName,
                OperationType = operation.OperationType,
                PaidStatus = operation.PaidStatus,
                Sum = operation.Sum,
                Currency = operation.Currency,
                Taxes = operation.Taxes.Select(Map).ToList(),
                PassThruPaymentState = operation.PassThruPaymentState?.Map(),
                Description = operation.Description,
                LinkedDocumentsCount = operation.LinkedDocumentsCount,
                HasUnbindedSalaryChargePayments = operation.HasUnbindedSalaryChargePayments,
                CanDownload = operation.CanDownload,
                PrimaryDocsStatus = operation.PrimaryDocStatus,
                UncoveredSum = operation.UncoveredSum,
                ImportRules = operation.ImportRules.Select(Map).ToArray(),
                CanSendToBank = operation.CanSendToBank,
                OutsourceImportRules = operation.OutsourceImportRules.Select(Map).ToArray(),
            };
        }

        private static AppliedImportRuleClientData Map(this MoneyTableOperationImportRule rule)
        {
            return new AppliedImportRuleClientData
            {
                Id = rule.Id,
                Name = rule.Name
            };
        }

        private static MoneyOperationClientData Map(this MoneyOperation operation)
        {
            return new MoneyOperationClientData
            {
                DocumentBaseId = operation.DocumentBaseId,
                Date = operation.Date,
                Number = operation.Number,
                Direction = operation.Direction,
                KontragentName = operation.KontragentName,
                OperationType = operation.OperationType,
                PaidStatus = operation.PaidStatus,
                Sum = operation.Sum,
                Currency = operation.Currency,
                Description = operation.Description,
            };
        }

        private static TaxSumRecClientData Map(this TaxSumRec operation)
        {
            return new TaxSumRecClientData
            {
                TaxType = operation.TaxType,
                Sum = operation.Sum
            };
        }
        
        private static PassThruPaymentStateClientData Map(this PassThruPaymentState state)
        {
            return new PassThruPaymentStateClientData
            {
                Message = state.Message,
                Status = state.Status,
                PartnerId = state.PartnerId
            };
        }

        public static MainMoneyTableResponseWithOperationsClientData Map(this MainMoneyMultiCurrencyTableResponse response)
        {
            return new MainMoneyTableResponseWithOperationsClientData
            {
                Summaries = response.Summaries.Select(MapMainTableSummaryToClient).ToArray(),
                BankBalance = MapMainTableSummaryToClient(response.BankBalance),
                Operations = response.Operations.Select(Map).ToArray(),
                TotalCount = response.Summaries.Sum(x => x.TotalCount)
            };
        }

        private static MainMoneyTableSummaryResponseClientData MapMainTableSummaryToClient(MainMoneyMultiCurrencyTableSummary summary)
        {
            return new MainMoneyTableSummaryResponseClientData
            {
                StartBalance = summary.StartBalance,
                EndBalance = summary.EndBalance,
                IncomingCount = summary.IncomingCount,
                IncomingBalance = summary.IncomingBalance,
                IncomingDate = summary.IncomingDate,
                OutgoingCount = summary.OutgoingCount,
                OutgoingBalance = summary.OutgoingBalance,
                OutgoingDate = summary.OutgoingDate,
                TotalCount = summary.TotalCount,
                //HasOperations = response.HasOperations,
                // note: костыль от Вани. Если ничего не изменится, то выпилить без жалости (в т.ч. из sql)
                HasOperations = true,
                Currency = summary.Currency
            };
        }

        private static MainMoneyTableBankBalanceResponseClientData MapMainTableSummaryToClient(MainMoneyTableBankBalance bankBalance)
        {
            if (bankBalance == null)
            {
                return null;
            }
            return new MainMoneyTableBankBalanceResponseClientData
            {
                SourceId = bankBalance.SourceId,
                StartBalance = bankBalance.StartBalance,
                EndBalance = bankBalance.EndBalance,
                IncomingBalance = bankBalance.IncomingBalance,
                //IncomingDate = bankBalance.IncomingDate,
                OutgoingBalance = bankBalance.OutgoingBalance,
                //OutgoingDate = bankBalance.OutgoingDate,
            };
        }

        private static bool? MapApprovedCondition(ApprovedCondition approvedCondition)
        {
            switch (approvedCondition)
            {
                case ApprovedCondition.No:
                    return false;
                case ApprovedCondition.Yes:
                    return true;
                default: // ApprovedCondition.NoMatter:
                    return null;
            }
        }
    }
}
