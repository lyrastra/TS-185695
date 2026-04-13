using System;
using System.Text;

namespace Moedelo.Common.Utils.Numbers
{
    /// <summary>
    /// Класс для записи денежных сумм прописью: "тысяча рублей 50 копеек".
    /// Валюта.Рубли.Пропись (123.45); // "сто двадцать три рубля 45 копеек"
    ///
    /// source: https://github.com/moedelo/md-infrastructure/blob/ace0d601fa5e08a0b42a3e2b4a43f2315893f464/src/infra/Moedelo.Infrastructure.Utils/Money/ConvertMoneyToString.cs#L10
    /// </summary>
    public static class Summa
    {
        /// <summary>
        /// Записывает пропись суммы в заданной валюте в <paramref name="result"/> строчными буквами.
        /// </summary>
        public static StringBuilder ToWords(decimal summa, Currency currency, StringBuilder result, bool copInNumber)
        {
            decimal celaya = Math.Floor(summa);
            var drobnaya = (uint)(Math.Round(summa * 100, 0) % 100);
            Number.ToWords(celaya, currency.ОсновнаяЕдиница, result);
            return AddCopecks(drobnaya, currency, result, copInNumber);
        }
        
        // Возвращает пропись заданной суммы строчными буквами.
        public static string ToWords(decimal n, Currency currency)
        {
            return Number.ApplyCaps(ToWords(n, currency, new StringBuilder(), true), CapitalLetter.No);
        }

        // Возвращает пропись заданной суммы.
        public static string ToWords(decimal n, Currency currency, CapitalLetter zaglavnue, bool copInNumber)
        {
            return Number.ApplyCaps(ToWords(n, currency, new StringBuilder(), copInNumber), zaglavnue);
        }

        // копейки сторокой
        private static StringBuilder AddCopecks(uint drobnaya, Currency currency, StringBuilder result, bool copInnumber)
        {
            result.Append(' ');
            if (copInnumber)
            {
                result.Append(drobnaya.ToString("00"));
                result.Append(' ');
                result.Append(Number.Accord(currency.ДробнаяЕдиница, drobnaya));
            }
            else
            {
                Number.ToWords(drobnaya, currency.ДробнаяЕдиница, result);
            }
            return result;
        }
    }

    /// <summary>
    /// Класс для преобразования чисел в пропись на русском языке.
    ///
    /// source: https://github.com/moedelo/md-infrastructure/blob/ace0d601fa5e08a0b42a3e2b4a43f2315893f464/src/infra/Moedelo.Infrastructure.Utils/Money/ConvertMoneyToString.cs#L109
    /// </summary>
    /// <example>
    /// Число.Пропись (1, РодЧисло.Мужской); // "один"
    /// Число.Пропись (2, РодЧисло.Женский); // "две"
    /// Число.Пропись (21, РодЧисло.Средний); // "двадцать одно"
    /// </example>
    /// <example>
    /// Число.Пропись (5, new ЕдиницаИзмерения (
    ///  РодЧисло.Мужской, "метр", "метра", "метров"), sb); // "пять метров"
    /// </example>
    public static class Number
    {
        // Получить пропись числа с согласованной единицей измерения.
        public static StringBuilder ToWords(decimal number, IUnitMeasure еи, StringBuilder result)
        {
            string error = CheckNumber(number);
            if (error != null)
            {
                throw new ArgumentException(error, nameof(number));
            }
            
            // Целочисленные версии работают в разы быстрее, чем decimal.
            if (number <= uint.MaxValue)
            {
                ToWords((uint)number, еи, result);
            }
            else if (number <= ulong.MaxValue)
            {
                ToWords((ulong)number, еи, result);
            }
            else
            {
                MyStringBuilder mySb = new MyStringBuilder(result);
                decimal div1000 = Math.Floor(number / 1000);
                ToWordsSeniorMoneyClasses(div1000, 0, mySb);
                ToWordsMoneyClass((uint)(number - div1000 * 1000), еи, mySb);
            }
            return result;
        }

        /// <summary>
        /// Получить пропись числа с согласованной единицей измерения.
        /// </summary>
        /// <param name="число"> 
        /// Число должно быть целым, неотрицательным, не большим <see cref="MaxDouble"/>. 
        /// </param>
        /// <param name="еи"></param>
        /// <param name="result"> Сюда записывается результат. </param>
        /// <exception cref="ArgumentException">
        /// Если число меньше нуля, не целое или больше <see cref="MaxDouble"/>. 
        /// </exception>
        /// <returns> <paramref name="result"/> </returns>
        /// <remarks>
        /// double по умолчанию преобразуется к double, поэтому нет перегрузки для double.
        /// В результате ошибок округления возможно расхождение цифр прописи и
        /// строки, выдаваемой double.ToString ("R"), начиная с 17 значащей цифры.
        /// </remarks>
        public static StringBuilder ToWords(double number, IUnitMeasure еи, StringBuilder result)
        {
            string error = CheckNumber(number);
            if (error != null) throw new ArgumentException(error, "число");
            if (number <= uint.MaxValue)
            {
                ToWords((uint)number, еи, result);
            }
            else if (number <= ulong.MaxValue)
            {
                // Пропись с ulong выполняется в среднем в 2 раза быстрее.
                ToWords((ulong)number, еи, result);
            }
            else
            {
                MyStringBuilder mySb = new MyStringBuilder(result);
                double div1000 = Math.Floor(number / 1000);
                ToWordsSeniorMoneyClasses(div1000, 0, mySb);
                ToWordsMoneyClass((uint)(number - div1000 * 1000), еи, mySb);
            }
            return result;
        }
        
        /// <summary>
        /// Получить пропись числа с согласованной единицей измерения.
        /// </summary>
        /// <returns> <paramref name="result"/> </returns>
        public static StringBuilder ToWords(ulong number, IUnitMeasure еи, StringBuilder result)
        {
            if (number <= uint.MaxValue)
            {
                ToWords((uint)number, еи, result);
            }
            else
            {
                MyStringBuilder mySb = new MyStringBuilder(result);
                ulong div1000 = number / 1000;
                ToWordsSeniorMoneyClasses(div1000, 0, mySb);
                ToWordsMoneyClass((uint)(number - div1000 * 1000), еи, mySb);
            }
            return result;
        }
        
        /// <summary>
        /// Получить пропись числа с согласованной единицей измерения.
        /// </summary>
        /// <returns> <paramref name="result"/> </returns>
        public static StringBuilder ToWords(uint number, IUnitMeasure еи, StringBuilder result)
        {
            MyStringBuilder mySb = new MyStringBuilder(result);
            if (number == 0)
            {
                mySb.Append("ноль");
                mySb.Append(еи.РодМнож);
            }
            else
            {
                uint div1000 = number / 1000;
                ToWordsSeniorMoneyClasses(div1000, 0, mySb);
                ToWordsMoneyClass(number - div1000 * 1000, еи, mySb);
            }
            return result;
        }

        /// <summary>Пропись старших классов
        /// Записывает в <paramref name="sb"/> пропись числа, начиная с самого 
        /// старшего класса до класса с номером <paramref name="номерКласса"/>.
        /// <param name="номерКласса">0 = класс тысяч, 1 = миллионов и т.д.</param>
        /// <remarks>
        /// В методе применена рекурсия, чтобы обеспечить запись в StringBuilder 
        /// в нужном порядке - от старших классов к младшим.
        /// </remarks>
        private static void ToWordsSeniorMoneyClasses(decimal number, int numMoneyClass, MyStringBuilder sb)
        {
            if (number == 0) return; // конец рекурсии
            // Записать в StringBuilder пропись старших классов.
            decimal div1000 = Math.Floor(number / 1000);
            ToWordsSeniorMoneyClasses(div1000, numMoneyClass + 1, sb);
            uint числоДо999 = (uint)(number - div1000 * 1000);
            if (числоДо999 == 0) return;
            ToWordsMoneyClass(числоДо999, Классы[numMoneyClass], sb);
        }

        private static void ToWordsSeniorMoneyClasses(double number, int numMoneyClass, MyStringBuilder sb)
        {
            if (number == 0) return; // конец рекурсии
            // Записать в StringBuilder пропись старших классов.
            double div1000 = Math.Floor(number / 1000);
            ToWordsSeniorMoneyClasses(div1000, numMoneyClass + 1, sb);
            uint числоДо999 = (uint)(number - div1000 * 1000);
            if (числоДо999 == 0) return;
            ToWordsMoneyClass(числоДо999, Классы[numMoneyClass], sb);
        }

        private static void ToWordsSeniorMoneyClasses(ulong number, int numMoneyClass, MyStringBuilder sb)
        {
            if (number == 0) return; // конец рекурсии
            // Записать в StringBuilder пропись старших классов.
            ulong div1000 = number / 1000;
            ToWordsSeniorMoneyClasses(div1000, numMoneyClass + 1, sb);
            uint числоДо999 = (uint)(number - div1000 * 1000);
            if (числоДо999 == 0) return;
            ToWordsMoneyClass(числоДо999, Классы[numMoneyClass], sb);
        }

        private static void ToWordsSeniorMoneyClasses(uint number, int numMoneyClass, MyStringBuilder sb)
        {
            if (number == 0) return; // конец рекурсии
            // Записать в StringBuilder пропись старших классов.
            uint div1000 = number / 1000;
            ToWordsSeniorMoneyClasses(div1000, numMoneyClass + 1, sb);
            uint числоДо999 = number - div1000 * 1000;
            if (числоДо999 == 0) return;
            ToWordsMoneyClass(числоДо999, Классы[numMoneyClass], sb);
        }

        #region ПрописьКласса

        /// <summary>
        /// Формирует запись класса с названием, например,
        /// "125 тысяч", "15 рублей".
        /// Для 0 записывает только единицу измерения в род.мн.
        /// </summary>
        private static void ToWordsMoneyClass(uint numberUntil999, IUnitMeasure moneyClass, MyStringBuilder sb)
        {
            uint numEdinic = numberUntil999 % 10;
            uint numDesuatkov = (numberUntil999 / 10) % 10;
            uint numSoten = (numberUntil999 / 100) % 10;
            sb.Append(Handreds[numSoten]);
            if ((numberUntil999 % 100) != 0)
            {
                Tens[numDesuatkov].ToWords(sb, numEdinic, moneyClass.РодЧисло);
            }
            // Добавить название класса в нужной форме.
            sb.Append(Accord(moneyClass, numberUntil999));
        }

        #endregion

        #region ПроверитьЧисло

        /// <summary>
        /// Проверяет, подходит ли число для передачи методу 
        /// <see cref="Пропись(decimal,IЕдиницаИзмерения,StringBuilder)"/>.
        /// </summary>
        /// <returns>
        /// Описание нарушенного ограничения или null.
        /// </returns>
        private static string CheckNumber(decimal number)
        {
            if (number < 0)
                return "Число должно быть больше или равно нулю.";
            if (number != decimal.Floor(number))
                return "Число не должно содержать дробной части.";
            return null;
        }

        /// <summary>
        /// Проверяет, подходит ли число для передачи методу 
        /// <see cref="Пропись(double,IЕдиницаИзмерения,StringBuilder)"/>.
        /// </summary>
        /// <returns>
        /// Описание нарушенного ограничения или null.
        /// </returns>
        private static string CheckNumber(double number)
        {
            if (number < 0)
                return "Число должно быть больше или равно нулю.";
            if (number != Math.Floor(number))
                return "Число не должно содержать дробной части.";
            if (number > MaxDouble)
            {
                return "Число должно быть не больше " + MaxDouble + ".";
            }
            return null;
        }

        #endregion

        #region Согласовать

        /// <summary>
        /// Согласовать 
        /// название единицы измерения с числом.
        /// Например, согласование единицы (рубль, рубля, рублей) 
        /// с числом 23 даёт "рубля", а с числом 25 - "рублей".
        /// </summary>
        public static string Accord(IUnitMeasure unitMeasure, uint number)
        {
            uint numEdinic = number % 10;
            uint numDesyatkov = (number / 10) % 10;
            if (numDesyatkov == 1) return unitMeasure.РодМнож;
            switch (numEdinic)
            {
                case 1: return unitMeasure.ИменЕдин;
                case 2:
                case 3:
                case 4: return unitMeasure.РодЕдин;
                default: return unitMeasure.РодМнож;
            }
        }

        #endregion

        static string ToWordNumbers(uint cifra, GenderNumber gender)
        {
            return Cifru[cifra].ToWords(gender);
        }

        abstract class Cifra
        {
            public abstract string ToWords(GenderNumber gender);
        }

        class ChangingOnGenders : Cifra, IChagingOnGender
        {
            public ChangingOnGenders(
                string man,
                string fem,
                string average,
                string multitude)
            {
                this.man = man;
                this.fem = fem;
                this.average = average;
                this.multitude = multitude;
            }


            public ChangingOnGenders(
                string singles,
                string manyes)
                : this(singles, singles, singles, manyes)
            {
            }

            private readonly string man;
            private readonly string fem;
            private readonly string average;
            private readonly string multitude;



            #region IИзменяетсяПоРодам Members

            public string Man => this.man;
            public string Fem => this.fem;
            public string Average => this.average;
            public string Multitude => this.multitude;

            #endregion


            public override string ToWords(GenderNumber род)
            {
                return род.ПолучитьФорму(this);
            }
        }


        // ЦифраНеизменяющаясяПоРодам
        class NumberNotChagingGender : Cifra
        {
            public NumberNotChagingGender(string toWords)
            {
                this.toWords = toWords;
            }
            private readonly string toWords;
            public override string ToWords(GenderNumber gender)
            {
                return this.toWords;
            }
        }

        private static readonly Cifra[] Cifru = new Cifra[]
        {
            null,
            new ChangingOnGenders ("один", "одна", "одно", "одни"),
            new ChangingOnGenders ("два", "две", "два", "двое"),
            new ChangingOnGenders ("три", "трое"),
            new ChangingOnGenders ("четыре", "четверо"),
            new NumberNotChagingGender ("пять"),
            new NumberNotChagingGender ("шесть"),
            new NumberNotChagingGender ("семь"),
            new NumberNotChagingGender ("восемь"),
            new NumberNotChagingGender ("девять"),
        };

        static readonly Ten[] Tens = new Ten[]
        {
            new FirstTen (),
            new SecondTen (),
            new OrdinaryTen ("двадцать"),
            new OrdinaryTen ("тридцать"),
            new OrdinaryTen ("сорок"),
            new OrdinaryTen ("пятьдесят"),
            new OrdinaryTen ("шестьдесят"),
            new OrdinaryTen ("семьдесят"),
            new OrdinaryTen ("восемьдесят"),
            new OrdinaryTen ("девяносто")
        };

        abstract class Ten
        {
            public abstract void ToWords(MyStringBuilder sb, uint числоЕдиниц, GenderNumber род);
        }

        class FirstTen : Ten
        {
            public override void ToWords(MyStringBuilder sb, uint числоЕдиниц, GenderNumber род)
            {
                sb.Append(ToWordNumbers(числоЕдиниц, род));
            }
        }

        class SecondTen : Ten
        {
            // ПрописьНаДцать
            static readonly string[] ToWordsOnDcat = new[]
            {
                "десять",
                "одиннадцать",
                "двенадцать",
                "тринадцать",
                "четырнадцать",
                "пятнадцать",
                "шестнадцать",
                "семнадцать",
                "восемнадцать",
                "девятнадцать"
            };

            public override void ToWords(MyStringBuilder sb, uint числоЕдиниц, GenderNumber gender)
            {
                sb.Append(ToWordsOnDcat[числоЕдиниц]);
            }
        }

        class OrdinaryTen : Ten
        {
            public OrdinaryTen(string nameTen)
            {
                this.nameTen = nameTen;
            }

            private readonly string nameTen;

            public override void ToWords(MyStringBuilder sb, uint числоЕдиниц, GenderNumber gender)
            {
                sb.Append(this.nameTen);
                if (числоЕдиниц == 0)
                {
                    // После "двадцать", "тридцать" и т.д. не пишут "ноль" (единиц)
                }
                else
                {
                    sb.Append(ToWordNumbers(числоЕдиниц, gender));
                }
            }
        }

        static readonly string[] Handreds = new string[]
        {
            null,
            "сто",
            "двести",
            "триста",
            "четыреста",
            "пятьсот",
            "шестьсот",
            "семьсот",
            "восемьсот",
            "девятьсот"
        };

        class ClassMoneyHundred : IUnitMeasure
        {
            public string ИменЕдин { get { return "тысяча"; } }
            public string РодЕдин { get { return "тысячи"; } }
            public string РодМнож { get { return "тысяч"; } }
            public GenderNumber РодЧисло { get { return GenderNumber.Женский; } }
        }

        class ClassMoney : IUnitMeasure
        {
            readonly string startingForm;
            public ClassMoney(string staringForm)
            {
                this.startingForm = staringForm;
            }
            public string ИменЕдин { get { return this.startingForm; } }
            public string РодЕдин { get { return this.startingForm + "а"; } }
            public string РодМнож { get { return this.startingForm + "ов"; } }
            public GenderNumber РодЧисло { get { return GenderNumber.Мужской; } }
        }

        /// <summary>
        /// Класс - группа из 3 цифр.  Есть классы единиц, тысяч, миллионов и т.д.
        /// </summary>
        static readonly IUnitMeasure[] Классы = new IUnitMeasure[]
        {
            new ClassMoneyHundred (),
            new ClassMoney ("миллион"),
            new ClassMoney ("миллиард"),
            new ClassMoney ("триллион"),
            new ClassMoney ("квадриллион"),
            new ClassMoney ("квинтиллион"),
            new ClassMoney ("секстиллион"),
            new ClassMoney ("септиллион"),
            new ClassMoney ("октиллион"),
            // Это количество классов покрывает весь диапазон типа decimal.
        };

        /// <summary>
        /// Максимальное число типа double, представимое в виде прописи.
        /// </summary>
        /// <remarks>
        /// Рассчитывается исходя из количества определённых классов.
        /// Если добавить ещё классы, оно будет автоматически увеличено.
        /// </remarks>
        private static double MaxDouble
        {
            get
            {
                if (maxDouble == 0)
                {
                    maxDouble = CalcMaxDouble();
                }
                return maxDouble;
            }
        }

        private static double maxDouble = 0;

        private static double CalcMaxDouble()
        {
            double max = Math.Pow(1000, Классы.Length + 1);
            double d = 1;
            while (max - d == max)
            {
                d *= 2;
            }
            return max - d;
        }

        #region Вспомогательные классы

        /// <summary>
        /// Вспомогательный класс, аналогичный <see cref="StringBuilder"/>.
        /// Между вызовами <see cref="MyStringBuilder.Append"/> вставляет пробелы.
        /// </summary>
        private class MyStringBuilder
        {
            public MyStringBuilder(StringBuilder sb)
            {
                this.sb = sb;
            }
            readonly StringBuilder sb;
            bool insertSpace = false;

            /// <summary>
            /// Добавляет слово <paramref name="s"/>,
            /// вставляя перед ним пробел, если нужно.
            /// </summary>
            public void Append(string s)
            {
                if (string.IsNullOrEmpty(s)) return;
                if (this.insertSpace)
                {
                    this.sb.Append(' ');
                }
                else
                {
                    this.insertSpace = true;
                }
                this.sb.Append(s);
            }

            public override string ToString()
            {
                return sb.ToString();
            }
        }

        #endregion

        #region Перегрузки метода ToWords(Пропись), возвращающие string

        /// <summary>
        /// Возвращает пропись числа строчными буквами.
        /// </summary>
        public static string ToWords(decimal number, IUnitMeasure еи)
        {
            return ToWords(number, еи, CapitalLetter.No);
        }

        /// <summary>
        /// Возвращает пропись числа.
        /// </summary>
        public static string ToWords(decimal number, IUnitMeasure еи, CapitalLetter capitalLetter)
        {
            return ApplyCaps(ToWords(number, еи, new StringBuilder()), capitalLetter);
        }

        /// <summary>
        /// Возвращает пропись числа строчными буквами.
        /// </summary>
        public static string ToWords(double number, IUnitMeasure еи)
        {
            return ToWords(number, еи, CapitalLetter.No);
        }

        /// <summary>
        /// Возвращает пропись числа.
        /// </summary>
        public static string ToWords(double number, IUnitMeasure еи, CapitalLetter capitalLetter)
        {
            return ApplyCaps(ToWords(number, еи, new StringBuilder()), capitalLetter);
        }

        /// <summary>
        /// Возвращает пропись числа строчными буквами.
        /// </summary>
        public static string ToWords(ulong number, IUnitMeasure еи)
        {
            return ToWords(number, еи, CapitalLetter.No);
        }

        /// <summary>
        /// Возвращает пропись числа.
        /// </summary>
        public static string ToWords(ulong number, IUnitMeasure еи, CapitalLetter capitalLetter)
        {
            return ApplyCaps(ToWords(number, еи, new StringBuilder()), capitalLetter);
        }

        /// <summary>
        /// Возвращает пропись числа строчными буквами.
        /// </summary>
        public static string ToWords(uint number, IUnitMeasure еи)
        {
            return ToWords(number, еи, CapitalLetter.No);
        }

        /// <summary>
        /// Возвращает пропись числа.
        /// </summary>
        public static string ToWords(uint number, IUnitMeasure еи, CapitalLetter capitalLetter)
        {
            return ApplyCaps(ToWords(number, еи, new StringBuilder()), capitalLetter);
        }
        internal static string ApplyCaps(StringBuilder sb, CapitalLetter capitalLetter)
        {
            capitalLetter.Применить(sb);
            return sb.ToString();
        }
        #endregion
    }

    /// <summary>
    /// Стратегия расстановки заглавных букв.
    /// </summary>
    public abstract class CapitalLetter
    {
        /// <summary>
        /// Применить стратегию к <paramref name="sb"/>.
        /// </summary>
        public abstract void Применить(StringBuilder sb);

        private class _ALL : CapitalLetter
        {
            public override void Применить(StringBuilder sb)
            {
                for (int i = 0; i < sb.Length; ++i)
                {
                    sb[i] = char.ToUpperInvariant(sb[i]);
                }
            }
        }

        private class _No : CapitalLetter
        {
            public override void Применить(StringBuilder sb)
            {
            }
        }

        private class _First : CapitalLetter
        {
            public override void Применить(StringBuilder sb)
            {
                sb[0] = char.ToUpperInvariant(sb[0]);
            }
        }
        public static readonly CapitalLetter ALL = new _ALL();
        public static readonly CapitalLetter No = new _No();
        public static readonly CapitalLetter First = new _First();
    }

    /// <summary>
    /// Описывает тип валюты как совокупность двух единиц измерения - основной и дробной.
    /// Содержит несколько предопределённых валют - рубли, доллары, евро.
    /// </summary>
    /// <remarks>
    /// Предполагается, что основная единица равна 100 дробным. 
    /// </remarks>
    public class Currency
    {
        /// <summary> </summary>
        public Currency(IUnitMeasure основная, IUnitMeasure дробная)
        {
            this.основная = основная;
            this.дробная = дробная;
        }
        readonly IUnitMeasure основная;
        readonly IUnitMeasure дробная;

        /// <summary>
        /// Основная единица измерения валюты - рубли, доллары, евро и т.д.
        /// </summary>
        public IUnitMeasure ОсновнаяЕдиница => this.основная;

        /// <summary>
        /// Дробная единица измерения валюты - копейки, центы, евроценты и т.д.
        /// </summary>
        public IUnitMeasure ДробнаяЕдиница => this.дробная;

        public static readonly Currency Rub = new Currency(
            new UnitMeasure(GenderNumber.Мужской, "рубль", "рубля", "рублей"),
            new UnitMeasure(GenderNumber.Женский, "копейка", "копейки", "копеек"));

        public static readonly Currency Dollar = new Currency(
            new UnitMeasure(GenderNumber.Мужской, "доллар США", "доллара США", "долларов США"),
            new UnitMeasure(GenderNumber.Мужской, "цент", "цента", "центов"));

        public static readonly Currency Euro = new Currency(
            new UnitMeasure(GenderNumber.Мужской, "евро", "евро", "евро"),
            new UnitMeasure(GenderNumber.Мужской, "цент", "цента", "центов"));

        /// <summary>
        /// Возвращает пропись суммы строчными буквами.
        /// </summary>
        public string ToWords(decimal summa)
        {
            return Summa.ToWords(summa, this);
        }

        /// <summary>
        /// Возвращает пропись суммы.
        /// </summary>
        public string ToWords(decimal summa, CapitalLetter capitalLetter, bool copeikiInNumber)
        {
            return Summa.ToWords(summa, this, capitalLetter, copeikiInNumber);
        }
    }

    /// <summary>
    /// Класс, хранящий падежные формы единицы измерения в явном виде.
    /// </summary>
    public class UnitMeasure : IUnitMeasure
    {
        /// <summary> </summary>
        public UnitMeasure(GenderNumber родЧисло, string именЕдин, string родЕдин, string родМнож)
        {
            this.родЧисло = родЧисло;
            this.именЕдин = именЕдин;
            this.родЕдин = родЕдин;
            this.родМнож = родМнож;
        }

        readonly GenderNumber родЧисло;
        readonly string именЕдин;
        readonly string родЕдин;
        readonly string родМнож;

        string IUnitMeasure.ИменЕдин => this.именЕдин;

        string IUnitMeasure.РодЕдин => this.родЕдин;

        string IUnitMeasure.РодМнож => this.родМнож;

        GenderNumber IUnitMeasure.РодЧисло => this.родЧисло;
    }

    #region РодЧисло

    /// <summary>
    /// Указывает род и число.
    /// Может передаваться в качестве параметра "единица измерения" метода 
    /// <see cref="Число.Пропись(decimal,IЕдиницаИзмерения,StringBuilder)"/>.
    /// Управляет родом и числом числительных один и два.
    /// </summary>
    /// <example>
    /// Число.Пропись (2, РодЧисло.Мужской); // "два"
    /// Число.Пропись (2, РодЧисло.Женский); // "две"
    /// Число.Пропись (21, РодЧисло.Средний); // "двадцать одно"
    /// </example>
    public abstract class GenderNumber : IUnitMeasure
    {

        internal abstract string ПолучитьФорму(IChagingOnGender слово);


        private class _Мужской : GenderNumber
        {
            internal override string ПолучитьФорму(IChagingOnGender слово)
            {
                return слово.Man;
            }
        }


        private class _Женский : GenderNumber
        {
            internal override string ПолучитьФорму(IChagingOnGender слово)
            {
                return слово.Fem;
            }
        }


        private class _Средний : GenderNumber
        {
            internal override string ПолучитьФорму(IChagingOnGender слово)
            {
                return слово.Average;
            }
        }


        private class _Множественное : GenderNumber
        {
            internal override string ПолучитьФорму(IChagingOnGender слово)
            {
                return слово.Multitude;
            }
        }


        public static readonly GenderNumber Мужской = new _Мужской();

        public static readonly GenderNumber Женский = new _Женский();

        public static readonly GenderNumber Средний = new _Средний();

        public static readonly GenderNumber Множественное = new _Множественное();


        GenderNumber IUnitMeasure.РодЧисло => this;

        string IUnitMeasure.ИменЕдин => null;

        string IUnitMeasure.РодЕдин => null;

        string IUnitMeasure.РодМнож => null;
    }


    internal interface IChagingOnGender
    {
        string Man { get; }
        string Fem { get; }
        string Average { get; }
        string Multitude { get; }
    }

    #endregion

    #region ЕдиницаИзмерения

    /// <summary>
    /// Представляет единицу измерения (например, метр, рубль)
    /// и содержит всю необходимую информацию для согласования
    /// этой единицы с числом, а именно - три падежно-числовых формы
    /// и грамматический род / число.
    /// </summary>
    public interface IUnitMeasure
    {
        /// <summary>
        /// Форма именительного падежа единственного числа.
        /// Согласуется с числительным "один":
        /// одна тысяча, один миллион, один рубль, одни сутки и т.д.
        /// </summary>
        string ИменЕдин { get; }
        /// <summary>
        /// Форма родительного падежа единственного числа.
        /// Согласуется с числительными "один, два, три, четыре":
        /// две тысячи, два миллиона, два рубля, двое суток и т.д.
        /// </summary>
        string РодЕдин { get; }

        /// <summary>
        /// Форма родительного падежа множественного числа.
        /// Согласуется с числительным "ноль, пять, шесть, семь" и др:
        /// пять тысяч, пять миллионов, пять рублей, пять суток и т.д.
        /// </summary>
        string РодМнож { get; }

        /// <summary>
        /// Род и число единицы измерения.
        /// </summary>
        GenderNumber РодЧисло { get; }
    }

    #endregion
}