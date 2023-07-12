namespace MSE.Engine.Core {
  public struct Time {
    public static float DeltaTime { get; private set; }

    public void UpdateDeltaTime(double deltaTime) {
      DeltaTime = (float)deltaTime;
    }
  }
}