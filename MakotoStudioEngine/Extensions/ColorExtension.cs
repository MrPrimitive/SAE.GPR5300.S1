using System.Numerics;

namespace MSE.Engine.Extensions {
  public static class ColorExtension {
    public static Vector3 ToVector3(this System.Drawing.Color color) {
      return new Vector3((float)color.R / 255, (float)color.G / 255, (float)color.B / 255);
    }
  }
}