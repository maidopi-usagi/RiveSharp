using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace RiveRenderer;

internal static partial class NativeMethods
{
    internal static partial class Device
    {
        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_enumerate_adapters")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static unsafe partial RendererStatus EnumerateAdapters(
            AdapterDescription* adapters,
            nuint capacity,
            out nuint count);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_device_create")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus Create(
            in NativeDeviceCreateInfo info,
            out NativeDeviceHandle device);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_device_retain")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus Retain(NativeDeviceHandle device);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_device_release")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus Release(NativeDeviceHandle device);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_device_capabilities")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus GetCapabilities(
            NativeDeviceHandle device,
            out NativeCapabilities capabilities);
    }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
internal struct NativeDeviceCreateInfo
{
    public RendererBackend Backend;
    private byte _backendPadding;
    public ushort AdapterIndex;
    public RendererDeviceFlags Flags;
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
internal struct NativeCapabilities
{
    public RendererBackend Backend;
    private byte _backendPadding;
    private ushort _reserved;
    public RendererFeatureFlags FeatureFlags;
    public ulong MaxBufferSize;
    public uint MaxTextureDimension;
    public uint MaxTextureArrayLayers;
    public float MaxSamplerAnisotropy;
    public byte SupportsHdr;
    public byte SupportsPresentation;
    private unsafe fixed byte _reservedPadding[6];
    private unsafe fixed byte _reservedTail[4];
}
