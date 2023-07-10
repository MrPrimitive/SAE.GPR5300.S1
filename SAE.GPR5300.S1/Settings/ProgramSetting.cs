using Microsoft.Extensions.Configuration;
using SAE.GPR5300.S1.Extensions;
using Silk.NET.Maths;

namespace SAE.GPR5300.S1.Settings {
  public class ProgramSetting {
    public static ProgramSetting Instance => Lazy.Value;
    private static readonly Lazy<ProgramSetting> Lazy = new(() => new ProgramSetting());

    public ProgramConfig ProgramConfig { get; }

    public Vector2D<int> GetScreenSize =>
      new(ProgramConfig.ScreenSize.ScreenWith, ProgramConfig.ScreenSize.ScreenHeight);

    public bool IsFullScreen => ProgramConfig.FullScreen;

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
  }
}