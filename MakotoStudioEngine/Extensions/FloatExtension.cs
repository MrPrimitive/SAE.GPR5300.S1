namespace MSE.Engine.Extensions {
  public static class FloatExtension {
    public static float DegreesToRadians(this float degrees) {
      return MathF.PI / 180f * degrees;
    }
  }
}