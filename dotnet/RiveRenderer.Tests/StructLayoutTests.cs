using System.Runtime.InteropServices;
using Xunit;

namespace RiveRenderer.Tests;

public class StructLayoutTests
{
    [Fact]
    public void AdapterDescription_SizeMatchesNative()
    {
        Assert.Equal(304, Marshal.SizeOf<AdapterDescription>());
    }

    [Fact]
    public void Capabilities_SizeMatchesNative()
    {
        Assert.Equal(40, Marshal.SizeOf<NativeCapabilities>());
    }

    [Fact]
    public void DeviceCreateInfo_SizeMatchesNative()
    {
        Assert.Equal(8, Marshal.SizeOf<NativeDeviceCreateInfo>());
    }

    [Fact]
    public void FrameOptions_SizeMatchesNative()
    {
        Assert.Equal(16, Marshal.SizeOf<FrameOptions>());
    }

    [Fact]
    public void TextStyle_SizeMatchesNative()
    {
        Assert.Equal(24, Marshal.SizeOf<TextStyleOptions>());
    }
}
