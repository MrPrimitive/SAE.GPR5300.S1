using Microsoft.Extensions.Configuration;
using SAE.GPR5300.S1.Core;
using SAE.GPR5300.S1.Extensions;
using Silk.NET.Maths;
using Silk.NET.Windowing;

namespace SAE.GPR5300.S1.Settings {
  public class ProgramSetting {
    public static ProgramSetting Instance => Lazy.Value;
    public ProgramConfig ProgramConfig { get; }

    public Vector2D<int> GetScreenSize =>
      new(ProgramConfig.ScreenSize.ScreenWith, ProgramConfig.ScreenSize.ScreenHeight);

    public bool IsFullScreen => ProgramConfig.FullScreen;

    private static readonly Lazy<ProgramSetting> Lazy = new(() => new ProgramSetting());

    private ProgramSetting() {
      ProgramConfig = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .GetConfig()
        .GetStartUpConfig();
    }

    public void SetSize(int sizeX, int sizeY) {
      ProgramConfig.ScreenSize.ScreenWith = sizeX;
      ProgramConfig.ScreenSize.ScreenHeight = sizeY;
    }

    public void SetSize(Vector2D<int> size) {
      ProgramConfig.ScreenSize.ScreenWith = size.X;
      ProgramConfig.ScreenSize.ScreenHeight = size.Y;
    }

    public void SetFullScreen() {
      if (IsFullScreen) {
        Game.Instance.GameWindow.WindowState = WindowState.Normal;
        Game.Instance.GameWindow.WindowBorder = WindowBorder.Resizable;
        ProgramConfig.FullScreen = false;
      }
      else {
        Game.Instance.GameWindow.WindowState = WindowState.Fullscreen;
        Game.Instance.GameWindow.WindowBorder = WindowBorder.Hidden;
        ProgramConfig.FullScreen = true;
      }
    }
  }
}