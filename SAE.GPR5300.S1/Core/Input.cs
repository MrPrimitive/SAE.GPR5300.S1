using System.Numerics;
using MSE.Engine.Core;
using MSE.Engine.GameObjects;
using Silk.NET.Input;

namespace SAE.GPR5300.S1.Core {
  public class Input {
    public static Input Instance => Lazy.Value;
    public IInputContext InputContext => _inputContext;
    public IKeyboard PrimaryKeyboard => _primaryKeyboard;
    public Vector2 LastMousePosition => _lastMousePosition;

    private static readonly Lazy<Input> Lazy = new(() => new Input());
    private bool _insertMode = true;
    private Vector2 _lastMousePosition;
    private IInputContext _inputContext;
    private IKeyboard _primaryKeyboard;

    private Input() {
      _inputContext = Game.Instance.GameWindow.CreateInput();
      _primaryKeyboard = _inputContext.Keyboards.FirstOrDefault();
    }

    public void AddKeyBordBindings() {
      if (_primaryKeyboard != null) {
        _primaryKeyboard.KeyDown += KeyDown;
      }

      foreach (var mouse in _inputContext.Mice) {
        mouse.Cursor.CursorMode = CursorMode.Normal;
        mouse.MouseMove += OnMouseMove;
        mouse.Scroll += OnMouseWheel;
      }
    }

    public void UpdateCamMove() {
      if (_insertMode)
        return;

      var moveSpeed = 10.5f * Time.DeltaTime;
      var multiplier = 1;
      if (_primaryKeyboard.IsKeyPressed(Key.ShiftLeft)) {
        multiplier = 4;
      }

      if (_primaryKeyboard.IsKeyPressed(Key.W)) {
        Camera.Instance.Position += moveSpeed * multiplier * Camera.Instance.Front;
      }

      if (_primaryKeyboard.IsKeyPressed(Key.S)) {
        Camera.Instance.Position -= moveSpeed * multiplier * Camera.Instance.Front;
      }

      if (_primaryKeyboard.IsKeyPressed(Key.A)) {
        Camera.Instance.Position -= Vector3.Normalize(Vector3.Cross(Camera.Instance.Front, Camera.Instance.Up)) *
                                    moveSpeed * multiplier;
      }

      if (_primaryKeyboard.IsKeyPressed(Key.D)) {
        Camera.Instance.Position += Vector3.Normalize(Vector3.Cross(Camera.Instance.Front, Camera.Instance.Up)) *
                                    moveSpeed * multiplier;
      }
    }

    private void KeyDown(IKeyboard arg1, Key arg2, int arg3) {
      if (arg2 == Key.Escape) {
        Game.Instance.GameWindow.Close();
      }
      else if (arg2 == Key.I) {
        _insertMode = !_insertMode;
        _inputContext.Mice[0].Cursor.CursorMode =
          _inputContext.Mice[0].Cursor.CursorMode == CursorMode.Normal ? CursorMode.Raw : CursorMode.Normal;
      }
    }

    private void OnMouseMove(IMouse mouse, Vector2 position) {
      if (_insertMode)
        return;

      const float lookSensitivity = 0.1f;
      if (_lastMousePosition == default) {
        _lastMousePosition = position;
      }
      else {
        var xOffset = (position.X - _lastMousePosition.X) * lookSensitivity;
        var yOffset = (position.Y - _lastMousePosition.Y) * lookSensitivity;
        _lastMousePosition = position;
        Camera.Instance.ModifyDirection(xOffset, yOffset);
      }
    }

    private void OnMouseWheel(IMouse mouse, ScrollWheel scrollWheel) {
      if (_insertMode)
        return;
      Camera.Instance.ModifyZoom(scrollWheel.Y);
    }
  }
}