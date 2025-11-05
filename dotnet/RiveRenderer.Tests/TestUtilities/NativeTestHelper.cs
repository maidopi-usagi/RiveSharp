using System;

namespace RiveRenderer.Tests.TestUtilities;

internal static class NativeTestHelper
{
    public static bool TryEnsureNative(out string? skipReason)
    {
        try
        {
            NativeLibraryLoader.EnsureLoaded();
            skipReason = null;
            return true;
        }
        catch (DllNotFoundException ex)
        {
            skipReason = $"Native renderer library unavailable: {ex.Message}";
            return false;
        }
    }
}
