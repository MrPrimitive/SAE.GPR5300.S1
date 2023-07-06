using System.Numerics;
using MakotoStudioEngine.Extensions;
using MakotoStudioEngine.GameObjects;
using SAE.GPR5300.S1.Assets;
using SAE.GPR5300.S1.Assets.Scenes;
using SAE.GPR5300.S1.Core;
using SAE.GPR5300.S1.Ui;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.OpenGL.Extensions.ImGui;
using Silk.NET.Windowing;

namespace SAE.GPR5300.S1;

public static class Program {
  private static int _screenWith = 1200;
  private static int _screenHeight = 720;

  private static WindowOptions WindowOptions = WindowOptions.Default with {
    Title = "SAE GPR5300.S1 - MSE Engine",
    WindowState = WindowState.Normal,
    WindowBorder = WindowBorder.Resizable
  };

  private static IWindow _window;
  private static ImGuiController _imGuiController;
  private static GL _gl;
  private static bool _insertMode;
  private static Camera _camera;
  private static Input _msInput;
  private static Game _game;
  private static IMonitor? _monitor;
  private static bool _isFullScrren;
  private static SkyBox _skyBox;

  public static void Main(string[] args) {
    if (args.Length != 0) {
      if (args[0].Equals("full")) {
        WindowOptions = WindowOptions with {
          WindowState = WindowState.Fullscreen,
          WindowBorder = WindowBorder.Hidden
        };
        _isFullScrren = true;
      }
    }

    _window = Window.Create(WindowOptions);
    _window.Load += OnLoad;
    _window.Update += OnUpdate;
    _window.Render += OnRender;
    _window.Resize += OnResize;
    _window.Closing += OnClose;

    _window.Run();
    _window.Dispose();
  }

  private static void OnLoad() {
    _gl = _window.CreateOpenGL();
    if (_isFullScrren) {
      _screenWith = _window.Monitor.Bounds.Size.X;
      _screenHeight = _window.Monitor.Bounds.Size.Y;
      _gl.Viewport(new Vector2D<int>(_screenWith, _screenHeight));
    }

    _camera = new Camera(Vector3.UnitZ * 50, Vector3.UnitZ * -1, Vector3.UnitY, (float)_screenWith / _screenHeight);
    
    _msInput = new Input(_window, _camera);
    _imGuiController = new ImGuiController(_gl, _window, Input.InputContext);
    _msInput.AddKeyBordBindings();
    Init();
  }

  private static void OnUpdate(double deltaTime) {
    _msInput.UpdateCamMove(deltaTime);
    _game.Scenes
      .GetActiveScene()
      .Update(deltaTime);
    _skyBox.Update();
  }

  private static void OnRender(double deltaTime) {
    _gl.Enable(EnableCap.DepthTest);
    _gl.Clear((uint)(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));

    _imGuiController.Update((float)deltaTime);
    _skyBox.Render();

    var scene = _game.Scenes
      .GetActiveScene();
    
    scene.Render(deltaTime);
    
    scene.Uis().Update();
    scene.Uis().Render();
  }

  private static void OnResize(Vector2D<int> size) {
    _camera.AspectRatio = (float)size.X / size.Y;
    _gl.Viewport(size);
  }

  private static void Init() {
    _game = new Game();


    var mainScene = new MainScene(
      _gl,
      "Main Scene",
      _imGuiController,
      _camera);
    var solarSystemScene = new SolarSystemScene(
      _gl,
      "Solar System Scene",
      _imGuiController,
      _camera);

    _game.AddScene(mainScene);
    _game.AddScene(solarSystemScene);

    // mainScene.SetActive();
    solarSystemScene.SetActive();
    solarSystemScene.Init();

    _skyBox = new SkyBox(_gl, _camera);
  }

  private static void OnClose() {
    _gl.Dispose();
  }
}