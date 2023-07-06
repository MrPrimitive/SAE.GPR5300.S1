namespace MakotoStudioEngine.GameObjects {
  public interface IGameObject {
    public unsafe void Update(double deltaTime);
    public unsafe void Render(double deltaTime);
  }
}