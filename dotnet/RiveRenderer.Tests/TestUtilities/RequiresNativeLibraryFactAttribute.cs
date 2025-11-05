using System;
using Xunit;

namespace RiveRenderer.Tests.TestUtilities;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
internal sealed class RequiresNativeLibraryFactAttribute : FactAttribute
{
    public RequiresNativeLibraryFactAttribute()
    {
        if (!NativeTestHelper.TryEnsureNative(out var reason))
        {
            Skip = reason;
        }
    }
}
