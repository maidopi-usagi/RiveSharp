using System;
using RiveRenderer.Tests.TestUtilities;

namespace RiveRenderer.Tests;

public class RendererBehaviorTests
{
    [RequiresNativeLibraryFact]
    public void CanDrawGradientPath()
    {
        using var device = RendererDevice.Create(RendererBackend.Null);
        using var context = device.CreateContext(64, 64);
        using var path = context.CreatePath();
        using var paint = context.CreatePaint();

        path.MoveTo(0, 0);
        path.LineTo(32, 0);
        path.LineTo(32, 32);
        path.Close();

        var shader = context.CreateLinearGradient(
            startX: 0,
            startY: 0,
            endX: 32,
            endY: 32,
            colors: new uint[] { 0xFF0000FF, 0xFF00FF00 },
            stops: new float[] { 0f, 1f });

        paint.SetStyle(PaintStyle.Fill);
        paint.SetShader(shader);

        context.BeginFrame();
        using (var renderer = context.CreateRenderer())
        {
            renderer.DrawPath(path, paint);
        }
        context.EndFrame();
    }
}
