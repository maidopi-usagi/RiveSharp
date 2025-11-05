using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace RiveRenderer;

internal static partial class NativeMethods
{
    internal static partial class Context
    {
        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_context_create")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus Create(
            NativeDeviceHandle device,
            uint width,
            uint height,
            out NativeContextHandle context);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_context_retain")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus Retain(NativeContextHandle context);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_context_release")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus Release(NativeContextHandle context);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_context_get_size")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus GetSize(
            NativeContextHandle context,
            out uint width,
            out uint height);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_context_resize")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus Resize(
            NativeContextHandle context,
            uint width,
            uint height);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_context_begin_frame")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus BeginFrame(
            NativeContextHandle context,
            in FrameOptions options);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_context_end_frame")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus EndFrame(NativeContextHandle context);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_context_submit")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus Submit(NativeContextHandle context);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_context_copy_cpu_framebuffer")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static unsafe partial RendererStatus CopyCpuFramebuffer(
            NativeContextHandle context,
            byte* pixels,
            nuint byteLength);
    }
}
