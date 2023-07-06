using System.Numerics;
using MakotoStudioEngine.GameObjects;
using MakotoStudioEngine.Inputs;
using Silk.NET.Input;
using Silk.NET.Windowing;

namespace SAE.GPR5300.S1.Ui {
  public class Input : BaseInput {
    public static IInputContext InputContext => _inputContext;
    public static IKeyboard PrimaryKeyboard => _primaryKeyboard;
    public Vector2 LastMousePosition => _lastMousePosition;

    private bool _insertMode;
    private Vector2 _lastMousePosition;
    private Camera _camera;

    public Input(IWindow window, Camera camera)
      : base(window) {
      _camera = camera;
    }

    public override void AddKeyBordBindings() {
      if (_primaryKeyboard != null) {
        _primaryKeyboard.KeyDown += KeyDown;
      }

      for (int i = 0; i < _inputContext.Mice.Count; i++) {
        _inputContext.Mice[i].Cursor.CursorMode = CursorMode.Raw;
        _inputContext.Mice[i].MouseMove += OnMouseMove;
        _inputContext.Mice[i].Scroll += OnMouseWheel;
      }
    }

    public override void UpdateCamMove(double deltaTime) {
      if (_insertMode)
        return;

      var moveSpeed = 20.5f * (float)deltaTime;

      if (_primaryKeyboard.IsKeyPressed(Key.W)) {
        //Move forwards by adding a movement amount in the Camera's Front direction
        _camera.Position += moveSpeed * _camera.Front;
      }

      if (_primaryKeyboard.IsKeyPressed(Key.S)) {
        //Move backwards by subtracting a movement amount in the Camera's Front direction
        _camera.Position -= moveSpeed * _camera.Front;
      }

      if (_primaryKeyboard.IsKeyPressed(Key.A)) {
        //Move left by subtracting movement from the 'Right' direction (calculated by 'crossing' the front and up directions)
        _camera.Position -= Vector3.Normalize(Vector3.Cross(_camera.Front, _camera.Up)) * moveSpeed;
      }

      if (_primaryKeyboard.IsKeyPressed(Key.D)) {
        //Move right by adding movement in the 'Right' direction (calculated by 'crossing' the front and up directions)
        _camera.Position += Vector3.Normalize(Vector3.Cross(_camera.Front, _camera.Up)) * moveSpeed;
      }
    }

    private void KeyDown(IKeyboard arg1, Key arg2, int arg3) {
      //Check to close the window on escape.
      if (arg2 == Key.Escape) {
        _window.Close();
      }
      else if (arg2 == Key.I) {
        _insertMode = !_insertMode;
        _inputContext.Mice[0].Cursor.CursorMode =
          _inputContext.Mice[0].Cursor.CursorMode == CursorMode.Normal ? CursorMode.Raw : CursorMode.Normal;
      }
    }

    private unsafe void OnMouseMove(IMouse mouse, Vector2 position) {
      if (_insertMode)
        return;

      var lookSensitivity = 0.1f;
      if (_lastMousePosition == default) {
        _lastMousePosition = position;
      }
      else {
        var xOffset = (position.X - _lastMousePosition.X) * lookSensitivity;
        var yOffset = (position.Y - _lastMousePosition.Y) * lookSensitivity;
        _lastMousePosition = position;
        _camera.ModifyDirection(xOffset, yOffset);
      }
    }

    private unsafe void OnMouseWheel(IMouse mouse, ScrollWheel scrollWheel) {
      _camera.ModifyZoom(scrollWheel.Y);
    }
  }
}