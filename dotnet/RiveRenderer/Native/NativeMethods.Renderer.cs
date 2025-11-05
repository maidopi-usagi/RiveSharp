using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace RiveRenderer;

internal static partial class NativeMethods
{
    internal static partial class Renderer
    {
        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_renderer_create")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus Create(
            NativeContextHandle context,
            out NativeRendererHandle renderer);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_renderer_retain")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus Retain(NativeRendererHandle renderer);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_renderer_release")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus Release(NativeRendererHandle renderer);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_renderer_save")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus Save(NativeRendererHandle renderer);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_renderer_restore")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus Restore(NativeRendererHandle renderer);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_renderer_transform")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus Transform(
            NativeRendererHandle renderer,
            in Mat2D transform);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_renderer_draw_path")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus DrawPath(
            NativeRendererHandle renderer,
            NativePathHandle path,
            NativePaintHandle paint);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_renderer_clip_path")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus ClipPath(
            NativeRendererHandle renderer,
            NativePathHandle path);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_renderer_draw_image")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static unsafe partial RendererStatus DrawImage(
            NativeRendererHandle renderer,
            NativeImageHandle image,
            ImageSampler* sampler,
            BlendMode blendMode,
            float opacity);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_renderer_draw_image_mesh")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static unsafe partial RendererStatus DrawImageMesh(
            NativeRendererHandle renderer,
            NativeImageHandle image,
            ImageSampler* sampler,
            NativeBufferHandle vertices,
            NativeBufferHandle uvs,
            NativeBufferHandle indices,
            uint vertexCount,
            uint indexCount,
            BlendMode blendMode,
            float opacity);
    }
}
