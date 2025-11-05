namespace RiveRenderer;

public readonly record struct AdapterInfo(
    RendererBackend Backend,
    ushort VendorId,
    ushort DeviceId,
    ushort SubsystemId,
    ushort Revision,
    ulong DedicatedVideoMemory,
    ulong SharedSystemMemory,
    RendererFeatureFlags Flags,
    string Name)
{
    internal static AdapterInfo FromNative(in AdapterDescription native)
    {
        return new AdapterInfo(
            native.Backend,
            native.VendorId,
            native.DeviceId,
            native.SubsystemId,
            native.Revision,
            native.DedicatedVideoMemory,
            native.SharedSystemMemory,
            (RendererFeatureFlags)native.Flags,
            native.GetName());
    }
}
