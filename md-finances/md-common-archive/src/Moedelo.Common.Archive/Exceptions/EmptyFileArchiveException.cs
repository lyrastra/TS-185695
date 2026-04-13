using System;

namespace Moedelo.Common.Archive.Exceptions;

public class EmptyFileArchiveException()
    : InvalidOperationException("Невозможно выполнить архивирование пустого файла.");