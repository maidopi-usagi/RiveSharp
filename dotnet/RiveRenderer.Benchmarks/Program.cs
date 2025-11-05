using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace RiveRenderer.Benchmarks;

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<RendererBenchmarks>();
    }
}

[MemoryDiagnoser]
public class RendererBenchmarks
{
    private RendererDevice? _device;

    [GlobalSetup]
    public void Setup()
    {
        _device = RendererDevice.Create(RendererBackend.Null);
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        _device?.Dispose();
    }

    [Benchmark]
    public void FrameLifecycle()
    {
        using var context = _device!.CreateContext(64, 64);
        context.BeginFrame();
        using var renderer = context.CreateRenderer();
        context.EndFrame();
    }
}
