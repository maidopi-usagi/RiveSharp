# Null Backend CPU Capture Sample

```csharp
using System;
using RiveRenderer;

const uint width = 256;
const uint height = 256;

using var device = RendererDevice.Create(RendererBackend.Null);
using var context = device.CreateContext(width, height);

context.BeginFrame();

using var renderer = context.CreateRenderer();
using var path = context.CreatePath();
using var paint = context.CreatePaint();

path.MoveTo(32, 32);
path.LineTo(224, 32);
path.LineTo(224, 224);
path.LineTo(32, 224);
path.Close();

var gradient = context.CreateRadialGradient(
    centerX: 128,
    centerY: 128,
    radius: 128,
    colors: new uint[] { 0xFF3366FF, 0xFFFFCC33 },
    stops: new float[] { 0.0f, 1.0f });

paint.SetStyle(PaintStyle.Fill);
paint.SetShader(gradient);

renderer.DrawPath(path, paint);

context.EndFrame();

Span<byte> pixels = stackalloc byte[(int)(width * height * 4)];
context.CopyCpuFramebuffer(pixels);

// Persist or inspect `pixels` as needed.
```

> **Note**
>
> The CPU framebuffer copy path is currently supported for the null backend. GPU backends will return `RendererStatus.Unsupported` until read-back support is implemented.
