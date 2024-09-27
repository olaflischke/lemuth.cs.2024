using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace ChinookUiMvvmToolkit.Infrastructure;

public class FileLoggerProvider : ILoggerProvider
{
    private readonly string _filePath;
    private readonly ConcurrentDictionary<string, FileLogger> _loggers = new ConcurrentDictionary<string, FileLogger>();

    public FileLoggerProvider(string filePath)
    {
        _filePath = filePath;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return _loggers.GetOrAdd(categoryName, name => new FileLogger(_filePath));
    }

    public void Dispose()
    {
        _loggers.Clear();
    }
}
