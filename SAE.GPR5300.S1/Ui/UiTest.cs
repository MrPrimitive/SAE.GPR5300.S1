using System.Numerics;
using ImGuiNET;
using MSE.Engine.Interfaces;

namespace SAE.GPR5300.S1.Ui {
  public class UiTest : IUserInterface {
    private bool _isColor;
    private bool _isTexture;
    private float _alpha;
    private Vector3 _colorValue;
    private Vector3 _scaleValue;

    public UiTest() {
    }

    public void LoadDemoWindow() {
      ImGui.ShowDemoWindow();
    }

    public void UpdateUi() {
      // ImGui.Begin("UI Settings");
      // ImGui.IsWindowDocked();
      // ImGui.Text("Texture must be active for Texture blend with alpha chânnel");
      // ImGui.Checkbox("Color", ref _isColor);
      // ImGui.Checkbox("Texture", ref _isTexture);
      // ImGui.End();
      // ImGui.Begin("UI Settings2");
      // ImGui.IsWindowDocked();
      // ImGui.Text("Texture must be active for Texture blend with alpha chânnel");
      // ImGui.SliderFloat("alpha", ref _alpha, 0.0f, 1.0f);
      // ImGui.DragFloat3("Scale", ref _scaleValue, 0.1f, 0.01f, 5.0f);
      // ImGui.ColorEdit3("Color", ref _colorValue);
      // ImGui.End();
      LoadDemoWindow();
    }

    public void RenderUi() {
      UiController.Instance.ImGuiController.Render();
    }
  }
}