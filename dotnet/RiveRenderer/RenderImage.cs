using System;

namespace RiveRenderer;

public sealed class RenderImage : IDisposable
{
    private readonly ImageHandleSafe _handle;
    private bool _disposed;

    internal RenderImage(ImageHandleSafe handle)
    {
        _handle = handle;
    }

    internal NativeImageHandle DangerousGetHandle() => new() { Handle = _handle.DangerousGetHandle() };

    public (uint Width, uint Height) Size
    {
        get
        {
            ThrowIfDisposed();
            var status = NativeMethods.Image.GetSize(DangerousGetHandle(), out var width, out var height);
            status.ThrowIfFailed("Failed to query image size.");
            return (width, height);
        }
    }

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
            throw new ObjectDisposedException(nameof(RenderImage));
        }
    }
}
