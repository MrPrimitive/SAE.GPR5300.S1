using MSE.Engine.Extensions;
using MSE.Engine.Interfaces;
using Silk.NET.OpenGL;

namespace MSE.Engine.GameObjects {
  public abstract class GameObject : Disposable, IGameObject {
    public GL Gl { get; }
    public Transform Transform { get; set; }
    public Material Material { get; set; }
    public Mesh Mesh { get; set; }

    protected GameObject(GL gl) {
      Gl = gl;
      Transform = new Transform();
    }

    public abstract void UpdateGameObject();

    public abstract void RenderGameObject();
  }
}