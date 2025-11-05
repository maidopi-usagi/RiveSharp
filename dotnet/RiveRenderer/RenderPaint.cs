using System;

namespace RiveRenderer;

public sealed class RenderPaint : IDisposable
{
    private readonly PaintHandleSafe _handle;
    private bool _disposed;

    internal RenderPaint(PaintHandleSafe handle)
    {
        _handle = handle;
    }

    internal NativePaintHandle DangerousGetHandle() => new() { Handle = _handle.DangerousGetHandle() };

    public void SetStyle(PaintStyle style)
    {
        ThrowIfDisposed();
        NativeMethods.Paint.SetStyle(DangerousGetHandle(), style)
            .ThrowIfFailed("Failed to set paint style.");
    }

    public void SetColor(uint rgba)
    {
        ThrowIfDisposed();
        NativeMethods.Paint.SetColor(DangerousGetHandle(), rgba)
            .ThrowIfFailed("Failed to set paint color.");
    }

    public void SetThickness(float thickness)
    {
        ThrowIfDisposed();
        NativeMethods.Paint.SetThickness(DangerousGetHandle(), thickness)
            .ThrowIfFailed("Failed to set paint thickness.");
    }

    public void SetJoin(StrokeJoin join)
    {
        ThrowIfDisposed();
        NativeMethods.Paint.SetJoin(DangerousGetHandle(), join)
            .ThrowIfFailed("Failed to set paint join.");
    }

    public void SetCap(StrokeCap cap)
    {
        ThrowIfDisposed();
        NativeMethods.Paint.SetCap(DangerousGetHandle(), cap)
            .ThrowIfFailed("Failed to set paint cap.");
    }

    public void SetFeather(float feather)
    {
        ThrowIfDisposed();
        NativeMethods.Paint.SetFeather(DangerousGetHandle(), feather)
            .ThrowIfFailed("Failed to set paint feather.");
    }

    public void SetBlendMode(BlendMode blendMode)
    {
        ThrowIfDisposed();
        NativeMethods.Paint.SetBlendMode(DangerousGetHandle(), blendMode)
            .ThrowIfFailed("Failed to set paint blend mode.");
    }

    public void SetShader(RenderShader shader)
    {
        ThrowIfDisposed();
        shader.ThrowIfDisposed();
        NativeMethods.Paint.SetShader(DangerousGetHandle(), shader.DangerousGetHandle())
            .ThrowIfFailed("Failed to set paint shader.");
    }

    public void ClearShader()
    {
        ThrowIfDisposed();
        NativeMethods.Paint.ClearShader(DangerousGetHandle())
            .ThrowIfFailed("Failed to clear paint shader.");
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
            throw new ObjectDisposedException(nameof(RenderPaint));
        }
    }
}
