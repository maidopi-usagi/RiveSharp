using System.Runtime.InteropServices;

namespace RiveRenderer;

internal static partial class NativeMethods
{
    private const string LibraryName = "rive_renderer_ffi";

    static NativeMethods()
    {
        NativeLibraryLoader.EnsureLoaded();
    }
}
