using System.Numerics;
using Silk.NET.Maths;

namespace MSE.Engine.GameObjects {
  public class Light {
    public static Light Instance => Lazy.Value;
    public Vector3 LightPosition { get; set; }
    public Matrix4x4 LightSpaceMatrix { get; set; }

    private Matrix4x4 _lightProjection;
    private Matrix4x4 _lightView;
    private int _width;
    private int _height;

    private static readonly Lazy<Light> Lazy = new(() => new());

    private Light() {
      LightPosition = new Vector3(0f, 2f, 0f);
    }

    public void Init(Vector2D<int> size) {
      _width = size.X;
      _height = size.Y;
    }

    public void Update() {
      _lightProjection = Matrix4x4.CreatePerspective(_width, _height, 0.1f, 100f);
      _lightView = Matrix4x4.CreateLookAt(Camera.Instance.Position, Camera.Instance.Front, Camera.Instance.Up);
      LightSpaceMatrix = _lightProjection * _lightView;
    }

    public void ViewSize(Vector2D<int> size) {
      _width = size.X;
      _height = size.Y;
    }
  }
}