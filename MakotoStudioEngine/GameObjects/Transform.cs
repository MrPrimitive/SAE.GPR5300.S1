using System.Numerics;

namespace MakotoStudioEngine.GameObjects {
  public class Transform {
    public Vector3 Position { get; set; } = new(0, 0, 0);
    public float Scale { get; set; } = 1f;
    public Quaternion Rotation { get; set; } = Quaternion.Identity;

    //Note: The order here does matter.
    public Matrix4x4 ViewMatrix =>
      Matrix4x4.Identity
      * Matrix4x4.CreateFromQuaternion(Rotation)
      * Matrix4x4.CreateScale(Scale)
      * Matrix4x4.CreateTranslation(Position);

    public static Quaternion RotateY(float rotationAngle) {
      var q = Quaternion.CreateFromAxisAngle(Vector3.UnitY, rotationAngle);
      var aVector = Vector3.UnitX;
      return q * (new Quaternion(aVector, 0)) * Quaternion.Conjugate(q);
    }
    
    public static Quaternion RotateZ(float rotationAngle) {
      var q = Quaternion.CreateFromAxisAngle(Vector3.UnitZ, rotationAngle);
      var aVector = Vector3.UnitY;
      return q * (new Quaternion(aVector, 0)) * Quaternion.Conjugate(q);
    }
    
    public static Quaternion RotateX(float rotationAngle) {
      var q = Quaternion.CreateFromAxisAngle(Vector3.UnitX, rotationAngle);
      var aVector = Vector3.UnitY;
      return q * (new Quaternion(aVector, 0)) * Quaternion.Conjugate(q);
    }
  }
}