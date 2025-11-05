namespace RiveRenderer;

public readonly record struct RendererCapabilities(
    RendererBackend Backend,
    RendererFeatureFlags FeatureFlags,
    ulong MaxBufferSize,
    uint MaxTextureDimension,
    uint MaxTextureArrayLayers,
    float MaxSamplerAnisotropy,
    bool SupportsHdr,
    bool SupportsPresentation)
{
    internal static RendererCapabilities FromNative(in NativeCapabilities native)
    {
        return new RendererCapabilities(
            native.Backend,
            native.FeatureFlags,
            native.MaxBufferSize,
            native.MaxTextureDimension,
            native.MaxTextureArrayLayers,
            native.MaxSamplerAnisotropy,
            native.SupportsHdr != 0,
            native.SupportsPresentation != 0);
    }
}
