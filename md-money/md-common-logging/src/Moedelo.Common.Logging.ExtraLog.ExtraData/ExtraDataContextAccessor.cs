using System.Threading;

namespace Moedelo.Common.Logging.ExtraLog.ExtraData
{
    internal static class ExtraDataContextAccessor
    {
        private static readonly AsyncLocal<ExtraDataContextHolder> Current = new AsyncLocal<ExtraDataContextHolder>();

        public static object ExtraDataContext
        {
            get => Current.Value?.Context;
            set => Current.Value = Current.Value?.Reset(value) ?? new ExtraDataContextHolder(value);
        }
        
        private class ExtraDataContextHolder
        {
            public ExtraDataContextHolder(object context)
            {
                Context = context;
            }
            
            public object Context { get; private set; }

            public ExtraDataContextHolder Reset(object context)
            {
                Context = context;

                return this;
            }
        }
    }
}