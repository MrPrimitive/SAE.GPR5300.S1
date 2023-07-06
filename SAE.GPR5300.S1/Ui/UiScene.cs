using ImGuiNET;
using MakotoStudioEngine.Games;
using MakotoStudioEngine.Interfaces;
using Silk.NET.OpenGL;
using Silk.NET.OpenGL.Extensions.ImGui;

namespace SAE.GPR5300.S1.Ui {
  public class UiScene : IUiInterface {
    private GL _gl;
    private ImGuiController _controller;
    private IBaseScene _scene;

    public UiScene(GL gl, ImGuiController imGuiController, IBaseScene scene) {
      _gl = gl;
      _controller = imGuiController;
      _scene = scene;
    }

    public void Update() {
      ImGui.Begin(_scene.GetSceneName());
      ImGui.Text("List view of all object in scene");

      ImGui.End();
    }

    public void Render() {
      _controller.Render();
    }
  }
}