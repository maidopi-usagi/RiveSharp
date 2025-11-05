using System;
using RiveRenderer;

static int RunValidation()
{
    try
    {
        using var device = RendererDevice.Create(RendererBackend.Null);
        using var context = device.CreateContext(128, 128);
        using var path = context.CreatePath();
        using var paint = context.CreatePaint();

        path.MoveTo(0, 0);
        path.LineTo(64, 0);
        path.LineTo(64, 64);
        path.Close();

        var shader = context.CreateLinearGradient(
            startX: 0,
            startY: 0,
            endX: 64,
            endY: 64,
            colors: new uint[] { 0xFF112233, 0xFF332211 },
            stops: new float[] { 0f, 1f });

        paint.SetStyle(PaintStyle.Fill);
        paint.SetShader(shader);

        context.BeginFrame();
        using (var renderer = context.CreateRenderer())
        {
            renderer.DrawPath(path, paint);
        }
        context.EndFrame();

        Console.WriteLine("Render validation succeeded using null backend.");
        return 0;
    }
    catch (DllNotFoundException ex)
    {
        Console.Error.WriteLine($"Native library missing: {ex.Message}");
        return 2;
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine($"Render validation failed: {ex}");
        return 1;
    }
}

return RunValidation();
