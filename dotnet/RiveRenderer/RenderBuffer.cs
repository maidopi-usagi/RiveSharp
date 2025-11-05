using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace RiveRenderer;

public sealed class RenderBuffer : IDisposable
{
    private readonly BufferHandleSafe _handle;
    private readonly BufferType _type;
    private readonly BufferFlags _flags;
    private readonly nuint _size;
    private bool _disposed;

    internal RenderBuffer(BufferHandleSafe handle, BufferType type, BufferFlags flags, nuint size)
    {
        _handle = handle;
        _type = type;
        _flags = flags;
        _size = size;
    }

    public BufferType Type => _type;
    public BufferFlags Flags => _flags;
    public nuint Size => _size;

    internal NativeBufferHandle DangerousGetHandle() => new() { Handle = _handle.DangerousGetHandle() };

    public void Upload(ReadOnlySpan<byte> data, nuint offset = 0)
    {
        ThrowIfDisposed();
        if (offset > _size)
        {
            throw new ArgumentOutOfRangeException(nameof(offset));
        }

        if ((nuint)data.Length > _size - offset)
        {
            throw new ArgumentOutOfRangeException(nameof(data), "Data span exceeds the target buffer size.");
        }

        if (data.IsEmpty)
        {
            return;
        }

        unsafe
        {
            fixed (byte* ptr = data)
            {
                NativeMethods.Buffer.Upload(DangerousGetHandle(), ptr, (nuint)data.Length, offset)
                    .ThrowIfFailed("Failed to upload data to buffer.");
            }
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
            throw new ObjectDisposedException(nameof(RenderBuffer));
        }
    }
}
