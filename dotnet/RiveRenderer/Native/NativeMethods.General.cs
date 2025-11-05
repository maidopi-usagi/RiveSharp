using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace RiveRenderer;

internal static partial class NativeMethods
{
    [LibraryImport(LibraryName, EntryPoint = "rive_renderer_get_last_error_message")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    internal static unsafe partial nuint GetLastErrorMessage(byte* buffer, nuint bufferLength);

    [LibraryImport(LibraryName, EntryPoint = "rive_renderer_clear_last_error")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    internal static partial void ClearLastError();

    [LibraryImport(LibraryName, EntryPoint = "rive_renderer_run_self_test")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    internal static partial RendererStatus RunSelfTest();
}
