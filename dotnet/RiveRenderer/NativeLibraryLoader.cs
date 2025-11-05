using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace RiveRenderer;

internal static class NativeLibraryLoader
{
    private static bool s_loaded;
    private static readonly object s_lock = new();

    public static void EnsureLoaded()
    {
        if (s_loaded)
        {
            return;
        }

        lock (s_lock)
        {
            if (s_loaded)
            {
                return;
            }

            foreach (var name in GetCandidateNames())
            {
                if (NativeLibrary.TryLoad(name, out _))
                {
                    s_loaded = true;
                    return;
                }
            }

            throw new DllNotFoundException(
                "Unable to locate native library 'rive_renderer_ffi'. Add the native binaries to the application directory or adjust the load path.");
        }
    }

    private static IEnumerable<string> GetCandidateNames()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            yield return "rive_renderer_ffi.dll";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            yield return "librive_renderer_ffi.dylib";
            yield return "rive_renderer_ffi.dylib";
        }
        else
        {
            yield return "librive_renderer_ffi.so";
            yield return "rive_renderer_ffi.so";
        }

        yield return "rive_renderer_ffi";
    }
}
