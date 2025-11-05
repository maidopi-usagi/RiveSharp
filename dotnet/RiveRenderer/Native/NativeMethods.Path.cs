using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace RiveRenderer;

internal static partial class NativeMethods
{
    internal static partial class Path
    {
        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_path_create")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus Create(
            NativeContextHandle context,
            FillRule fillRule,
            out NativePathHandle path);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_path_retain")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus Retain(NativePathHandle path);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_path_release")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus Release(NativePathHandle path);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_path_rewind")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus Rewind(NativePathHandle path);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_path_set_fill_rule")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus SetFillRule(NativePathHandle path, FillRule fillRule);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_path_move_to")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus MoveTo(NativePathHandle path, float x, float y);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_path_line_to")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus LineTo(NativePathHandle path, float x, float y);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_path_cubic_to")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus CubicTo(
            NativePathHandle path,
            float ox,
            float oy,
            float ix,
            float iy,
            float x,
            float y);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_path_close")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus Close(NativePathHandle path);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_path_add_path")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus AddPath(
            NativePathHandle destination,
            NativePathHandle source,
            in Mat2D transform);
    }
}
