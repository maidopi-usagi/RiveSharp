using System;

namespace RiveRenderer;

public readonly struct RendererVulkanFeatures
{
    public RendererVulkanFeatures(
        uint apiVersion,
        bool independentBlend = false,
        bool fillModeNonSolid = false,
        bool fragmentStoresAndAtomics = false,
        bool shaderClipDistance = false,
        bool rasterizationOrderColorAttachmentAccess = false,
        bool fragmentShaderPixelInterlock = false,
        bool portabilitySubset = false)
    {
        ApiVersion = apiVersion;
        IndependentBlend = independentBlend;
        FillModeNonSolid = fillModeNonSolid;
        FragmentStoresAndAtomics = fragmentStoresAndAtomics;
        ShaderClipDistance = shaderClipDistance;
        RasterizationOrderColorAttachmentAccess = rasterizationOrderColorAttachmentAccess;
        FragmentShaderPixelInterlock = fragmentShaderPixelInterlock;
        PortabilitySubset = portabilitySubset;
    }

    public uint ApiVersion { get; }
    public bool IndependentBlend { get; }
    public bool FillModeNonSolid { get; }
    public bool FragmentStoresAndAtomics { get; }
    public bool ShaderClipDistance { get; }
    public bool RasterizationOrderColorAttachmentAccess { get; }
    public bool FragmentShaderPixelInterlock { get; }
    public bool PortabilitySubset { get; }

    internal NativeVulkanFeatures ToNative() =>
        new()
        {
            ApiVersion = ApiVersion,
            IndependentBlend = IndependentBlend ? (byte)1 : (byte)0,
            FillModeNonSolid = FillModeNonSolid ? (byte)1 : (byte)0,
            FragmentStoresAndAtomics = FragmentStoresAndAtomics ? (byte)1 : (byte)0,
            ShaderClipDistance = ShaderClipDistance ? (byte)1 : (byte)0,
            RasterizationOrderColorAttachmentAccess =
                RasterizationOrderColorAttachmentAccess ? (byte)1 : (byte)0,
            FragmentShaderPixelInterlock = FragmentShaderPixelInterlock ? (byte)1 : (byte)0,
            PortabilitySubset = PortabilitySubset ? (byte)1 : (byte)0,
        };
}

public readonly struct RendererVulkanDeviceOptions
{
    private readonly uint _presentQueueFamilyIndex;
    private readonly bool _hasPresentQueueFamilyIndex;

    public RendererVulkanDeviceOptions(
        nint instance,
        nint physicalDevice,
        nint device,
        nint graphicsQueue,
        uint graphicsQueueFamilyIndex,
        RendererVulkanFeatures features,
        nint presentQueue = 0,
        uint? presentQueueFamilyIndex = null,
        nint getInstanceProcAddr = 0,
        nint allocatorCallbacks = 0)
    {
        Instance = instance;
        PhysicalDevice = physicalDevice;
        Device = device;
        GraphicsQueue = graphicsQueue;
        GraphicsQueueFamilyIndex = graphicsQueueFamilyIndex;
        Features = features;
        PresentQueue = presentQueue;
        _presentQueueFamilyIndex = presentQueueFamilyIndex ?? graphicsQueueFamilyIndex;
        _hasPresentQueueFamilyIndex = presentQueueFamilyIndex.HasValue;
        GetInstanceProcAddr = getInstanceProcAddr;
        AllocatorCallbacks = allocatorCallbacks;
    }

    public nint Instance { get; }
    public nint PhysicalDevice { get; }
    public nint Device { get; }
    public nint GraphicsQueue { get; }
    public uint GraphicsQueueFamilyIndex { get; }
    public RendererVulkanFeatures Features { get; }
    public nint PresentQueue { get; }
    public nint GetInstanceProcAddr { get; }
    public nint AllocatorCallbacks { get; }
    public uint PresentQueueFamilyIndex =>
        _hasPresentQueueFamilyIndex ? _presentQueueFamilyIndex : GraphicsQueueFamilyIndex;

    internal NativeDeviceCreateInfoVulkan ToNative()
    {
        if (Instance == 0)
        {
            throw new ArgumentException("Vulkan instance handle must be provided.", nameof(Instance));
        }

        if (PhysicalDevice == 0)
        {
            throw new ArgumentException("Vulkan physical device handle must be provided.", nameof(PhysicalDevice));
        }

        if (Device == 0)
        {
            throw new ArgumentException("Vulkan logical device handle must be provided.", nameof(Device));
        }

        if (GraphicsQueue == 0)
        {
            throw new ArgumentException("Vulkan graphics queue handle must be provided.", nameof(GraphicsQueue));
        }

        var presentQueue = PresentQueue != 0 ? PresentQueue : GraphicsQueue;
        var presentQueueFamilyIndex = PresentQueue != 0
            ? PresentQueueFamilyIndex
            : GraphicsQueueFamilyIndex;

        return new NativeDeviceCreateInfoVulkan
        {
            Instance = Instance,
            PhysicalDevice = PhysicalDevice,
            Device = Device,
            Features = Features.ToNative(),
            GetInstanceProcAddr = GetInstanceProcAddr,
            GraphicsQueue = GraphicsQueue,
            GraphicsQueueFamilyIndex = GraphicsQueueFamilyIndex,
            PresentQueue = presentQueue,
            PresentQueueFamilyIndex = presentQueueFamilyIndex,
            AllocatorCallbacks = AllocatorCallbacks,
        };
    }
}
