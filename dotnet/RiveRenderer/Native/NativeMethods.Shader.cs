using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace RiveRenderer;

internal static partial class NativeMethods
{
    internal static partial class Shader
    {
        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_shader_linear_gradient_create")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static unsafe partial RendererStatus CreateLinearGradient(
            NativeContextHandle context,
            float startX,
            float startY,
            float endX,
            float endY,
            uint* colors,
            float* stops,
            nuint count,
            out NativeShaderHandle shader);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_shader_radial_gradient_create")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static unsafe partial RendererStatus CreateRadialGradient(
            NativeContextHandle context,
            float centerX,
            float centerY,
            float radius,
            uint* colors,
            float* stops,
            nuint count,
            out NativeShaderHandle shader);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_shader_retain")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus Retain(NativeShaderHandle shader);

        [LibraryImport(LibraryName, EntryPoint = "rive_renderer_shader_release")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        internal static partial RendererStatus Release(NativeShaderHandle shader);
    }
}
