using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace RiveRenderer;

internal static partial class NativeMethods
{
    internal static partial class Font
    {
        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_font_decode")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static unsafe partial RendererStatus Decode(
            NativeContextHandle context,
            void* data,
            nuint length,
            out NativeFontHandle font);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_font_retain")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus Retain(NativeFontHandle font);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_font_release")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus Release(NativeFontHandle font);
    }
}
