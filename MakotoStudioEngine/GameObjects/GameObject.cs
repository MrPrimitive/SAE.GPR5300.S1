using MakotoStudioEngine.Core;
using Silk.NET.OpenGL;

namespace MakotoStudioEngine.GameObjects {
  public abstract class GameObject : IGameObject {
    public GL Gl { get; }
    public Transform Transform { get; set; }
    public Material Material { get; set; }
    public Mesh Mesh { get; set; }

    protected GameObject(GL gl) {
      Gl = gl;
      Transform = new Transform();
    }

    public abstract void Init();
    public abstract unsafe void Update(double deltaTime);

    public abstract unsafe void Render(double deltaTime);
  }
}