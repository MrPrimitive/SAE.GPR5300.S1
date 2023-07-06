using System.Numerics;
using MakotoStudioEngine.Extensions;

namespace MakotoStudioEngine.GameObjects {
  public class Camera {
    public static Vector3 CameraPosition => _cameraPosition;
    public static Vector3 CameraFront => _cameraFront;
    public static Vector3 CameraUp => _cameraUp;
    public static Vector3 CameraDirection => _cameraDirection;
    public static float CameraYaw => _cameraYaw;
    public static float CameraPitch => _cameraPitch;
    public static float CameraZoom => _cameraZoom;

    private static Vector3 _cameraPosition = new(0.0f, 0.0f, 3.0f);
    private static Vector3 _cameraFront = new(0.0f, 0.0f, -1.0f);
    private static Vector3 _cameraUp = Vector3.UnitY;
    private static Vector3 _cameraDirection = Vector3.Zero;
    private static float _cameraYaw = -90f;
    private static float _cameraPitch = 0f;
    private static float _cameraZoom = 45f;

    public Vector3 Position { get; set; }
    public Vector3 Front { get; set; }

    public Vector3 Up { get; private set; }
    public float AspectRatio { get; set; }

    public float Yaw { get; set; } = -90f;
    public float Pitch { get; set; }

    private float Zoom = 45f;

    public Camera(Vector3 position,
      Vector3 front,
      Vector3 up,
      float aspectRatio) {
      Position = position;
      AspectRatio = aspectRatio;
      Front = front;
      Up = up;
    }

    public void ModifyZoom(float zoomAmount) {
      //We don't want to be able to zoom in too close or too far away so clamp to these values
      Zoom = Math.Clamp(Zoom - zoomAmount, 1.0f, 45f);
    }

    public void ModifyDirection(float xOffset, float yOffset) {
      Yaw += xOffset;
      Pitch -= yOffset;

      //We don't want to be able to look behind us by going over our head or under our feet so make sure it stays within these bounds
      Pitch = Math.Clamp(Pitch, -89f, 89f);

      var cameraDirection = Vector3.Zero;
      cameraDirection.X = MathF.Cos(Yaw.DegreesToRadians()) * MathF.Cos(Pitch.DegreesToRadians());
      cameraDirection.Y = MathF.Sin(Pitch.DegreesToRadians());
      cameraDirection.Z = MathF.Sin(Yaw.DegreesToRadians()) * MathF.Cos(Pitch.DegreesToRadians());

      Front = Vector3.Normalize(cameraDirection);
    }

    public Matrix4x4 GetViewMatrix() {
      return Matrix4x4.CreateLookAt(Position, Position + Front, Up);
    }

    public Matrix4x4 GetProjectionMatrix() {
      return Matrix4x4.CreatePerspectiveFieldOfView(Zoom.DegreesToRadians(), AspectRatio, 0.1f, 1000.0f);
    }
  }
}