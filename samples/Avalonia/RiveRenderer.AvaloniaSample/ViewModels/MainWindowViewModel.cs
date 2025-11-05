using System;
using CommunityToolkit.Mvvm.ComponentModel;
using RiveRenderer;

namespace RiveRenderer.AvaloniaSample.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private string statusMessage = "Initialising renderer...";

    public MainWindowViewModel()
    {
        try
        {
            using var device = RendererDevice.Create(RendererBackend.Null);
            using var context = device.CreateContext(64, 64);

            StatusMessage = $"Native renderer initialised using {RendererBackend.Null} backend.";
        }
        catch (DllNotFoundException ex)
        {
            StatusMessage = $"Native library not found. Run the platform build script to stage binaries. ({ex.Message})";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Renderer initialisation failed: {ex.Message}";
        }
    }
}
