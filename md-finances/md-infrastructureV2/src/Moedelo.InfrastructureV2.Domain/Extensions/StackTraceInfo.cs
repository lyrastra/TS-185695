using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Moedelo.InfrastructureV2.Domain.Extensions;

internal class StackTraceInfo
{
    public List<StackTraceFrame> Frames { get; } = new List<StackTraceFrame>();

    public StackTraceInfo(Exception ex)
    {
        StackTrace st = new StackTrace(ex, true);

        for (var i = st.FrameCount - 1; i >= 0; i--)
        {
            StackFrame frame = st.GetFrame(i);
            AddFrame(frame.GetFileName(), frame.GetMethod().ToString(), frame.GetFileLineNumber());
        }
    }

    private void AddFrame(string file, string method, int line)
    {
        if (string.IsNullOrEmpty(file) && line == 0)
        {
            return;
        }

        var frame = new StackTraceFrame {File = file, Method = method, Line = line};
        Frames.Add(frame);
    }
}
