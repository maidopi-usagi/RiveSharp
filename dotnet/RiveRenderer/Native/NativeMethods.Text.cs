using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace RiveRenderer;

internal static partial class NativeMethods
{
    internal static partial class Text
    {
        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_text_create_path")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static unsafe partial RendererStatus CreatePath(
            NativeContextHandle context,
            NativeFontHandle font,
            byte* utf8Text,
            nuint utf8Length,
            in TextStyleOptions style,
            FillRule fillRule,
            out NativePathHandle path);
    }
}
