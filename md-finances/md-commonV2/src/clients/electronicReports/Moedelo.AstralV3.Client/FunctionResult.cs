using System;

namespace Moedelo.AstralV3.Client
{
    public class FunctionResult<TResultType>
    {

        /// <summary> Результат работы функции </summary>
        public SimpleFunctionResult Result { get; private set; }

        /// <summary>Значение рузультата </summary>
        public TResultType ResultValue { get; private set; }

        /// <summary> Ошибка работы </summary>
        public Exception ExecuteException { get; private set; }

        /// <summary> Консутрктор </summary>
        /// <param name="functionResult">Результат работы функции</param>
        public FunctionResult(SimpleFunctionResult functionResult)
        {
            Result = functionResult;
        }

        /// <summary> Консутрктор </summary>
        /// <param name="functionResult">Результат работы функции</param>
        /// <param name="resultValue">Значение</param>
        public FunctionResult(SimpleFunctionResult functionResult, TResultType resultValue) : this(functionResult, resultValue, null) { }

        /// <summary> Консутрктор </summary>
        /// <param name="functionResult">Результат работы функции</param>
        /// <param name="resultValue">Значение</param>
        /// <param name="exception">Последняя ошибка</param>
        public FunctionResult(SimpleFunctionResult functionResult, TResultType resultValue, Exception exception)
        {
            Result = functionResult;
            ResultValue = resultValue;
            ExecuteException = exception;
        }

        /// <summary> Закрытый конструктор </summary>
        private FunctionResult() { }

        public delegate FunctionResult<T> Function<T>();

        /// <summary> Выполнить метод </summary>
        /// <typeparam name="TResultType">Тип возвращаемого значения</typeparam>
        /// <returns>Результат выполнения</returns>
        public static FunctionResult<TResultType> ExecuteSafeMethod(Function<TResultType> function)
        {
            try
            {
                return function();
            }
            catch (Exception e)
            {
                return new FunctionResult<TResultType> { ExecuteException = e, Result = SimpleFunctionResult.Exception };
            }
        }
    }

    public enum SimpleFunctionResult
    {

        /// <summary> Во время работы функции возник Exception </summary>
        Exception = -1,

        /// <summary> Отрицательный результат </summary>
        False = 0,

        /// <summary> Всё успешно </summary>
        True = 1,

        /// <summary> Ошибка во входных параметрах </summary>
        InputParameterError = 2,

    }
}