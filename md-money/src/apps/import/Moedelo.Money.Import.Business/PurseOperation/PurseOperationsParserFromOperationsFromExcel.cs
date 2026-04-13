using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Cells;
using Moedelo.Accounting.Domain.Shared.NdsRates;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Enums;
using Moedelo.Money.Import.Business.Helpers;
using Moedelo.Money.Import.Domain.Exceptions;
using Moedelo.Money.Import.Domain.Interfaces;
using Moedelo.Money.Import.Domain.Models.PurseOperation;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using TaxationSystemType = Moedelo.Money.Enums.TaxationSystemType;

namespace Moedelo.Money.Import.Business.PurseOperation
{
    [InjectAsSingleton(typeof(IPurseOperationsParserFromExcel))]
    public class PurseOperationsParserFromOperationsFromExcel : IPurseOperationsParserFromExcel
    {
        private static readonly string Date = "Дата".ToLower();
        private static readonly string Received = "Поступило".ToLower();
        private static readonly string ConsiderIn = "Учесть в".ToLower();
        private static readonly string NdsRate = "Ставка НДС".ToLower();
        private static readonly string SettlementAccount = "Номер расчетного счета".ToLower();
        private static readonly string TotalToBePaid = "Всего к оплате".ToLower();

        private readonly Dictionary<string, NdsType> ndsDictionary;
        
        public PurseOperationsParserFromOperationsFromExcel()
        {
            ndsDictionary = EnumHelper.EnumToList<NdsType>().ToDictionary(x => x.GetEnumDescription().Replace("%", "").ToLower(), x => x);
        }

        public (IList<PurseOperationFromExcel>, IList<string>) GetOperations(
            PurseOperationImportRequest request, 
            IList<TaxationSystemDto> taxationSystems,
            IList<PatentWithoutAdditionalDataDto> patents,
            DateTime? registrationDate)
        {
            var book = GetExcelFile(request.File);
            
            var sheets = book.Worksheets;
            var purseOperations = new List<PurseOperationFromExcel>();
            var purseErrors = new List<string>();

            foreach (var sheet in sheets)
            {
                var operationType = sheet.Name.ToLower() switch
                {
                    "поступление" => PurseOperationType.Income,
                    "удержание комиссии" => PurseOperationType.Comission,
                    "перевод на счет" => PurseOperationType.Transfer,
                    _ => PurseOperationType.Default
                };

                var cells = sheet.Cells;
                cells.DeleteBlankRows();
                var rows = cells.Rows;
                var indexes = GetIndexes(rows, operationType);

                var dataRows = new List<Row>();
                for (var i = indexes.FirstRow; i < (indexes.LastRow - indexes.FirstRow + 1); i++)
                {
                    dataRows.Add(rows[i]);
                }

                var (operations, errors) =
                    GetPurseOperationList(operationType, 
                        dataRows, 
                        indexes, 
                        taxationSystems, 
                        patents, 
                        registrationDate, 
                        request.currentYear);
                
                purseOperations.AddRange(operations);
                purseErrors.AddRange(errors);
            }
            
            return (purseOperations, purseErrors);
        }
        
        private static PurseExcelIndexes GetIndexes(RowCollection rows, PurseOperationType operationType)
        {
            var header = GetHeader(rows, operationType);

            if (header == null)
            {
                throw new FileValidationException($"{GetNameList(operationType)} Не найдены столбцы с данными для импорта.");
            }
            
            var lastRow = rows[rows.Count];
            if (lastRow == null)
            {
                throw new FileValidationException($"{GetNameList(operationType)} Нет последней строки");
            }
            
            var headerStringValues = GetCellsStringValues(header);

            PurseExcelIndexes indexes = null;
            switch (operationType)
            { 
                case PurseOperationType.Income:
                    indexes = new PurseExcelIndexes
                    {
                        Date = headerStringValues.FindIndex(i => i == Date),
                        Sum = headerStringValues.FindIndex(i => i == Received),
                        SNO = headerStringValues.FindIndex(i => i == ConsiderIn),
                        Nds = headerStringValues.FindIndex(i => i == NdsRate),
                        FirstRow = header.Index + 1,
                        LastRow = lastRow.Index
                    };
                    break;
                case PurseOperationType.Comission:
                    indexes = new PurseExcelIndexes
                    {
                        Date = headerStringValues.FindIndex(i => i == Date),
                        Sum = headerStringValues.FindIndex(i => i == TotalToBePaid),
                        SNO = headerStringValues.FindIndex(i => i == ConsiderIn),
                        Nds = -1,
                        FirstRow = header.Index + 1,
                        LastRow = lastRow.Index
                    };
                    break;
                case PurseOperationType.Transfer:
                    indexes = new PurseExcelIndexes
                    {
                        Date = headerStringValues.FindIndex(i => i == Date),
                        SettlementAccount = headerStringValues.FindIndex(i => i == SettlementAccount),
                        Sum = headerStringValues.FindIndex(i => i == TotalToBePaid),
                        Nds = -1,
                        FirstRow = header.Index + 1,
                        LastRow = lastRow.Index
                    };
                    break;
            }

            return indexes;
        }

        private static Row GetHeader(RowCollection rows, PurseOperationType operationType)
        {
            for (var i = 0; i < rows.Count; i++)
            {
                var endColumn = rows[i].LastDataCell?.Column ?? 0;
                if (endColumn >= 0)
                {
                    var cells = GetCellsStringValues(rows[i]);

                    switch (operationType)
                    {
                        case PurseOperationType.Income when (FieldValidation(cells, operationType, Date) &&
                                                            FieldValidation(cells, operationType, Received) &&
                                                            FieldValidation(cells, operationType, ConsiderIn)) ||
                                                            (FieldValidation(cells, operationType, Date) &&
                                                             FieldValidation(cells, operationType, Received) &&
                                                             FieldValidation(cells, operationType, ConsiderIn) &&
                                                             FieldValidation(cells, operationType, NdsRate)):
                            return rows[i];

                        case PurseOperationType.Transfer when FieldValidation(cells, operationType, Date) &&
                                                              FieldValidation(cells, operationType, SettlementAccount) &&
                                                              FieldValidation(cells, operationType, TotalToBePaid):
                            return rows[i];
                        case PurseOperationType.Comission when FieldValidation(cells, operationType, Date) &&
                                                               FieldValidation(cells, operationType, TotalToBePaid) &&
                                                               FieldValidation(cells, operationType, ConsiderIn):
                            return rows[i];
                    }
                }
            }

            return null;
        }

        private static bool FieldValidation(ICollection<string> cells, PurseOperationType operationType, string nameField)
        {
            return cells.Contains(nameField)
                ? true
                : throw new FileValidationException(
                    $"{GetNameList(operationType)} Не найден столбец [{nameField}] c данными.");
        }

        private static List<string> GetCellsStringValues(Row row)
        {
            var stringValues = new List<string>();
            var endColumn = row.LastDataCell?.Column ?? 0;

            for (var i = 0; i <= endColumn; i++)
            {
                stringValues.Add((row.GetCellOrNull(i)?.StringValue ?? "").Trim().ToLower());
            }
            return stringValues;
        }

        private (IList<PurseOperationFromExcel>, IList<string>) GetPurseOperationList(
            PurseOperationType operationType,
            List<Row> dataRows, 
            PurseExcelIndexes indexes, 
            IList<TaxationSystemDto> taxationSystems,
            IList<PatentWithoutAdditionalDataDto> patents,
            DateTime? registrationDate, 
            int? currentYear)
        {
            var purseData = new List<PurseOperationFromExcel>();
            var listErrors = new List<string>();
            var currentNameSheet = GetNameList(operationType);
            
            foreach (var row in dataRows)
            {
                var currentRowIndex = row.Index + 1;

                (var date, listErrors) = GetDate(indexes, registrationDate, row, listErrors, currentNameSheet,
                    currentRowIndex, currentYear);

                (var sum, listErrors) = GetSum(indexes, row, listErrors, currentNameSheet, currentRowIndex);

                (var sno, listErrors) = GetTaxationSystemType(operationType, indexes, taxationSystems, patents, row,
                    listErrors, currentNameSheet, currentRowIndex, date, currentYear);

                (var ndsType, listErrors) = GetNdsType(operationType, indexes, row, date, sno, listErrors,
                    currentNameSheet, currentRowIndex);

                var ndsSum = GetNdsSum(sum, ndsType);

                (var settlementAccount, listErrors) = GetSettlementAccount(operationType, indexes, row, date, sum,
                    listErrors, currentNameSheet, currentRowIndex);

                purseData.Add(new PurseOperationFromExcel
                {
                    Date = date,
                    Sum = sum,
                    SettlementAccount = settlementAccount,
                    TaxationSystemType = sno,
                    PurseOperationType = operationType,
                    IncludeNds = ndsType != null,
                    NdsSum = ndsSum,
                    NdsType = ndsType
                });
            }
            return (purseData, listErrors);
        }

        private static (string, List<string>) GetSettlementAccount(PurseOperationType operationType, PurseExcelIndexes indexes, Row row,
            DateTime date, decimal sum, List<string> listErrors, string currentNameSheet, int currentRowIndex)
        {
            if (operationType != PurseOperationType.Transfer) 
                return (string.Empty, listErrors);
            
            var settlementAccount = row[indexes.SettlementAccount].Value == null
                ? string.Empty
                : row[indexes.SettlementAccount].StringValue;

            if (string.IsNullOrEmpty(settlementAccount))
            {
                listErrors.Add(
                    $"{currentNameSheet}[Строка {currentRowIndex}] По операции 'Перевод на р/с' отсутствует р/с [Дата {date:dd.MM.yyyy}, Сумма {sum}]");
            }

            return (settlementAccount, listErrors);
        }

        private static (DateTime, List<string>) GetDate(PurseExcelIndexes indexes, DateTime? registrationDate, Row row, List<string> listErrors,
            string currentNameSheet, int currentRowIndex, int? currentYear)
        {
            var firmDateRegistration = registrationDate ?? new DateTime(2000, 01, 01);
            var isDate = DateTime.TryParse(row[indexes.Date]?.StringValue ?? string.Empty, out var date);
            // Операции не могут быть больше текущего дня, если только не заполнено тестовое поле currentYear
            var currentDate = currentYear == null ? DateTime.Today : new DateTime(currentYear.Value, DateTime.Today.Month, DateTime.Today.Day);
            if (!isDate || firmDateRegistration > date || date > currentDate)
            {
                listErrors.Add(
                    $"{currentNameSheet}[Строка {currentRowIndex}] Неправильная дата в колонке Дата [{date:dd.MM.yyyy}]. Дата должна быть не меньше {firmDateRegistration:dd.MM.yyyy} и не больше текущей даты.");
            }

            return (date, listErrors);
        }

        private static (decimal, List<string>) GetSum(PurseExcelIndexes indexes, Row row, List<string> listErrors, string currentNameSheet,
            int currentRowIndex)
        {
            var isNumSum = decimal.TryParse(row[indexes.Sum]?.StringValue ?? string.Empty, out var sum);

            if (!isNumSum || sum < 0.01m || sum > 999999999.99m)
            {
                listErrors.Add($"{currentNameSheet}[Строка {currentRowIndex}] Сумма {sum} должна быть в диапазоне от 0.01 до 999 999 999.99");
            }

            return (sum, listErrors);
        }
        
        private static decimal? GetNdsSum(decimal sum, NdsType? nds)
        {
            return nds == null ? null : NdsHelper.GetNdsFromSum(sum, nds);
        }

        private static (TaxationSystemType?, List<string>) GetTaxationSystemType(PurseOperationType operationType, PurseExcelIndexes indexes,
            IList<TaxationSystemDto> taxationSystems, IList<PatentWithoutAdditionalDataDto> patents, Row row, List<string> listErrors, string currentNameSheet, 
            int currentRowIndex, DateTime date, int? currentYear)
        {
            var sno = GetTaxationSystem(row[indexes.SNO].Value == null
                ? string.Empty
                : row[indexes.SNO].StringValue);
            ValidationSno(operationType, taxationSystems, patents, sno, listErrors, currentNameSheet, currentRowIndex, date, currentYear);
            return (sno, listErrors);
        }

        private (NdsType?, List<string>) GetNdsType(PurseOperationType operationType, PurseExcelIndexes indexes, Row row, DateTime date,
            TaxationSystemType? sno, List<string> listErrors, string currentNameSheet, int currentRowIndex)
        {
            if (operationType != PurseOperationType.Income || 
                sno == TaxationSystemType.Osno || 
                date < NdsRateStartDates.Nds5Or7StartDate)
            {
                return (null, listErrors);
            }

            if (indexes.Nds < 0)
            {
                listErrors.Add($"{currentNameSheet}[Строка {currentRowIndex}] Неверно указана ставка НДС или отсутствует.");
                return (null, listErrors);
            }
            
            var ndsString = row[indexes.Nds].Value == null ? string.Empty : row[indexes.Nds].StringValue.ToLower();

            if (string.IsNullOrEmpty(ndsString))
            {
                listErrors.Add($"{currentNameSheet}[Строка {currentRowIndex}] Неверно указана ставка НДС или отсутствует.");
                return (null, listErrors);
            }

            if (!ndsDictionary.TryGetValue(ndsString, out var ndsType))
            {
                listErrors.Add($"{currentNameSheet}[Строка {currentRowIndex}] Нераспознан указанный НДС.");
                return (null, listErrors);
            }

            if (ndsType is NdsType.Nds22 or NdsType.Nds22To122 
                && date < NdsRateStartDates.Nds22StartDate)
            {
                listErrors.Add($"{currentNameSheet}[Строка {currentRowIndex}] Ставку НДС 22% нельзя применять к операциям раньше {NdsRateStartDates.Nds22StartDate.Year} года.");
                return (null, listErrors);
            }
            
            return (ndsType, listErrors);
        }

        private static void ValidationSno(PurseOperationType operationType, IList<TaxationSystemDto> taxationSystems, IList<PatentWithoutAdditionalDataDto> patents,
            TaxationSystemType? sno, List<string> listErrors, string currentNameSheet, int currentRowIndex, DateTime date, int? currentYear)
        {
            if (operationType == PurseOperationType.Transfer) 
                return;
            
            switch (sno)
            {
                case null:
                    listErrors.Add($"{currentNameSheet}[Строка {currentRowIndex}] Отсутствует система налогообложения.");
                    break;

                case TaxationSystemType.Patent:
                {
                    var patentTrue = patents.FirstOrDefault(x =>
                        x.StartDate <= date && date <= x.EndDate && x.IsStopped == false);

                    if (patentTrue == null)
                    {
                        listErrors.Add($"{currentNameSheet}[Строка {currentRowIndex}] Неверно указана система налогообложения.");
                    }

                    break;
                }

                default:
                {
                    var snoTrue = taxationSystems.FirstOrDefault(x =>
                        x.StartYear <= date.Year && date.Year <= (x.EndYear ?? currentYear ?? DateTime.Now.Year));

                    if (snoTrue == null || sno.Value != (TaxationSystemType)snoTrue.ToTaxationSystemType())
                    {
                        listErrors.Add($"{currentNameSheet}[Строка {currentRowIndex}] Неверно указана система налогообложения.");
                    }

                    break;
                }
            }
        }

        private static TaxationSystemType? GetTaxationSystem(string sno)
        {
            return sno.ToUpper() switch
            {
                "УСН" => TaxationSystemType.Usn,
                "ПСН" => TaxationSystemType.Patent,
                "ОСНО" => TaxationSystemType.Osno,
                "ЕНВД" => TaxationSystemType.Envd,
                _ => null
            };
        }
        
        private static Workbook GetExcelFile(Stream file)
        {
            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            memoryStream.ToArray();
            memoryStream.Position = 0;

            var workbook = new Workbook(memoryStream);
            return workbook;
        }
        
        private static string GetNameList(PurseOperationType purseOperationType)
        {
            return purseOperationType switch
            {
                PurseOperationType.Income => "Поступление: ",
                PurseOperationType.Transfer => "Перевод на счет: ",
                PurseOperationType.Comission => "Удержание комиссии: ",
                _ => string.Empty
            };
        }
    }
}