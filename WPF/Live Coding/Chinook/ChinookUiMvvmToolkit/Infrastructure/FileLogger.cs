using Microsoft.Extensions.Logging;
using System.IO;

namespace ChinookUiMvvmToolkit.Infrastructure;

public class FileLogger : ILogger
{
    private readonly string _filePath;
    private readonly object _lock = new object();

    public FileLogger(string filePath)
    {
        this._filePath = filePath;
    }

    public IDisposable BeginScope<TState>(TState state) => null;

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        var message = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} [{logLevel}] {formatter(state, exception)}";

        lock (_lock)
        {
            File.AppendAllText(_filePath, message + Environment.NewLine);
        }

    }
}
