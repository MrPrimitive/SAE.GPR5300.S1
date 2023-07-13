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
    private readonly WindowOptions _windowOptions = WindowOptions.Default;
    private Time _time;

    private Game() {
      _time = new Time();
      _windowOptions = _windowOptions with {
        Size = ProgramSetting.Instance.GetScreenSize,
        Title = "SAE GPR5300.S1 - MSE Engine",
        WindowState = ProgramSetting.Instance.ProgramConfig.WindowState,
        WindowBorder = ProgramSetting.Instance.ProgramConfig.WindowBorder,
      };
      if (ProgramSetting.Instance.IsFullScreen) {
        _windowOptions = _windowOptions with {
          WindowState = WindowState.Fullscreen,
          WindowBorder = WindowBorder.Hidden,
        };
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

      Camera.Instance.SetUp(Vector3.UnitZ * 50,
        Vector3.UnitZ * -1,
        Vector3.UnitY,
        (float)ProgramSetting.Instance.GetScreenSize.X / ProgramSetting.Instance.GetScreenSize.Y);
      Input.Instance.AddKeyBordBindings();

      var mainScene = new MainScene("Main Scene");
      SceneManager.Instance.AddScene(mainScene);
      
      var solarSystemScene = new SolarSystemScene("Solar System Scene");
      SceneManager.Instance.AddScene(solarSystemScene);

      var p8Scene = new P8Scene("P8 - Scene");
      SceneManager.Instance.AddScene(p8Scene);
      
      // SceneManager.Instance.SetSceneActive("Solar System Scene");
      // SceneManager.Instance.SetSceneActive("Main Scene");
      SceneManager.Instance.SetSceneActive("P8 - Scene");
    }

    private void OnUpdate(double deltaTime) {
      // Create Time Class wih own delta time 
      _time.UpdateDeltaTime(deltaTime);
      Input.Instance.UpdateCamMove();
      SceneManager.Instance
        .GetActiveScene()
        .UpdateScene();
    }

    private void OnRender(double deltaTime) {
      Gl.Enable(EnableCap.DepthTest);
      Gl.DepthMask(true);
      Gl.Clear((uint)(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));
      UiController.Instance.ImGuiController.Update(Time.DeltaTime);
      SceneManager.Instance
        .GetActiveScene()
        .RenderScene();
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