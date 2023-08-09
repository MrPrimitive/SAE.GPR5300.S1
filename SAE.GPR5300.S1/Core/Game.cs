using System.Numerics;
using MSE.Engine.Core;
using MSE.Engine.GameObjects;
using SAE.GPR5300.S1.Assets.Scenes;
using SAE.GPR5300.S1.Settings;
using SAE.GPR5300.S1.Ui;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace SAE.GPR5300.S1.Core {
  public class Game {
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
      Gl.PolygonMode(TriangleFace.Back, PolygonMode.Fill);
      if (ProgramSetting.Instance.IsFullScreen) {
        Gl.Viewport(ProgramSetting.Instance.GetScreenSize);
      }

      

      int width = ProgramSetting.Instance.GetScreenSize.X;
      int height = ProgramSetting.Instance.GetScreenSize.Y;

      Camera.Instance.SetUp(Vector3.UnitZ * 50,
        Vector3.UnitZ * -1,
        Vector3.UnitY,
        (float)width / height);

      Input.Instance.AddKeyBordBindings();
      Light.Instance.Init(new Vector2D<int>(width, height));

      AddAllScene();
      ActivateScene();
    }

    private void ActivateScene() {
      SceneManager.Instance.SetSceneActive(SceneName.MainMenu);
    }

    private void AddAllScene() {
      SceneManager.Instance.AddScene(new MainMenuScene());
      SceneManager.Instance.AddScene(new SolarSystemScene());
      SceneManager.Instance.AddScene(new ReflectionScene());
      SceneManager.Instance.AddScene(new WorldMapScene());
      SceneManager.Instance.AddScene(new BlinnPhongLightingScene());
    }

    private void OnUpdate(double deltaTime) {
      _time.UpdateDeltaTime(deltaTime);
      Input.Instance.UpdateCamMove();
      Light.Instance.Update();
      UiController.Instance.ImGuiController.Update(Time.DeltaTime);
      SceneManager.Instance
        .GetActiveScene()
        .UpdateScene();
    }

    private void OnRender(double deltaTime) {
      Gl.Enable(EnableCap.DepthTest);
      Gl.DepthMask(true);
      Gl.Clear((uint)(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));
      SceneManager.Instance
        .GetActiveScene()
        .RenderScene();
      UiController.Instance.ImGuiController.Render();
    }

    private void OnResize(Vector2D<int> size) {
      Camera.Instance.AspectRatio = (float)size.X / size.Y;
      Light.Instance.ViewSize(size);
      Gl.Viewport(size);
      ProgramSetting.Instance.SetSize(size);
    }

    private void OnClose() {
      Gl.Dispose();
    }
  }
}