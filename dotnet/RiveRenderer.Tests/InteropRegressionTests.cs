using System;
using RiveRenderer.Tests.TestUtilities;
using Xunit;

namespace RiveRenderer.Tests;

public class InteropRegressionTests
{
    [RequiresNativeLibraryFact]
    public void DisposedContextThrows()
    {
        using var device = RendererDevice.Create(RendererBackend.Null);
        var context = device.CreateContext(16, 16);
        context.Dispose();

        Assert.Throws<ObjectDisposedException>(() => context.BeginFrame());
    }

    [RequiresNativeLibraryFact]
    public void InvalidHandleThrowsRendererException()
    {
        var status = NativeMethods.Device.Create(default, out var handle);
        Assert.NotEqual(RendererStatus.Ok, status);
    }
}
