using System.Numerics;
using MSE.Engine.Core;
using MSE.Engine.GameObjects;
using MSE.Engine.Interfaces;
using SAE.GPR5300.S1.Assets.Scenes;
using SAE.GPR5300.S1.Settings;
using SAE.GPR5300.S1.Ui;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace SAE.GPR5300.S1.Core {
  public class Game : IGame {
    public static Game Instance => Lazy.Value;
    public IWindow GameWindow { get; private set; } = null!;
    public GL Gl { get; private set; } = null!;

    private static readonly Lazy<Game> Lazy = new(() => new Game());
    private WindowOptions _windowOptions = WindowOptions.Default;

    private Game() {
      _windowOptions = _windowOptions with {
        Size = ProgramSetting.Instance.GetScreenSize,
        Title = "SAE GPR5300.S1 - MSE Engine",
        WindowState = ProgramSetting.Instance.ProgramConfig.WindowState,
        WindowBorder = ProgramSetting.Instance.ProgramConfig.WindowBorder,
      };
      if (ProgramSetting.Instance.IsFullScreen) {
        _windowOptions = _windowOptions with {
          Position = new Vector2D<int>(0, 0),
          WindowState = WindowState.Fullscreen,
          WindowBorder = WindowBorder.Hidden,
        };
      }
    }

    public void SetFullScreen() {
      if (ProgramSetting.Instance.IsFullScreen) {
        GameWindow.WindowState = WindowState.Normal;
        GameWindow.WindowBorder = WindowBorder.Resizable;
        ProgramSetting.Instance.ProgramConfig.FullScreen = false;
      }
      else {
        GameWindow.WindowState = WindowState.Fullscreen;
        GameWindow.WindowBorder = WindowBorder.Hidden;
        ProgramSetting.Instance.ProgramConfig.FullScreen = true;
      }
    }

    public void Run() {
      GameWindow = Window.Create(_windowOptions);
      GameWindow.Load += OnLoad;
      GameWindow.Update += OnUpdate;
      GameWindow.Render += OnRender;
      GameWindow.Resize += OnResize;
      GameWindow.Closing += OnClose;
      GameWindow.Run();
      GameWindow.Dispose();
    }

    private void OnLoad() {
      Gl = GameWindow.CreateOpenGL();
      ProgramSetting.Instance.SetSize(GameWindow.Size.X, GameWindow.Size.Y);

      if (ProgramSetting.Instance.IsFullScreen) {
        Gl.Viewport(ProgramSetting.Instance.GetScreenSize);
      }

      // Gl.PolygonMode(TriangleFace.Front, PolygonMode.Fill);
      Camera.Instance.SetUp(Vector3.UnitZ * 50,
        Vector3.UnitZ * -1,
        Vector3.UnitY,
        (float)ProgramSetting.Instance.GetScreenSize.X / ProgramSetting.Instance.GetScreenSize.Y);
      Input.Instance.AddKeyBordBindings();

      var mainScene = new MainScene("Main Scene");
      var solarSystemScene = new SolarSystemScene("Solar System Scene");

      SceneManager.Instance.AddScene(mainScene);
      SceneManager.Instance.AddScene(solarSystemScene);
      SceneManager.Instance.SetSceneActive("Solar System Scene");
    }

    private void OnUpdate(double deltaTime) {
      Input.Instance.UpdateCamMove(deltaTime);
      SceneManager.Instance
        .GetActiveScene()
        .UpdateScene(deltaTime);
    }

    private void OnRender(double deltaTime) {
      Gl.Enable(EnableCap.DepthTest);
      Gl.DepthMask(true);
      Gl.Clear((uint)(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));
      UiController.Instance.ImGuiController.Update((float)deltaTime);
      SceneManager.Instance
        .GetActiveScene()
        .RenderScene(deltaTime);
    }

    private void OnResize(Vector2D<int> size) {
      Camera.Instance.AspectRatio = (float)size.X / size.Y;
      Gl.Viewport(size);
      ProgramSetting.Instance.SetSize(size);
    }

    private void OnClose() {
      Gl.Dispose();
    }
  }
}