using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace RiveRenderer;

internal static partial class NativeMethods
{
    internal static partial class Image
    {
        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_image_decode")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static unsafe partial RendererStatus Decode(
            NativeContextHandle context,
            void* data,
            nuint length,
            out NativeImageHandle image);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_image_retain")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus Retain(NativeImageHandle image);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_image_release")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus Release(NativeImageHandle image);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_image_get_size")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus GetSize(
            NativeImageHandle image,
            out uint width,
            out uint height);
    }
}
