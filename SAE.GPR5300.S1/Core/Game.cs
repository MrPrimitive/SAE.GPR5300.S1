using System.Numerics;
using MSE.Engine.Core;
using MSE.Engine.GameObjects;
using MSE.Engine.Interfaces;
using SAE.GPR5300.S1.Assets.Scenes;
using SAE.GPR5300.S1.Ui;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace SAE.GPR5300.S1.Core {
  public class Game : IGame {
    public static Game Instance => Lazy.Value;
    public IWindow GameWindow { get; private set; } = null!;
    public GL Gl { get; private set; } = null!;
    public int ScreenWith { get; private set; } = WindowOptions.Default.Size.X;
    public int ScreenHeight { get; private set; } = WindowOptions.Default.Size.Y;

    private static readonly Lazy<Game> Lazy = new(() => new Game());
    private WindowOptions _windowOptions = WindowOptions.Default;
    private bool _isFullScreen;

    private Game() {
      _windowOptions = _windowOptions with {
        Title = "SAE GPR5300.S1 - MSE Engine",
        WindowState = WindowState.Normal,
        WindowBorder = WindowBorder.Resizable
      };
    }

    public Game SetConfig(StartUpConfig startUpConfig) {
      if (startUpConfig.FullScreen) {
        _windowOptions = _windowOptions with {
          WindowState = WindowState.Fullscreen,
          WindowBorder = WindowBorder.Hidden,
        };
        _isFullScreen = true;
      }

      return this;
    }

    public void Run() {
      GameWindow = Window.Create(_windowOptions);
      GameWindow.Load += OnLoad;
      GameWindow.Update += OnUpdate;
      GameWindow.Render += OnRender;
      GameWindow.Resize += OnResize;
      GameWindow.Closing += OnClose;

      // Set Values

      GameWindow.Run();
      GameWindow.Dispose();
    }

    #region SilkEvents

    private void OnLoad() {
      Gl = GameWindow.CreateOpenGL();
      if (_isFullScreen) {
        ScreenWith = GameWindow.Monitor.Bounds.Size.X;
        ScreenHeight = GameWindow.Monitor.Bounds.Size.Y;
        Gl.Viewport(new Vector2D<int>(ScreenWith, ScreenHeight));
      }

      SetScreenSize(new Vector2D<int>(ScreenWith, ScreenHeight));
      Camera.Instance.SetUp(Vector3.UnitZ * 50, Vector3.UnitZ * -1, Vector3.UnitY, (float)ScreenWith / ScreenHeight);
      Input.Instance.AddKeyBordBindings();

      var mainScene = new MainScene("Main Scene");
      var solarSystemScene = new SolarSystemScene("Solar System Scene");

      SceneManager.Instance.AddScene(mainScene);
      SceneManager.Instance.AddScene(solarSystemScene);
      SceneManager.Instance.SetSceneActive("Main Scene");
    }

    private void OnUpdate(double deltaTime) {
      Input.Instance.UpdateCamMove(deltaTime);

      SceneManager.Instance
        .GetActiveScene()
        .UpdateScene(deltaTime);
    }

    private void OnRender(double deltaTime) {
      Gl.Enable(EnableCap.DepthTest);
      Gl.Clear((uint)(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));

      UiController.Instance.ImGuiController.Update((float)deltaTime);

      SceneManager.Instance
        .GetActiveScene()
        .RenderScene(deltaTime);
    }

    private void OnResize(Vector2D<int> size) {
      Camera.Instance.AspectRatio = (float)size.X / size.Y;
      Gl.Viewport(size);
      Instance.SetScreenSize(size);
    }

    private void OnClose() {
      Gl.Dispose();
    }

    #endregion

    private void SetScreenSize(Vector2D<int> size) {
      ScreenWith = size.X;
      ScreenHeight = size.Y;
    }
  }
}