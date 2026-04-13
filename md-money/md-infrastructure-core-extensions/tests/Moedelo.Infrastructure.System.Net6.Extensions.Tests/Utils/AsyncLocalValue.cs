using System;
using System.Threading;

namespace Moedelo.Infrastructure.System.Net6.Extensions.Tests.Utils;

internal sealed class AsyncLocalValue<TRawValue>
{
    private readonly AsyncLocal<Holder?> current = new ();

    public TRawValue? Value => current.Value != null ? current.Value.Value : default;

    public IDisposable SetValue(TRawValue value)
    {
        ClearValue();
        
        current.Value = new Holder
        {
            Value = value
        };

        return new HolderAutoClear(this);
    }
    
    internal void ClearValue()
    {
        var holder = current.Value;

        if (holder != null)
        {
            holder.Value = default;
        }
    }

    private class HolderAutoClear : IDisposable
    {
        private readonly AsyncLocalValue<TRawValue> value;

        public HolderAutoClear(AsyncLocalValue<TRawValue> value)
        {
            this.value = value;
        }

        public void Dispose()
        {
            value.ClearValue();
        }
    }

    private class Holder
    {
        internal TRawValue? Value { get; set; }
    }
}
