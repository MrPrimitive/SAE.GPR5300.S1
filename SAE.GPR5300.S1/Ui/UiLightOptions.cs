using ImGuiNET;
using MSE.Engine.Interfaces;
using Silk.NET.OpenGL.Extensions.ImGui;

namespace SAE.GPR5300.S1.Ui {
  public class UiLightOptions : IUiInterface {
    private ImGuiController _controller;
    private bool _isDirectional;

    public static UiLightOptions Instance => Lazy.Value;
    private static readonly Lazy<UiLightOptions> Lazy = new(() => new());

    public static Action<bool> DirectionalEvent;

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

    public void RenderUi() {
      _controller.Render();
    }
  }
}