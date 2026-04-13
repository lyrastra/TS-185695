using System.Collections.Generic;
using NUnit.Framework;
using Moedelo.BankIntegrations.Converter1C.Business;
using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.Models.Movement;

namespace Moedelo.BankIntegrations.Converter1C.Tests
{
    [TestFixture]
    public class GetDocumentSectionFrom1CTests
    {
        private const string OurAccount = "40702810123450000123";
        private const string CounterpartyAccount = "40701810987650000456";

        // Вспомогательный метод для упрощения
        private static List<DocumentSection> ParseAndSetType(string data)
        {
            var docs = GetDocumentSectionFrom1C.GetDocumentSection(data);
            var list = new List<DocumentSection>(docs);
            var settlement = GetDocumentSectionFrom1C.GetStandartSettlementLength(OurAccount);
            foreach (var doc in list)
            {
                GetDocumentSectionFrom1C.SetOperationType(doc, settlement);
            }
            return list;
        }

        [Test]
        public void EmptyFile_ReturnsEmptyList()
        {
            string data = "";
            var result = ParseAndSetType(data);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ValidHeader_WithBalances_NoDocuments_ReturnsEmptyList()
        {
            string data = @"1CClientBankExchange
ВерсияФормата=1.02
Кодировка=Windows
Отправитель=Банк
Получатель=moedelo.org
ДатаСоздания=02.12.2025
ВремяСоздания=10:00:00
ДатаНачала=01.03.2025
ДатаКонца=01.03.2025
РасчСчет=40702810123450000123

СекцияРасчСчет
ДатаНачала=01.03.2025
ДатаКонца=01.03.2025
РасчСчет=40702810123450000123
НачальныйОстаток=50000.00
ВсегоПоступило=0.00
ВсегоСписано=0.00
КонечныйОстаток=50000.00
КонецРасчСчет
КонецФайла";

            var result = ParseAndSetType(data);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void OneIncomeOperation_ParsesCorrectly()
        {
            string data = @"1CClientBankExchange
ВерсияФормата=1.02
Кодировка=Windows
РасчСчет=40702810123450000123

СекцияРасчСчет
РасчСчет=40702810123450000123
НачальныйОстаток=0
ВсегоПоступило=15000.50
ВсегоСписано=0
КонечныйОстаток=15000.50
КонецРасчСчет

СекцияДокумент=ПлатежноеПоручение
Номер=IN-001
Дата=01.03.2025
Сумма=15000.50
Плательщик=ООО Поставщик
ПлательщикСчет=40701810987650000456
ПлательщикИНН=7712345678
ПлательщикБИК=044525225
ПлательщикБанк1=Сбербанк
ДатаСписано=
Получатель=ИП Иванов
ПолучательСчет=40702810123450000123
ПолучательИНН=772345678901
ПолучательБИК=044525555
ПолучательБанк1=ВТБ
ДатаПоступило=01.03.2025
НазначениеПлатежа=Оплата за товар
КонецДокумента
КонецФайла";

            var result = ParseAndSetType(data);
            Assert.That(result, Has.Count.EqualTo(1));
            var doc = result[0];
            Assert.Multiple(() =>
            {
                Assert.That(doc.OperationType, Is.EqualTo(OperationType.IncomeOperation));
                Assert.That(doc.Summa, Is.EqualTo(15000.50m));
                Assert.That(doc.ContractorAccount, Is.EqualTo(OurAccount.Substring(0, 20)));
                Assert.That(doc.PayerAccount, Is.EqualTo(CounterpartyAccount.Substring(0, 20)));
            });
        }

        [Test]
        public void MultipleOperations_ParsesAll()
        {
            string data = @"1CClientBankExchange
ВерсияФормата=1.02
РасчСчет=40702810123450000123

СекцияРасчСчет
РасчСчет=40702810123450000123
НачальныйОстаток=100000
ВсегоПоступило=20000
ВсегоСписано=15000
КонечныйОстаток=105000
КонецРасчСчет

СекцияДокумент=ПлатежноеПоручение
Номер=IN-001
Дата=01.03.2025
Сумма=20000.00
Плательщик=Клиент А
ПлательщикСчет=40701810987650000456
Получатель=ИП Иванов
ПолучательСчет=40702810123450000123
НазначениеПлатежа=Оплата
КонецДокумента

СекцияДокумент=ПлатежноеПоручение
Номер=OUT-001
Дата=02.03.2025
Сумма=15000.00
Плательщик=ИП Иванов
ПлательщикСчет=40702810123450000123
Получатель=Поставщик Б
ПолучательСчет=40701810112233444555
НазначениеПлатежа=Оплата поставщику
КонецДокумента
КонецФайла";

            var result = ParseAndSetType(data);
            Assert.That(result, Has.Count.EqualTo(2));

            Assert.That(result[0].OperationType, Is.EqualTo(OperationType.IncomeOperation));
            Assert.That(result[1].OperationType, Is.EqualTo(OperationType.OutcomeOperation));
        }

        [Test]
        public void SelfTransfer_OperationTypeUnknown()
        {
            // Перевод между своими счетами: и плательщик, и получатель — наш счёт
            string data = @"1CClientBankExchange
ВерсияФормата=1.02
РасчСчет=40702810123450000123

СекцияРасчСчет
РасчСчет=40702810123450000123
НачальныйОстаток=50000
ВсегоПоступило=10000
ВсегоСписано=10000
КонечныйОстаток=50000
КонецРасчСчет

СекцияДокумент=ПлатежноеПоручение
Номер=SELF-001
Дата=01.03.2025
Сумма=10000.00
Плательщик=ИП Иванов
ПлательщикСчет=40702810123450000123
Получатель=ИП Иванов
ПолучательСчет=40702810123450000123
НазначениеПлатежа=Перевод между счетами
КонецДокумента
КонецФайла";

            var result = ParseAndSetType(data);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].OperationType, Is.EqualTo(OperationType.OutcomeOperation));
        }

        [Test]
        public void BudgetOperation_ParsesCorrectly()
        {
            string data = @"1CClientBankExchange
ВерсияФормата=1.02
РасчСчет=40702810123450000123

СекцияРасчСчет
РасчСчет=40702810123450000123
НачальныйОстаток=0
ВсегоПоступило=50000
ВсегоСписано=0
КонечныйОстаток=50000
КонецРасчСчет

СекцияДокумент=ПлатежноеПоручение
Номер=BUD-001
Дата=01.03.2025
Сумма=50000.00
Плательщик=ФНС России
ПлательщикСчет=40101810200000010001
ПлательщикИНН=7700000000
ПлательщикБИК=044525001
ПлательщикКПП=770001001
Получатель=ИП Иванов
ПолучательСчет=40702810123450000123
ПолучательИНН=772345678901
ПолучательБИК=044525555
ПолучательКПП=
ДатаПоступило=01.03.2025
НазначениеПлатежа=Налог на прибыль
СтатусСоставителя=01
ПоказательКБК=18210101011011000120
ОКАТО=45286555
ПоказательОснования=01
ПоказательПериода=2025
ПоказательНомера=12345
ПоказательДаты=01.02.2025
ПоказательТипа=1
КонецДокумента
КонецФайла";

            var result = ParseAndSetType(data);
            Assert.That(result, Has.Count.EqualTo(1));
            var doc = result[0];
            Assert.Multiple(() =>
            {
                Assert.That(doc.OperationType, Is.EqualTo(OperationType.IncomeOperation));
                Assert.That(doc.IndicatorKbk, Is.EqualTo("18210101011011000120"));
                Assert.That(doc.Okato, Is.EqualTo("45286555"));
                Assert.That(doc.PayerStatus, Is.EqualTo("01"));
                Assert.That(doc.PaymentFoundation, Is.EqualTo("01"));
                Assert.That(doc.Period, Is.EqualTo("2025"));
                Assert.That(doc.PaymentNumber, Is.EqualTo("12345"));
                Assert.That(doc.PaymentDate, Is.EqualTo("01.02.2025"));
                Assert.That(doc.PaymentType, Is.EqualTo("1"));
            });
        }

        [Test]
        public void OperationsWithoutBalancesSection_ParsesCorrectly()
        {
            string data = @"1CClientBankExchange
ВерсияФормата=1.02
Кодировка=Windows
РасчСчет=40702810123450000123

СекцияДокумент=ПлатежноеПоручение
Номер=OUT-002
Дата=03.03.2025
Сумма=5000.00
Плательщик=ИП Иванов
ПлательщикСчет=40702810123450000123
Получатель=Арендодатель
ПолучательСчет=40701810198765432109
НазначениеПлатежа=Аренда офиса
КонецДокумента
КонецФайла";

            var result = ParseAndSetType(data);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].OperationType, Is.EqualTo(OperationType.OutcomeOperation));
        }
    }
}