using System;

namespace RiveRenderer;

public sealed class RendererException : Exception
{
    public RendererStatus Status { get; }

    internal RendererException(RendererStatus status, string? message = null)
        : base(message ?? $"Renderer operation failed with status {status}.")
    {
        Status = status;
    }
}
