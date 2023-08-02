using MSE.Engine.Core;

namespace SAE.GPR5300.S1.Utils {
  public static class PlanetSystemUtil {
    public static float Rotation360(this float degrees, float speed) {
      degrees += speed * Time.DeltaTime;
      if (degrees > 360)
        return 0;
      return degrees;
    }
  }
}