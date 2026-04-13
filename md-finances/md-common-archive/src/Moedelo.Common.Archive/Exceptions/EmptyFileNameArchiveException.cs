using System;

namespace Moedelo.Common.Archive.Exceptions;

public class EmptyFileNameArchiveException()
    : InvalidOperationException("Невозможно выполнить архивирование файла без имени.");