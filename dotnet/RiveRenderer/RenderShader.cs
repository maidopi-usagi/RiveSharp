using System;

namespace RiveRenderer;

public sealed class RenderShader : IDisposable
{
    private readonly ShaderHandleSafe _handle;
    private bool _disposed;

    internal RenderShader(ShaderHandleSafe handle)
    {
        _handle = handle;
    }

    internal NativeShaderHandle DangerousGetHandle() => new() { Handle = _handle.DangerousGetHandle() };

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
            throw new ObjectDisposedException(nameof(RenderShader));
        }
    }
}
