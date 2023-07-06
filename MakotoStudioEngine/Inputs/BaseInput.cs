using System.Numerics;
using MakotoStudioEngine.GameObjects;
using Silk.NET.Input;
using Silk.NET.Windowing;

namespace MakotoStudioEngine.Inputs {
  public abstract class BaseInput {
    protected static IInputContext _inputContext;
    protected static IKeyboard _primaryKeyboard;
    // protected static Camera _camera;
    protected IWindow _window;

    protected BaseInput(IWindow window) {
      _window = window;
      _inputContext = _window.CreateInput();
      // _camera = new Camera(Vector3.UnitZ * 6, Vector3.UnitZ * -1, Vector3.UnitY, (float)screenWith / screenHeight);
      _primaryKeyboard = _inputContext.Keyboards.FirstOrDefault();
    }

    public abstract void AddKeyBordBindings();
    public abstract void UpdateCamMove(double deltaTime);
  }
}