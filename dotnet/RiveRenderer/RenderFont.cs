using System;

namespace RiveRenderer;

public sealed class RenderFont : IDisposable
{
    private readonly FontHandleSafe _handle;
    private bool _disposed;

    internal RenderFont(FontHandleSafe handle)
    {
        _handle = handle;
    }

    internal NativeFontHandle DangerousGetHandle() => new() { Handle = _handle.DangerousGetHandle() };

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _handle.Dispose();
    }

    internal void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(RenderFont));
        }
    }
}
