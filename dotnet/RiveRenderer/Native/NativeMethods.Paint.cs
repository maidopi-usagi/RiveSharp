using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace RiveRenderer;

internal static partial class NativeMethods
{
    internal static partial class Paint
    {
        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_paint_create")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus Create(
            NativeContextHandle context,
            out NativePaintHandle paint);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_paint_retain")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus Retain(NativePaintHandle paint);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_paint_release")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus Release(NativePaintHandle paint);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_paint_set_style")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus SetStyle(NativePaintHandle paint, PaintStyle style);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_paint_set_color")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus SetColor(NativePaintHandle paint, uint color);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_paint_set_thickness")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus SetThickness(NativePaintHandle paint, float thickness);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_paint_set_join")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus SetJoin(NativePaintHandle paint, StrokeJoin join);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_paint_set_cap")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus SetCap(NativePaintHandle paint, StrokeCap cap);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_paint_set_feather")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus SetFeather(NativePaintHandle paint, float feather);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_paint_set_blend_mode")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus SetBlendMode(NativePaintHandle paint, BlendMode blendMode);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_paint_set_shader")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus SetShader(NativePaintHandle paint, NativeShaderHandle shader);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_paint_clear_shader")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus ClearShader(NativePaintHandle paint);
    }
}
