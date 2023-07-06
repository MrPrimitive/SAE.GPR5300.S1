using MakotoStudioEngine.GameObjects;
using MakotoStudioEngine.Interfaces;
using Silk.NET.OpenGL;

namespace MakotoStudioEngine.Games {
  public interface IBaseScene {
    public void Init();
    public bool IsActiveScene();
    public GL Gl();
    public string GetSceneName();
    public void AddGameObject(GameObject go);
    public void SetActive();
    public void AddUi(IUiInterface ui);
    public void Render(double deltaTime);
    public void Update(double deltaTime);
    public List<IUiInterface> Uis();
  }
}