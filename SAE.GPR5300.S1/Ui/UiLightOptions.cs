using ImGuiNET;
using MSE.Engine.Interfaces;
using Silk.NET.OpenGL.Extensions.ImGui;

namespace SAE.GPR5300.S1.Ui {
  public class UiLightOptions : IUserInterface {
    public static Action<bool> DirectionalEvent;
    
    private ImGuiController _controller;
    private bool _isDirectional;

    public UiLightOptions() {
      _controller = UiController.Instance.ImGuiController;
    }

    public void UpdateUi() {
      ImGui.Begin("Shader Setting", ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove);
      if (ImGui.Checkbox("Directional Light", ref _isDirectional)) {
        DirectionalEvent.Invoke(_isDirectional);
      }

      ImGui.End();
    }
  }
}