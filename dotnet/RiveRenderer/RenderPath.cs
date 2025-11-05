using System;

namespace RiveRenderer;

public sealed class RenderPath : IDisposable
{
    private readonly PathHandleSafe _handle;
    private bool _disposed;

    internal RenderPath(PathHandleSafe handle, FillRule fillRule)
    {
        _handle = handle;
        FillRule = fillRule;
    }

    public FillRule FillRule { get; private set; }

    internal NativePathHandle DangerousGetHandle() => new() { Handle = _handle.DangerousGetHandle() };

    public void Rewind()
    {
        ThrowIfDisposed();
        var status = NativeMethods.Path.Rewind(DangerousGetHandle());
        status.ThrowIfFailed("Failed to rewind path.");
    }

    public void SetFillRule(FillRule fillRule)
    {
        ThrowIfDisposed();
        var status = NativeMethods.Path.SetFillRule(DangerousGetHandle(), fillRule);
        status.ThrowIfFailed("Failed to set path fill rule.");
        FillRule = fillRule;
    }

    public void MoveTo(float x, float y)
    {
        ThrowIfDisposed();
        NativeMethods.Path.MoveTo(DangerousGetHandle(), x, y).ThrowIfFailed("Failed to moveTo.");
    }

    public void LineTo(float x, float y)
    {
        ThrowIfDisposed();
        NativeMethods.Path.LineTo(DangerousGetHandle(), x, y).ThrowIfFailed("Failed to lineTo.");
    }

    public void CubicTo(float ox, float oy, float ix, float iy, float x, float y)
    {
        ThrowIfDisposed();
        NativeMethods.Path.CubicTo(DangerousGetHandle(), ox, oy, ix, iy, x, y)
            .ThrowIfFailed("Failed to cubicTo.");
    }

    public void Close()
    {
        ThrowIfDisposed();
        NativeMethods.Path.Close(DangerousGetHandle()).ThrowIfFailed("Failed to close path.");
    }

    public void AddPath(RenderPath other, in Mat2D transform)
    {
        ThrowIfDisposed();
        other.ThrowIfDisposed();
        NativeMethods.Path.AddPath(DangerousGetHandle(), other.DangerousGetHandle(), in transform)
            .ThrowIfFailed("Failed to add path.");
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
            throw new ObjectDisposedException(nameof(RenderPath));
        }
    }
}
