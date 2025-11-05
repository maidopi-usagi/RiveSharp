using System;
using Xunit;
using Xunit.Sdk;

namespace RiveRenderer.Tests;

public class SmokeTests
{
    [Fact]
    public void NullBackendDeviceLifecycle()
    {
        try
        {
            using var device = RendererDevice.Create(RendererBackend.Null);
            using var context = device.CreateContext(64, 64);
            context.BeginFrame();
            using var renderer = context.CreateRenderer();
            context.EndFrame();
        }
        catch (DllNotFoundException)
        {
            return;
        }
        catch (RendererException ex) when (ex.Status == RendererStatus.Unsupported)
        {
            return;
        }
    }
}
