using Silk.NET.Windowing;

namespace SAE.GPR5300.S1.Settings;

public class ProgramConfig {
  public bool FullScreen { get; set; } = false;
  public ScreenSize ScreenSize { get; set; } = new();
  public WindowState WindowState { get; set; } = WindowState.Normal;
  public WindowBorder WindowBorder { get; set; } = WindowBorder.Resizable;
  public Position Position { get; set; } = new();
}

public class ScreenSize {
  public int ScreenWith { get; set; } = 1280;
  public int ScreenHeight { get; set; } = 720;
}

public class Position {
  public int XPos { get; set; } = 50;
  public int YPos { get; set; } = 50;
}