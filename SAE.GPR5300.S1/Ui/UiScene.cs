using ImGuiNET;
using MSE.Engine.Interfaces;
using Silk.NET.OpenGL;
using Silk.NET.OpenGL.Extensions.ImGui;

namespace SAE.GPR5300.S1.Ui {
  public class UiScene : IUiInterface {
    private IScene _scene;

    public UiScene(IScene scene) {
      _scene = scene;
    }

    public void UpdateUi() {
      ImGui.Begin(_scene.GetSceneName());
      ImGui.Text("List view of all object in scene");

      ImGui.End();
    }

    public void RenderUi() {
      UiController.Instance.ImGuiController.Render();
    }
  }
}